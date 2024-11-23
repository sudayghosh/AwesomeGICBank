using AutoMapper;
using AwesomeGIC.Bank.Application.Common;
using Microsoft.EntityFrameworkCore;
using System;

namespace AwesomeGIC.Bank.Application.Rule.Queries
{
    public class GetRulesQuery : BaseRequest<List<InterestRuleListDto>>
    {
        public class GetRulesQueryHandler : BaseRequestHandler<GetRulesQuery, List<InterestRuleListDto>>
        {
            private readonly IMapper _mapper;

            public GetRulesQueryHandler(IAwesomeGICBankDBContext context
                , IMapper mapper) : base(context)
            {
                _mapper = mapper;
            }

            public override async Task<List<InterestRuleListDto>> Handle(GetRulesQuery request
                , CancellationToken cancellationToken)
            {
                try
                {
                    var rules = await _context.InterestRules.AsNoTracking()
                        .ToListAsync(cancellationToken);

                    var dto = new List<InterestRuleListDto>();
                    _mapper.Map(rules, dto);
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
