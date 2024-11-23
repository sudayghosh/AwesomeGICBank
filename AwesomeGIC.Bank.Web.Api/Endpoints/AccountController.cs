using AwesomeGIC.Bank.Application.Account.Commands;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountReqDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        //[ApiKey]
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpsertAccount([FromBody] AccountReqDto accountReqDto
            , CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _sender.Send(new UpsertAccountCommand()
            {
                Dto = accountReqDto
            }, cancellationToken);
            return Ok(result);
        }


        //[ApiKey]
        [HttpPost("transactions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<AccountRespDto> GetTransactionsByAccountNo([FromBody] AccountTransactionReqDto dto
            , CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetTransactionsStatementQuery()
            {
                Dto = dto
            }, cancellationToken);
            return result;
        }
    }
}
