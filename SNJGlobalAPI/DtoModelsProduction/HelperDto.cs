namespace SNJGlobalAPI.DtoModels
{
    public class Responder<T>
    {
        public int? Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

    public class LoginResponder<T>
    {
        public int? Code { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public T Data { get; set; }
    }


    public class PathResponder
    {
        public string fileName { get; set; }
        public string folderPath { get; set; }
        public string FileType { get; set; }
    }
    public class SearchDto
    {
        public int page { get; set; }
        public string search { get; set; }
    }


    public class PagingInfo
    {
        public static int take { get; set; }
    }

    public class UploadFileDto
    {
        public IFormFile File { get; set; }
        public int CurrentChunk { get; set; }
        public int TotalChunks { get; set; }
    }

}
