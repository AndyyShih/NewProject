using Common.Enums;

namespace DataAccess.Models.ResponseModel
{
    /// <summary>
    /// 無回傳DTO
    /// </summary>
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public ApiStatusCode StatusCode { get; set; }

        public static ApiResponse Create(ApiStatusCode statusCode, string message = null, List<string> errors = null)
        {
            return new ApiResponse
            {
                IsSuccess = statusCode < ApiStatusCode.BadRequest,
                StatusCode = statusCode,
                Message = message,
                Errors = errors
            };
        }
    }

    /// <summary>
    /// 有回傳DTO
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public ApiStatusCode StatusCode { get; set; }

        public ApiResponse()
        {

        }

        public static ApiResponse<T> Create(ApiStatusCode statusCode, T data = default, string message = null, List<string> errors = null)
        {
            return new ApiResponse<T>
            {
                IsSuccess = statusCode < ApiStatusCode.BadRequest,
                StatusCode = statusCode,
                Message = message,
                Data = data,
                Errors = errors
            };
        }
    }
}
