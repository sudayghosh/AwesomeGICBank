using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeGIC.Bank.UI.Dto
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public int statusCode { get; set; }
    }
}
