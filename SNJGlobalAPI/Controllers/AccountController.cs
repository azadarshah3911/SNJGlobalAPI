using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.Repositories.CommonInterfaces;

namespace SNJGlobalAPI.Controllers
{

    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccount _repo;
        public AccountController(IAccount repo) =>
            _repo = repo;

        [HttpPost("Signin"), AllowAnonymous]
        public async Task<IActionResult> Signin(SigninDto dto) =>
            Ok(await _repo.SigninAsync(dto));

        //[HttpGet("GetRoles"), Authorize(Roles = "SuperAdmin")]
        [HttpGet("GetRoles"), AllowAnonymous]
        public async Task<IActionResult> GetRoles() =>
            Ok(await _repo.GetRolesAsync());

        //[HttpPost("Signup"), Authorize(Roles = "SuperAdmin")]
        [HttpPost("Signup"), AllowAnonymous]
        public async Task<IActionResult> Signup([FromForm]SignupDto dto) =>
            Ok(await _repo.SignupAsync(dto));

        [HttpPost("Update"), AllowAnonymous]
        public async Task<IActionResult> Update([FromForm]UserUpdateDto dto) =>
            Ok(await _repo.UpdateAsync(dto));

        [HttpPost("UpdateFromUser"), AllowAnonymous]
        public async Task<IActionResult> UpdateFromUser([FromForm]UserUpdateForUserDto dto) =>
           Ok(await _repo.UpdateUserFromUserAsync(dto));

        [HttpPost("ActivateDeactivate"), AllowAnonymous]
        public async Task<IActionResult> ActivateDeactivate(UserActDctInputDto dto) =>
            Ok(await _repo.ActtDctAsync(dto));

        [HttpDelete("Delete"), AllowAnonymous]
        public async Task<IActionResult> Delete(int userid) =>
            Ok(await _repo.DeleteAsync(userid));

        [HttpGet("Unlock"), AllowAnonymous]
        public async Task<IActionResult> Unlock(int userid) =>
            Ok(await _repo.UnlockAsync(userid));

        [HttpPost("ForgetPassword"), AllowAnonymous]
        public async Task<IActionResult> ForgetPassword(EmailDto dto) =>
            Ok(await _repo.ConfirmEmailAsync(dto));

        [HttpPost("ResetPassword"), AllowAnonymous]
        public async Task<IActionResult> ResetPassword(NewPswdDto dto) =>
            Ok(await _repo.ResetPswdAsync(dto));

        [HttpGet("GetUserForUser"), AllowAnonymous]
        public async Task<IActionResult> GetUserForUser(int userid) =>
            Ok(await _repo.GetUserForUserAsync(userid));

        [HttpGet("GetUserForAdmin"), AllowAnonymous]
        public async Task<IActionResult> GetUserForAdmin(int userid) =>
            Ok(await _repo.GetUserForAdminAsync(userid));

        [Authorize(Roles = $"{appRolesNameDto.ChassingManager},{appRolesNameDto.SuperAdmin},{appRolesNameDto.ProductionManager},{appRolesNameDto.QaManager}")]
        [HttpPost("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers(SearchDto dto) =>
            Ok(await _repo.GetAllUsersAsync(dto));

        [HttpGet("GetLoggedInUserId"), AllowAnonymous]
        public async Task<IActionResult> GetLoggedInUserId() =>
           Ok(await _repo.GetCurrentUserIdAsync());

        [HttpPost("Signout"), AllowAnonymous]
        public async Task<IActionResult> Signout() =>
            Ok(await _repo.SignoutAsync());

        [HttpGet("GetLoggedInUserAuthorization"), AllowAnonymous]
        public async Task<IActionResult> GetLoggedInUserAuthorization() =>
          Ok(await _repo.GetCurrentUserAuthorization());


        [HttpPost("ImportExcelFile"), AllowAnonymous]
        public async Task<IActionResult> ImportExcelFile(List<SignupInListDto> dto) =>
            Ok(await _repo.AddUserInListAsync(dto));

    }//account controller
}//namespace