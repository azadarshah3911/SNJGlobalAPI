using SNJGlobalAPI.DtoModels;

namespace SNJGlobalAPI.Repositories.CommonInterfaces
{
    public interface IUploadFile
    {
        Task<Responder<string>> UploadFile(UploadFileDto dto, string folder); 
    }
}
