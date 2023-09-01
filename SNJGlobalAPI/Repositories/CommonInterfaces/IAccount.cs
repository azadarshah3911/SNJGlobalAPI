using SNJGlobalAPI.DtoModels;

namespace SNJGlobalAPI.Repositories.CommonInterfaces
{
    public interface IAccount
    {
        Task<bool> IsExistsAsync(string email, int id);

        Task<Responder<string>> SigninAsync(SigninDto dto);
        Task<Responder<string>> SignupAsync(SignupDto dto);

        Task<Responder<string>> AddUserInListAsync(List<SignupInListDto> dto);

        Task<Responder<UserGetDtoForUser>> GetUserForUserAsync(int id);
        Task<Responder<UserGetDtoForAdmin>> GetUserForAdminAsync(int id);
        Task<Responder<List<UserGetDtoForAdmin>>> GetAllUsersAsync(SearchDto search);

        Task<Responder<string>> UpdateAsync(UserUpdateDto dto);
        Task<Responder<string>> UpdateUserFromUserAsync(UserUpdateForUserDto dto);

        Task<Responder<string>> DeleteAsync(int id);

        Task<Responder<string>> ActtDctAsync(UserActDctInputDto dto);
        Task<Responder<string>> UnlockAsync(int id);

        Task<Responder<string>> ConfirmEmailAsync(EmailDto dto);
        Task<Responder<string>> ResetPswdAsync(NewPswdDto dto);

        Task<Responder<List<RolesGetDto>>> GetRolesAsync();
        Task<Responder<string>> GetCurrentUserIdAsync();
        Task<Responder<AuthorizationDto>> GetCurrentUserAuthorization();
        Task<Responder<string>> SignoutAsync();
    }
}