using System.Collections.Generic;


namespace Vennderful.Application.Common
{
    public enum ResponseStatus
    {
        Error,
        Success,
        Warning,
        Info
    }
    public class BaseResponse
    {
        public bool Success { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public dynamic Data { get; set; }
    }
}
