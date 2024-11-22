using MediatR;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeGIC.Bank.Web.Api.Common
{
    public class BaseController : ControllerBase
    {
        protected readonly ISender _sender;

        public BaseController(ISender sender)
        {
            _sender = sender;
        }
    }
}
