using AutoMapper;
using AwesomeGIC.Bank.Application.Common;
using Microsoft.EntityFrameworkCore;
using System;

namespace AwesomeGIC.Bank.Application.Account.Queries
{
    public class GetTransactionListByAccountNoQuery : BaseRequest<AccountRespDto>
    {
        public string AccountNo { get; set; }

        public class GetTransactionListByAccountNoQueryHandler
            : BaseRequestHandler<GetTransactionListByAccountNoQuery, AccountRespDto>
        {
            private readonly IMapper _mapper;

            public GetTransactionListByAccountNoQueryHandler(IAwesomeGICBankDBContext context
                , IMapper mapper) : base(context)
            {
                _mapper = mapper;
            }

            public override async Task<AccountRespDto> Handle(GetTransactionListByAccountNoQuery request
                , CancellationToken cancellationToken)
            {
                try
                {
                    var account = await _context.Accounts.AsNoTracking()
                        .Include(e => e.Transactions)
                        .Where(w => w.AccountNo == request.AccountNo)
                        .FirstOrDefaultAsync(cancellationToken);

                    var dto = new AccountRespDto();
                    _mapper.Map(account, dto);

                    foreach (var transaction in dto.Transactions)
                    {
                        transaction.TxnDate = DateTime.SpecifyKind(transaction.TxnDate, DateTimeKind.Utc);
                    }
                    return dto;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
