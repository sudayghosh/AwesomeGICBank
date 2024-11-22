using AwesomeGIC.Bank.Application.Account.Queries;
using AwesomeGIC.Bank.Web.Api.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeGIC.Bank.Web.Api.Endpoints
{
    /// <summary>
    /// 
    /// </summary>
    [Route("account")]
    public class AccountController : BaseController
    {
        public AccountController(ISender sender) : base(sender) 
        {            
        }

        //[ApiKey]
        [HttpGet("{accountNo}/list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<AccountDto> AccountListByAccountNo(string accountNo
            , CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetAccountListByAccountNoQuery()
            {
                AccountNo = accountNo
            }, cancellationToken);
            return result;
        }
    }
}
