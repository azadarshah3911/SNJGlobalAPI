using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SNJGlobalAPI.DbModels;
using SNJGlobalAPI.DbModelsProduction;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.GeneralServices;
using SNJGlobalAPI.Mappers;
using SNJGlobalAPI.Repositories.CommonInterfaces;
using SNJGlobalAPI.SecurityHandlers;
using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using static System.Net.WebRequestMethods;
using static System.Reflection.Metadata.BlobBuilder;

namespace SNJGlobalAPI.Repositories.CommonRepos
{
    public class AccountRepo : IAccount
    {
        private readonly IDb _db;
        private readonly HttpContext _httpContext;
        private readonly IConfiguration _config;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMapper _mapper;

        public AccountRepo(IDb db, IHttpContextAccessor accessor, IConfiguration config, IJwtHandler jwtHandler, IMapper mapper)
        {
            _db = db;
            _httpContext = accessor.HttpContext;
            _config = config;
            _jwtHandler = jwtHandler;
            _mapper = mapper;
        }
        public async Task<Responder<UserGetDtoForAdmin>> GetUserForAdminAsync(int id)
        {
            //check and get it
            var User = await _db.GetByAsync<User, UserGetDtoForAdmin>(
                w => w.ID == id &&
                w.DeletedAt == null,
                UserMapper.UserGetForAdmin);

            if (User is null) return Rr.NotFound<UserGetDtoForAdmin>("User", id.ToString());

            return Rr.SuccessFetch(User);
        }
        public async Task<Responder<UserGetDtoForUser>> GetUserForUserAsync(int id)
        {
            //check and get it
            var User = await _db.GetByAsync<User, UserGetDtoForUser>(
                w => w.ID == id &&
                w.DeletedAt == null,UserMapper.GetUserForUser);

            if (User is null) return Rr.NotFound<UserGetDtoForUser>("User", id.ToString());

            return Rr.SuccessFetch(User);
        }
        public async Task<Responder<List<UserGetDtoForAdmin>>> GetAllUsersAsync(SearchDto search)
        {
            if (_httpContext.User.IsInRole(appRolesNameDto.Agent) || _httpContext.User.IsInRole(appRolesNameDto.ChassingVerificationAgent) || _httpContext.User.IsInRole(appRolesNameDto.QaAgent) || _httpContext.User.IsInRole(appRolesNameDto.ChassingAgent))
                return Rr.NoData(new List<UserGetDtoForAdmin>());

            Expression<Func<UserRole, bool>> predicateAny = null;

            if (_httpContext.User.IsInRole(appRolesNameDto.QaManager))
                predicateAny = where => where.Fk_RoleId == 5;
            else if (_httpContext.User.IsInRole(appRolesNameDto.ProductionManager))
                predicateAny = where => where.Fk_RoleId == 2 || where.Fk_RoleId == 9 || where.Fk_RoleId == 10;
            else if (_httpContext.User.IsInRole(appRolesNameDto.TeamLead))
                predicateAny = where => where.Fk_RoleId == 2;
            else if (_httpContext.User.IsInRole(appRolesNameDto.ChassingManager))
                predicateAny = where => where.Fk_RoleId == 7 || where.Fk_RoleId == 8;
          
            //get all Users
            var Users = predicateAny is not null ? await _db.GetAllByAsync<User, UserGetDtoForAdmin>(
                    UserMapper.UserGetForAdmin,
                    w => w.usersRoles.AsQueryable().Any(predicateAny) && w.DeletedAt == null &&
                    (w.FirstName.Contains(search.search) ||
                      w.LastName.Contains(search.search) ||
                      w.Email.Contains(search.search))
                     ,
                    page: search.page) : 
                    await _db.GetAllByAsync<User, UserGetDtoForAdmin>(
                    UserMapper.UserGetForAdmin,
                    w => w.DeletedAt == null &&
                    (w.FirstName.Contains(search.search) ||
                      w.LastName.Contains(search.search) ||
                      w.Email.Contains(search.search)),
                    page: search.page); 

            if (Users.Any()) return Rr.NoData(Users);

            return Rr.SuccessFetch(Users);
        }
        public async Task<Responder<string>> SignupAsync(SignupDto dto)
        {
            //check is already exists
            if (await IsExistsAsync(dto.Email, 0)) return Rr.Custom<string>($"{dto.Email} already exists");

            //map
            var map = _mapper.Map<User>(dto);
            //hash passwrod
            Hashing.CreatePasswordHash(dto.Password, out byte[] hash, out byte[] salt);
            map.PasswordHash = hash;
            map.PasswordSalt = salt;
            map.Fk_CreatedBy = JwtHandlerRepo.GetCrntUserId(_httpContext);

            if (dto.ProfileImage is not null)
            {
                var path = await UploadFiles.SaveAsync(dto.ProfileImage, "User/Image");
                if (path is null)
                    return Rr.Fail<string>("update");
                map.Image = $"{path.folderPath}/{path.fileName}";
            }

            var tran = await _db.BeginTranAsync();
            if (!await _db.PostAsync(map)) return Rr.Fail<string>("create");

            //now add roles
            if (!await AddRolesAsync(dto.Roles, map.ID, false, map.Fk_CreatedBy))
            {
                await tran.RollbackAsync();
                return Rr.Fail<string>("create");
            }
            await tran.CommitAsync();
            return Rr.Success<string>("created", map.ID.ToString());
        }
        public async Task<Responder<string>> UpdateAsync(UserUpdateDto dto)
        {
            //verify User id
            var user = await _db.GetAsync<User>(w => w.ID == dto.Id && w.DeletedAt == null);
            if (user is null) return Rr.NotFound<string>("user", dto.Id.ToString());

            if (user.Email != dto.Email && await IsExistsAsync(dto.Email, user.ID)) return Rr.Custom<string>($"Your new email already exists {dto.Email}");
            if (dto.Password is not null)
            {
                Hashing.CreatePasswordHash(dto.Password, out byte[] hash, out byte[] salt);
                user.PasswordHash = hash;
                user.PasswordSalt = salt;
            }
            if (dto.Image is not null)
            {
                var path = await UploadFiles.SaveAsync(dto.Image, "User/Image");
                if (path is null)
                    return Rr.Fail<string>("update");
                user.Image = $"{path.folderPath}/{path.fileName}";
            }
            //swap data
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Email = dto.Email;
            user.Fk_BranchId = dto.Fk_BranchId;
            user.PhoneNumber = dto.PhoneNumber;
            user.Address = dto.Address;
            user.NicNo = dto.NicNo;
            user.JoiningDate = dto.JoiningDate;
            user.Gender = dto.Gender;
            user.EmployeeId = dto.EmployeeId;
            user.EmergencyContact = dto.EmergencyContact;
            user.UpdatedAt = DateTime.UtcNow;
            user.Fk_UpdatedBy = JwtHandlerRepo.GetCrntUserId(_httpContext);

            //begin transaction. 
            var tran = await _db.BeginTranAsync();
            if (!await _db.UpdateAsync(user)) return Rr.Fail<string>("update");

            if (!await AddRolesAsync(dto.Roles, user.ID, true, user.Fk_UpdatedBy))
            {
                await tran.RollbackAsync();
                return Rr.Fail<string>("update");
            }

            await tran.CommitAsync();
            return Rr.Success<string>("updated");
        }//update method

        public async Task<Responder<string>> UpdateUserFromUserAsync(UserUpdateForUserDto dto)
        {
            //verify User id
            var user = await _db.GetAsync<User>(w => w.ID == dto.Id && w.DeletedAt == null);
            if (user is null) return Rr.NotFound<string>("user", dto.Id.ToString());

            if (user.Email != dto.Email && await IsExistsAsync(dto.Email, user.ID)) return Rr.Custom<string>($"Your new email already exists {dto.Email}");
            if(dto.Password is not null)
            {
                Hashing.CreatePasswordHash(dto.Password, out byte[] hash, out byte[] salt);
                user.PasswordHash = hash;
                user.PasswordSalt = salt;
            }
            if(dto.Image is not null)
            {
                var path = await UploadFiles.SaveAsync(dto.Image, "User/Image");
                if (path is null)
                    return Rr.Fail<string>("update");
                user.Image = $"{path.folderPath}/{path.fileName}";
            }
            //swap data
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Email = dto.Email;
            user.Fk_BranchId = dto.Fk_BranchId;
            user.PhoneNumber = dto.PhoneNumber;
            user.Address = dto.Address;
            user.NicNo = dto.NicNo;
            user.JoiningDate = dto.JoiningDate;
            user.Gender = dto.Gender;
            user.EmployeeId = dto.EmployeeId;
            user.EmergencyContact = dto.EmergencyContact;
          

            //begin transaction. 
            if (!await _db.UpdateAsync(user)) return Rr.Fail<string>("update");
            return Rr.Success<string>("updated");
        }//update method

        private async Task<bool> AddRolesAsync(ICollection<int> roles, int userId, bool removeOld, int? byUser)
        {
            if (!roles.Any()) return true;//if no roles then return true;

            /*
             * remove old, is true, 
             * when updating user info
             */
            if (removeOld)
            {
                string query = $"DELETE FROM [UsersRoles] WHERE [Fk_UserId]={userId}";
                if (await _db.IsAnyAsync<UserRole>(predicate: w => w.Fk_UserId == userId))
                    //if any old role 
                    if (!await _db.SqlQueryAsync(query))
                    return false;
            } //if old reles toremove

            //now roles
            List<UserRole> UserRoles = new();
            foreach (int roleId in roles)
            {
                UserRoles.Add(new()
                {
                    Fk_UserId = userId,
                    Fk_RoleId = roleId,
                    Fk_CreatedBy = byUser
                });
            }
            //now add above roles list into db
            return await _db.PostRangeAsync(UserRoles);
        }
        public async Task<Responder<string>> SigninAsync(SigninDto dto)
        {
            //first confirm form our db table
            var user = await _db.GetAsync<User>(w => w.Email == dto.Email && w.DeletedAt == null,
                includes: s => s.Include(i => i.usersRoles).ThenInclude(t => t.role).Include(a => a.branch));

            //check is there any
            if (user is null) return Rr.Custom<string>("Invalid Email Id");

            //check is account deactivagted by admin 
            if (!user.IsActivated)
                return Rr.Custom("Your account is deactivated by admin", "");


            //check is account locked causes of failure to login
            if (user.LockedOn != null)
                return Rr.Custom("Your account is Locked", "");

            //verify password
            bool isValid = Hashing.VerifyPassword(dto.Password, user.PasswordHash, user.PasswordSalt);
            //check is login fail, 
            if (!isValid)
            {
                await LoginFailAsync(user.ID, user.LoginFailedOn, user.LoginFailedCount);
                return Rr.Custom<string>("Invalid Password");
            }//if not success

            //now create JWT token to autheticate volunteer
            string jwtToken = _jwtHandler.CreateToken(new()
            {
                Id = user.ID,
                Name = user.FirstName + " " + user.LastName,
                Branch = user.branch.Name,
                Image = !String.IsNullOrEmpty(user.Image) ? user.Image : ".",
                Roles = user.usersRoles?.Select(s => s.role.Name).ToList() ?? new(),
            });
            await AddLoginAsync(user.ID, true);
            return Rr.Custom("Successfully LoggedIn", jwtToken);
        }//method
        private async Task AddLoginAsync(int userId, bool IsSuccessfull)
        {
            await _db.PostAsync<UserLogin>(new() { Fk_UserId = userId, IsLoggedIn = IsSuccessfull });
        }
        private async Task LoginFailAsync(int id, DateTime? failedOn, byte failCount)
        {
            //get allowed attempts from config
            byte limit = _config.GetValue<byte>("Account:FailLogin");

            string query = string.Empty;
            //check how many already failed attempted in single day, if limit over then block account

            if (failCount >= limit && failedOn <= DateTime.UtcNow.AddDays(1))
                query = $"UPDATE [users] SET [LoginFailedOn]=null, [LoginFailedCount]=0, [LockedOn]=GETDATE() WHERE [ID]={id}";
            else//counting no of failed attempt
                query = $"UPDATE [users] SET [LoginFailedOn]=GETDATE(), [LoginFailedCount]={++failCount} WHERE [ID]={id}";
            await _db.SqlQueryAsync(query);
            await AddLoginAsync(id, false);
        }
        public async Task<bool> IsExistsAsync(string email, int id) =>
            await _db.IsAnyAsync<User>(x => x.Email == email && x.ID != id);
        public async Task<Responder<string>> ActtDctAsync(UserActDctInputDto dto)
        {
            //get User
            var user = await _db.GetByAsync<User, UserActDctOutputDto>(w =>
            w.ID == dto.UserId &&
            w.DeletedAt == null);

            if (user is null) return Rr.NotFound<string>("User", dto.UserId.ToString());

            //check if user alrady activated then deactivate and viceversa
            bool whatToDo = user.IsActivated ? false : true;
            string query = $"UPDATE [Users] SET [IsActivated]={(whatToDo ? 1 : 0)}, [ActDctAt]=GETDATE(), [Fk_ActDctBy]=@actdctBy WHERE [ID]=@userid";

            var userId = new SqlParameter("@userid", dto.UserId);
            var actdctBy = new SqlParameter("@actdctBy", JwtHandlerRepo.GetCrntUserId(_httpContext));

            string action = whatToDo ? "Activate" : "Deactivate";
            if (!await _db.SqlQueryAsync(query, actdctBy, userId)) return Rr.Fail<string>(action);

            return Rr.Success<string>($"{action}d");
        }
        public async Task<Responder<string>> DeleteAsync(int id)
        {
            //confirm is such user exists 
            if (await _db.IsAnyAsync<User>(w => w.ID == id && w.DeletedAt == null))
                return Rr.NotFound<string>("User", id.ToString());

            //query to mark delete
            var deletedBy = new SqlParameter("@deletedBy", JwtHandlerRepo.GetCrntUserId(_httpContext));
            var userId = new SqlParameter("@userId", id);

            string query = "UPDATE [users] SET [DeletedAt]=@GETDATE(), [Fk_DeletedBy]=@deletedBy WHERE [ID]=@userId";

            if (!await _db.SqlQueryAsync(query, deletedBy, userId)) return Rr.Fail<string>("Delete");

            return Rr.Success<string>("Deleted");
        }
        public async Task<Responder<string>> UnlockAsync(int id)
        {
            //check is user exists
            var user = await _db.GetByAsync<User, UserLockDto>(w => w.ID == id && w.DeletedAt == null);

            if (user is null) return Rr.NotFound<string>("User", id.ToString());

            var userid = new SqlParameter("@id", id);
            string query = $"UPDATE [users] SET [LockedOn]=null WHERE id=@id";
            if (!await _db.SqlQueryAsync(query, userid)) return Rr.Fail<string>("unlock");
            return Rr.Success<string>("Unlocked");

        }
        public async Task<Responder<string>> ConfirmEmailAsync(EmailDto dto)
        {
            //get record
            var user = await _db.GetByAsync<User, UserResetDto>
                (w => w.Email == dto.Email && w.DeletedAt == null);

            if (user is null) return Rr.NotFound<string>("User", dto.Email);

            //check is it's account deactivated
            if (user.IsActivated)
                return Rr.Custom("Your account is deactivated by admin", "");

            //now if exists then send reset email with link attach 
            var reset = new ForgetPassword()
            {
                Fk_UserId = user.ID
            };

            if (!await _db.PostAsync(reset)) return Rr.Fail<string>("Reset Password");

            //get base address for email reset page
            string parentLink = _config.GetValue<string>("Urls:ForgetPswd");
            var mailInfo = BodyMaker.Make(
                user.Email,
                "Password Reset SNJ",
                _config.GetValue<string>("EmailTemplates:ForgetPswd"),
                user.FirstName + " " + user.LastName,
                parentLink,
                reset.Id);

            //now send mail
            await SendMailAsync(mailInfo);
            return Rr.Custom("Request Successfully Completed", "Please check your mail box");
        }
        public async Task<Responder<string>> ResetPswdAsync(NewPswdDto dto)
        {
            //confirm is reset id exists
            var confirm = await _db.GetAsync<ForgetPassword>(w => w.Id == dto.ResetId);
            if (confirm is null) return Rr.Custom("Invalid Request", "Please don't change key");

            //now check is already used
            if (confirm.UsedOn != null) return Rr.Custom("Link already used", "Please request new reset");

            //get link expiry time
            byte mints = _config.GetValue<byte>("Account:ResetTime");

            //check is requst under specific time
            if (confirm.RequestedOn.AddMinutes(mints) < DateTime.UtcNow)
                return Rr.Custom("Link expired", "Try reset password again");

            //now hash passwords
            Hashing.CreatePasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var hash = new SqlParameter("@hash", passwordHash);
            var salt = new SqlParameter("@salt", passwordSalt);
            var id = new SqlParameter("@id", confirm.Fk_UserId);

            //reset password
            string query = $"UPDATE [users] SET [PasswordSalt]=@salt, [PasswordHash]=@hash WHERE [ID]=@id";
            if (!await _db.SqlQueryAsync(query, salt, hash, id))
                return Rr.Fail<string>("Reset Password");

            //mark password link used 
            await _db.SqlQueryAsync($"UPDATE [ForgetPasswords] SET [UsedOn]=GETDATE() WHERE [id]={confirm.Id}");

            return Rr.Success<string>("Password Resetted");
        }
        public async Task<Responder<List<RolesGetDto>>> GetRolesAsync() => Rr.SuccessFetch(await _db.GetAllByAsync<Role, RolesGetDto>(UserMapper.Roles));
        private async Task SendMailAsync(EmailOptions op)
        {
            try
            {
                string response = await EmailService.SendAsync(op);

                //now save record in db
                var track = new MailsTracking()
                {
                    Email = String.Join(',', op.recipients),
                    Subject = op.subject,
                    Body = op.body,
                    SentAt = response == "Success" ? DateTime.UtcNow : null,
                    NotSentReason = response == "Success" ? null : response
                };
                await _db.PostAsync(track);
            }
            catch (Exception) { }
        }

        //By Azadar
        public async Task<Responder<string>> GetCurrentUserIdAsync()
        {
            int? user = JwtHandlerRepo.GetCrntUserId(_httpContext);
            return Rr.Custom<string>(user.ToString());

        }


        public async Task<Responder<AuthorizationDto>> GetCurrentUserAuthorization()
        {
            AuthorizationDto dto = new();
            if (_httpContext.User.IsInRole(appRolesNameDto.SuperAdmin))
                dto = new()
                {
                    ShowDashboard = true,
                    ShowLeads = true,
                    ShowAllLeads = true,
                    ShowUploadLead = true,
                    ShowEligibility = true,
                    ShowSns = true,
                    ShowQa = true,
                    ShowChassing = true,
                    ShowVerification = true,
                    ShowCgmAndBraces = true,
                    ShowConfirmation = true,
                };
            else if (_httpContext.User.IsInRole(appRolesNameDto.Agent))
                dto = new()
                {
                    ShowDashboard = true,
                    ShowLeads = true,
                    ShowAllLeads = true,
                    ShowUploadLead = true,
                    ShowEligibility = false,
                    ShowSns = false,
                    ShowQa = false,
                    ShowChassing = false,
                    ShowVerification = false,
                    ShowCgmAndBraces = false,
                    ShowConfirmation = false,
                };
            else if (_httpContext.User.IsInRole(appRolesNameDto.ProductionManager))
                dto = new()
                {
                    ShowDashboard = true,
                    ShowLeads = true,
                    ShowAllLeads = true,
                    ShowUploadLead = true,
                    ShowEligibility = true,
                    ShowSns = false,
                    ShowQa = false,
                    ShowChassing = false,
                    ShowVerification = false,
                    ShowCgmAndBraces = false,
                    ShowConfirmation = false,
                };
            else if (_httpContext.User.IsInRole(appRolesNameDto.TeamLead))
                dto = new()
                {
                    ShowDashboard = true,
                    ShowLeads = true,
                    ShowAllLeads = true,
                    ShowUploadLead = true,
                    ShowEligibility = false,
                    ShowSns = false,
                    ShowQa = false,
                    ShowChassing = false,
                    ShowVerification = false,
                    ShowCgmAndBraces = false,
                    ShowConfirmation = false,
                };
            else if (_httpContext.User.IsInRole(appRolesNameDto.QaAgent))
                dto = new()
                {
                    ShowDashboard = true,
                    ShowLeads = true,
                    ShowAllLeads = false,
                    ShowUploadLead = true,
                    ShowEligibility = true,
                    ShowSns = true,
                    ShowQa = true,
                    ShowChassing = false,
                    ShowVerification = false,
                    ShowCgmAndBraces = false,
                    ShowConfirmation = false,
                };
            else if (_httpContext.User.IsInRole(appRolesNameDto.QaManager))
                dto = new()
                {
                    ShowDashboard = true,
                    ShowLeads = true,
                    ShowAllLeads = true,
                    ShowUploadLead = true,
                    ShowEligibility = true,
                    ShowSns = true,
                    ShowQa = true,
                    ShowChassing = false,
                    ShowVerification = false,
                    ShowCgmAndBraces = false,
                    ShowConfirmation = false,
                };
            else if (_httpContext.User.IsInRole(appRolesNameDto.ChassingAgent))
                dto = new()
                {
                    ShowDashboard = true,
                    ShowLeads = false,
                    ShowAllLeads = false,
                    ShowUploadLead = false,
                    ShowEligibility = false,
                    ShowSns = false,
                    ShowQa = false,
                    ShowChassing = true,
                    ShowVerification = false,
                    ShowCgmAndBraces = true,
                    ShowConfirmation = true,
                };
            else if (_httpContext.User.IsInRole(appRolesNameDto.ChassingManager))
                dto = new()
                {
                    ShowDashboard = true,
                    ShowLeads = true,
                    ShowAllLeads = true,
                    ShowUploadLead = true,
                    ShowEligibility = true,
                    ShowSns = true,
                    ShowQa = true,
                    ShowChassing = true,
                    ShowVerification = true,
                    ShowCgmAndBraces = true,
                    ShowConfirmation = true,
                };
            else if (_httpContext.User.IsInRole(appRolesNameDto.ChassingVerificationAgent))
                dto = new()
                {
                    ShowDashboard = false,
                    ShowLeads = false,
                    ShowAllLeads = false,
                    ShowUploadLead = false,
                    ShowEligibility = false,
                    ShowSns = false,
                    ShowQa = false,
                    ShowChassing = true,
                    ShowVerification = true,
                    ShowCgmAndBraces = false,
                    ShowConfirmation = false,
                };
            else if (_httpContext.User.IsInRole(appRolesNameDto.LeadFixer))
                dto = new()
                {
                    ShowDashboard = false,
                    ShowLeads = false,
                    ShowAllLeads = false,
                    ShowUploadLead = false,
                    ShowEligibility = true,
                    ShowSns = false,
                    ShowQa = false,
                    ShowChassing = false,
                    ShowVerification = false,
                    ShowCgmAndBraces = false,
                    ShowConfirmation = false,
                };
            return Rr.SuccessFetch(dto);
        }

        public async Task<Responder<string>> SignoutAsync()
        {
            await _httpContext.SignOutAsync();
            return Rr.Success<string>("Success","Success");
        }


        public async Task<Responder<string>> AddUserInListAsync(List<SignupInListDto> data)
        {
            //check is already exists

            var tran = await _db.BeginTranAsync();
            foreach (var dto in data)
            {
                //map
                var map = _mapper.Map<User>(dto);
                //hash passwrod
                Hashing.CreatePasswordHash(dto.Password, out byte[] hash, out byte[] salt);
                map.PasswordHash = hash;
                map.PasswordSalt = salt;
                map.Fk_CreatedBy = JwtHandlerRepo.GetCrntUserId(_httpContext);

                if (!await _db.PostAsync(map)) return Rr.Fail<string>("create");

                //now add roles
                if (!await AddRolesAsync(dto.Roles, map.ID, false, map.Fk_CreatedBy))
                {
                    await tran.RollbackAsync();
                    return Rr.Fail<string>("create");
                }
            }
            
            await tran.CommitAsync();
            return Rr.Success<string>("created", "Success");
        }
    }//class repo

}//namespace