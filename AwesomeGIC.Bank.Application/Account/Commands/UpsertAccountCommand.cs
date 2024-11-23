using AutoMapper;
using AwesomeGIC.Bank.Application.Account.Queries;
using AwesomeGIC.Bank.Application.Common;
using AwesomeGIC.Bank.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;

namespace AwesomeGIC.Bank.Application.Account.Commands
{
    public class UpsertAccountCommand : BaseRequest<AccountRespDto>
    {
        public AccountReqDto Dto { get; set; }

        public class UpsertAccountCommandHandler : BaseRequestHandler<UpsertAccountCommand, AccountRespDto>
        {
            private readonly IMapper _mapper;
            private readonly ISender _sender;

            public UpsertAccountCommandHandler(IAwesomeGICBankDBContext context
                , IMapper mapper
                , ISender sender) : base(context)
            {
                _mapper = mapper;
                _sender = sender;
            }

            public override async Task<AccountRespDto> Handle(UpsertAccountCommand request
                , CancellationToken cancellationToken)
            {
                try
                {
                    var account = await _context.Accounts
                        .Include(e => e.Transactions.OrderByDescending(o => o.CreateDateTime).Take(1))
                        .FirstOrDefaultAsync(a => a.AccountNo == request.Dto.AccountNo, cancellationToken);
                    bool addingNew = account != null;
                    if (account != null)
                    {
                        await AddTransaction(request.Dto, account, addingNew);
                    }
                    else
                    {
                        account = new Domain.Entities.Account();
                        _mapper.Map(request.Dto, account);

                        await AddTransaction(request.Dto, account, addingNew);

                        await _context.Accounts.AddAsync(account);
                    }

                    var noEntities = await _context.SaveChangesAsync(cancellationToken);

                    var result = await _sender.Send(new GetTransactionListByAccountNoQuery()
                    {
                        AccountNo = request.Dto.AccountNo
                    }, cancellationToken);
                    return result;

                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            private async Task AddTransaction(AccountReqDto dto, Domain.Entities.Account account, bool addingNew)
            {
                var transDt = DateTime.ParseExact(dto.TxnDate, "yyyyMMdd", CultureInfo.InvariantCulture);
                var transCount = await _context.Transactions.AsNoTracking()
                    .CountAsync(a => a.TxnDate == transDt);

                var trans = new Transaction();
                _mapper.Map(dto, trans);
                trans.TxnId = $"{dto.TxnDate}-{transCount + 1}";
                await CalcBalance(account, trans, addingNew);
                account.Transactions.Add(trans);
            }

            private async Task CalcBalance(Domain.Entities.Account account, Transaction trans, bool addingNew)
            {
                trans.Balance = trans.Amount;
                if (account.Transactions.FirstOrDefault() != null)
                {
                    if (trans.Type == TransactionType.D.ToString())
                    {
                        trans.Balance += account.Transactions.FirstOrDefault().Balance;
                    }
                    else if (trans.Type == TransactionType.W.ToString())
                    {
                        trans.Balance = account.Transactions.FirstOrDefault().Balance - trans.Amount;
                    }
                }
                await Task.CompletedTask;
            }
        }
    }
}
