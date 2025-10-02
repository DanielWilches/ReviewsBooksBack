using Books.Domain.Layer.Interfaces;

namespace Books.Application.Layer.DTOs
{
    public class ResultDto<T> : IResultDto<T>
    {
        public int Code { get; set; }
        
        public string Message { get; set; } 
        public bool IsSuccess { get; set; }
        public List<T>? Data { get; set; }
        public string Token { get ; set ; }

        public ResultDto()
        {
         
        }
        public ResultDto(int code, List<T> data, string message)
        {
            Code = code;
            Data = data;
            Message = message;
        }
       
        public ResultDto(int _code, string _message)
        {
            Code = _code;
            Message = _message;
        }

        public ResultDto(int _code,string token, string _message)
        {
            Code = _code;
            Token = token;
            Message = _message;
        }

        public static ResultDto<T> AddMessage(int Code, List<T> data, string message)
        {
            return new ResultDto<T>(Code, data, message);
        }
    }
}
