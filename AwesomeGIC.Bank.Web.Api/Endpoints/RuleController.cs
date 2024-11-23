using AwesomeGIC.Bank.Application.Account.Commands;
using AwesomeGIC.Bank.Application.Account.Queries;
using AwesomeGIC.Bank.Application.Rule.Commands;
using AwesomeGIC.Bank.Application.Rule.Queries;
using AwesomeGIC.Bank.Web.Api.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeGIC.Bank.Web.Api.Endpoints
{
    [Route("rule")]
    public class RuleController : BaseController
    {
        public RuleController(ISender sender) : base(sender)
        {
        }

        //[ApiKey]
        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<List<InterestRuleListDto>> GetAllRules(CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetRulesQuery()
            {
            }, cancellationToken);
            return result;
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
        public async Task<List<InterestRuleListDto>> UpsertRule([FromBody] RuleReqDto ruleReqDto
            , CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new UpsertRuleCommand()
            {
                Dto = ruleReqDto
            }, cancellationToken);
            return result;
        }
    }
}
