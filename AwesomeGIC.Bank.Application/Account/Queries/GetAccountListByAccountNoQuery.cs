using AutoMapper;
using AwesomeGIC.Bank.Application.Common;
using Microsoft.EntityFrameworkCore;
using System;

namespace AwesomeGIC.Bank.Application.Account.Queries
{
    public class GetAccountListByAccountNoQuery : BaseRequest<AccountDto>
    {
        public string AccountNo { get; set; }

        public class GetActionByIdQueryHandler : BaseRequestHandler<GetAccountListByAccountNoQuery, AccountDto>
        {
            private readonly IMapper _mapper;

            public GetActionByIdQueryHandler(IAwesomeGICBankDBContext context
                , IMapper mapper) : base(context)
            {
                _mapper = mapper;
            }

            public override async Task<AccountDto> Handle(GetAccountListByAccountNoQuery request
                , CancellationToken cancellationToken)
            {
                try
                {
                    var account = await _context.Accounts.AsNoTracking()
                        .Include(e => e.Transactions)
                        .Where(w => w.AccountNo == request.AccountNo)
                        .FirstOrDefaultAsync(cancellationToken);

                    var dto = new AccountDto();
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
