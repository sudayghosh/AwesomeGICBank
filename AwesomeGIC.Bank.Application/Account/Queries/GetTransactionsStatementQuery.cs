using AutoMapper;
using AwesomeGIC.Bank.Application.Account.Commands;
using AwesomeGIC.Bank.Application.Common;
using AwesomeGIC.Bank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;

namespace AwesomeGIC.Bank.Application.Account.Queries
{
    public class GetTransactionsStatementQuery : BaseRequest<AccountRespDto>
    {
        public AccountTransactionReqDto Dto { get; set; }

        public class GetTransactionsStatementQueryHandler
            : BaseRequestHandler<GetTransactionsStatementQuery, AccountRespDto>
        {
            private readonly IMapper _mapper;

            public GetTransactionsStatementQueryHandler(IAwesomeGICBankDBContext context
                , IMapper mapper) : base(context)
            {
                _mapper = mapper;
            }

            private async Task ApplyInterestRules(DateTime startDt, DateTime endDt, List<TransactionDto> transactions)
            {
                var interestRules = await _context.InterestRules
                    //.Where(w => w.DateTime >= startDt && w.DateTime <= endDt)
                    .GroupBy(s => new { s.RuleId, s.DateTime })
                    .Select(s => new
                    {
                        s.Key.RuleId,
                        s.Key.DateTime,
                        Rate = s.Max(m => m.Rate)
                    })
                    .OrderBy(o => o.DateTime)
                    .ToListAsync();

                decimal interest = 0m;
                foreach (var transaction in transactions)
                {
                    var interestRule = interestRules.Where(w => w.DateTime <= transaction.TxnDate).FirstOrDefault();
                    if (interestRule != null)
                    {
                        interest += transaction.Balance * (interestRule.Rate / 100);
                    }
                }
                interest /= 365;

                var lastBal = transactions.OrderByDescending(o => o.TxnDate).First().Balance + interest;
                transactions.Add(new TransactionDto
                {
                    Type = nameof(TransactionType.I),
                    Amount = interest,
                    Balance = lastBal,
                    TxnDate = endDt,
                    TxnId = "        ",
                });
            }

            public override async Task<AccountRespDto> Handle(GetTransactionsStatementQuery request
                , CancellationToken cancellationToken)
            {
                try
                {
                    DateTime startDt = DateTime.ParseExact($"{request.Dto.TxnDate}01", "yyyyMMdd", CultureInfo.InvariantCulture);
                    DateTime endDt = startDt.AddMonths(1).AddDays(-1);

                    var account = await _context.Accounts.AsNoTracking()
                        .Include(e => e.Transactions.OrderBy(a => a.CreateDateTime))
                        .Where(w => w.AccountNo == request.Dto.AccountNo
                            && w.Transactions.Any(w => w.TxnDate >= startDt && w.TxnDate <= endDt))
                        .FirstOrDefaultAsync(cancellationToken);

                    var dto = new AccountRespDto();
                    _mapper.Map(account, dto);

                    if (dto.Transactions != null)
                    {
                        var trans = dto.Transactions.ToList();
                        await ApplyInterestRules(startDt, endDt, trans);
                        foreach (var transaction in trans)
                        {
                            transaction.TxnDate = DateTime.SpecifyKind(transaction.TxnDate, DateTimeKind.Utc);
                        }
                        dto.Transactions = trans;
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
