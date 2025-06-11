using Books.ApplicationBusiness.Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Books.EnterpriseBusiness.Layer.Models
{
    public class ModelResult<T> : IModelResult<T>
    {
        public int Code { get; set; }
        
        public string Message { get; set; } 
        public bool IsSuccess { get; set; }
        public List<T>? Data { get; set; }
        public string Token { get ; set ; }

        public ModelResult()
        {
         
        }
        public ModelResult(int code, List<T> data, string message)
        {
            Code = code;
            Data = data;
            Message = message;
        }
       
        public ModelResult(int _code, string _message)
        {
            Code = _code;
            Message = _message;
        }

        public ModelResult(int _code,string token, string _message)
        {
            Code = _code;
            Token = token;
            Message = _message;
        }

        public static ModelResult<T> AddMessage(int Code, List<T> data, string message)
        {
            return new ModelResult<T>(Code, data, message);
        }
    }
}
