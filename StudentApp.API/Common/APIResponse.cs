using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace StudentApp.API.Common
{
    public sealed class APIResponse<T>
    {
        public int StatusCode { get; set; } = 200;
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public DateTime ResponseTime { get; set; }

        private APIResponse(int statusCode)
        {
            StatusCode = statusCode;
            ResponseTime = DateTime.UtcNow;
        }

        private APIResponse(string message, int statusCode) : this(statusCode) => this.Message = message;
        private APIResponse(T? data, int statusCode, string message) : this(message, statusCode) => this.Data = data;

        public static APIResponse<T> Success(int statusCode = 200)
        {
            return new APIResponse<T>(statusCode);
        }

        public static APIResponse<T> Success(string message, int statusCode = 200)
        {
            return new APIResponse<T>(message, statusCode);
        }

        public static APIResponse<T> Success(T data, string message, int statusCode = 200)
        {
            return new APIResponse<T>(data, statusCode, message);
        }

        public static APIResponse<T> Error(int statusCode = 500)
        {
            return new APIResponse<T>(statusCode);
        }

        public static APIResponse<T> Error(string message, int statusCode = 500)
        {
            return new APIResponse<T>(message,statusCode);
        }

        public static APIResponse<T> Error(T data, string message, int statusCode = 500)
        {
            return new APIResponse<T>(data, statusCode, message);
        }
    }
}
