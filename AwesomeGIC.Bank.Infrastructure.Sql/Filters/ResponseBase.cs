using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeGIC.Bank.Infrastructure.Filters
{
    public class ResponseBase<T>
    {
        public ResponseBase()
        {
        }
        public ResponseBase(T data, int statusCode, string message, string status)
        {
            StatusCode = statusCode;
            Message = message;
            Status = status;
            Data = data;
        }

        public T Data { get; set; }
        public int StatusCode { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }

    }
}
