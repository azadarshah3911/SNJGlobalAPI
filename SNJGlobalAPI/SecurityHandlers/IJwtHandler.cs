using SNJGlobalAPI.DtoModels;

namespace SNJGlobalAPI.SecurityHandlers
{
    public interface IJwtHandler
    {
        string CreateToken(UserAuthDto user);
    }
}
