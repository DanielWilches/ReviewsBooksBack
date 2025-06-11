using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.ApplicationBusiness.Layer.Interfaces
{
    public interface IModelResult<T>
    {
        int Code { get; set; }
        List<T>? Data { get; set; }
        string Message { get; set; }
        string Token{ get; set; }

        bool IsSuccess { get; }
    }

}
