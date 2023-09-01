using SNJGlobalAPI.DtoModels;

namespace SNJGlobalAPI.GeneralServices
{
    public static class Rr
    {
        //Response Results
        //common responses
        //custom message
        public static Responder<T> Custom<T>(string msg, T t = null) where T : class =>
            new() { Code = 204, Message = msg, Data = t };

        //already exists
        public static Responder<T> Exists<T>(string what, string identity) where T : class
            => new() { Code = 200, Message = $"{what} with {identity} already exists" };

        public static Responder<T> InvalidModel<T>(string msg = null) where T : class
            => new() { Code = 204, Message = $"Invalid model {msg}" };

        //Success
        public static Responder<T> Success<T>(string action, T obj = null) where T : class
          => new() { Code = 200, Message = $"Successfully {action}", Data = obj };

        public static Responder<T> SuccessFetch<T>(T obj = null, string msg = null) where T : class
            => new() { Code = 200, Message = $"Successfully fetched record(s) {msg}", Data = obj };

        //For any kind failuure such as create, update, delete, act/deact
        public static Responder<T> Fail<T>(string action, string reason = null) where T : class
            => new() { Code = 500, Message = $"Failed to {action} record {reason}" };

        //no data
        public static Responder<T> NoData<T>(T t = null) where T : class
            => new() { Code = 204, Message = "No data found", Data = t };

        //no such data found
        public static Responder<T> NotFound<T>(string what, string identity) where T : class
            => new() { Code = 204, Message = $"record with {identity} not found with in {what}" };

    }
}
