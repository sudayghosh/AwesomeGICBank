﻿using AwesomeGIC.Bank.Application.Account.Commands;
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
        public async Task<AccountRespDto> UpsertAccount([FromBody] AccountReqDto accountReqDto
            , CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new UpsertAccountCommand()
            {
                Dto = accountReqDto
            }, cancellationToken);
            return result;
        }
    }
}
