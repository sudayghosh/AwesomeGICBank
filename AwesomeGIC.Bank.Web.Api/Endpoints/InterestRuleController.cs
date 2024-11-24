using AwesomeGIC.Bank.Application.Rule.Commands;
using AwesomeGIC.Bank.Application.Rule.Queries;
using AwesomeGIC.Bank.Infrastructure.Filters;
using AwesomeGIC.Bank.Web.Api.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeGIC.Bank.Web.Api.Endpoints
{
    /// <summary>
    /// The inerest rule api which are providing upsert interest rule and interest rules
    /// </summary>
    [ApiKey]
    [Route("interestrule")]
    public class InterestRuleController : BaseController
    {
        public InterestRuleController(ISender sender) : base(sender)
        {
        }

        /// <summary>
        /// Displaying all interest rules
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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
        /// Upsert Interest Rules
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
