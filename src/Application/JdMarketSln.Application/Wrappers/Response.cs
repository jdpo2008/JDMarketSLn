using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Wrappers
{
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T data, string message = null, bool isSuccess = true)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }
        public Response(string message)
        {
            IsSuccess = true;
            Message = message;
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public IDictionary<string, string[]> Errors { get; set; }
        public T Data { get; set; }
    }
}
