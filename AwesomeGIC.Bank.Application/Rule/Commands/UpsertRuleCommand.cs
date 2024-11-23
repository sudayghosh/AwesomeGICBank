using AutoMapper;
using AwesomeGIC.Bank.Application.Account.Queries;
using AwesomeGIC.Bank.Application.Common;
using AwesomeGIC.Bank.Application.Rule.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;

namespace AwesomeGIC.Bank.Application.Rule.Commands
{
    public class UpsertRuleCommand : BaseRequest<List<InterestRuleListDto>>
    {
        public RuleReqDto Dto { get; set; }

        public class UpsertRuleCommandHandler : BaseRequestHandler<UpsertRuleCommand, List<InterestRuleListDto>>
        {
            private readonly IMapper _mapper;
            private readonly ISender _sender;

            public UpsertRuleCommandHandler(IAwesomeGICBankDBContext context
                , IMapper mapper
                , ISender sender) : base(context)
            {
                _mapper = mapper;
                _sender = sender;
            }

            public override async Task<List<InterestRuleListDto>> Handle(UpsertRuleCommand request
                , CancellationToken cancellationToken)
            {
                try
                {
                    DateTime dt = DateTime.ParseExact(request.Dto.DateTime, "yyyyMMdd", CultureInfo.InvariantCulture);
                    var rule = await _context.InterestRules
                        .FirstOrDefaultAsync(a => a.DateTime == dt, cancellationToken);
                    bool addingNew = rule != null;
                    if (rule != null)
                    {
                        _mapper.Map(request.Dto, rule);
                    }
                    else
                    {
                        rule = new Domain.Entities.InterestRule();
                        _mapper.Map(request.Dto, rule);

                        await _context.InterestRules.AddAsync(rule);
                    }

                    var noEntities = await _context.SaveChangesAsync(cancellationToken);

                    var result = await _sender.Send(new GetRulesQuery()
                    {
                    }, cancellationToken);
                    return result;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

        }
    }
}
