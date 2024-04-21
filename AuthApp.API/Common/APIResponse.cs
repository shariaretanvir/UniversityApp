namespace AuthApp.API.Common
{
    public class APIResponse<T>
    {
        public int StatusCode { get; private set; }
        public T Data { get; private set; }
        public string Message { get; private set; } = string.Empty;
        public DateTime CreatedOn { get; private set; }

        private APIResponse(int statudCode)
        {
            StatusCode = statudCode;
            CreatedOn = DateTime.Now;
        }
        private APIResponse(string message, int statudCode = 200) : this(statudCode) => Message = message;
        private APIResponse(T data, string message, int statudCode = 200) : this(message, statudCode) => Data = data;

        public static APIResponse<T> Success(int statudCode)
        {
            return new APIResponse<T>(statudCode);
        }

        public static APIResponse<T> Success(string message, int statudCode = 200)
        {
            return new APIResponse<T>(message, statudCode);
        }

        public static APIResponse<T> Success(T data, string message = "", int statudCode = 200)
        {
            return new APIResponse<T>(data, message, statudCode);
        }

        public static APIResponse<T> Error(int statudCode = 500)
        {
            return new APIResponse<T>(statudCode);
        }

        public static APIResponse<T> Error(string message, int statudCode = 500)
        {
            return new APIResponse<T>(message, statudCode);
        }

        public static APIResponse<T> Error(T data, string message, int statudCode = 500)
        {
            return new APIResponse<T>(data, message, statudCode);
        }
    }
}
