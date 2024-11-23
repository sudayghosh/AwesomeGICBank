//using AwesomeGIC.Bank.Application.Account.Queries;
//using AwesomeGIC.Bank.Web.Api.Common;
//using MediatR;
//using Microsoft.AspNetCore.Mvc;

//namespace AwesomeGIC.Bank.Web.Api.Endpoints
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    [Route("transaction")]
//    public class TransactionController : BaseController
//    {
//        public TransactionController(ISender sender) : base(sender) 
//        {            
//        }

//        //[ApiKey]
//        [HttpPost("{accountNo}/list")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesDefaultResponseType]
//        public async Task<AccountRespDto> AccountListByAccountNo(string accountNo
//            , CancellationToken cancellationToken)
//        {
//            var result = await _sender.Send(new GetTransactionListByAccountNoQuery()
//            {
//                AccountNo = accountNo
//            }, cancellationToken);
//            return result;
//        }
//    }
//}
