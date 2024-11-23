using AutoMapper;
using AwesomeGIC.Bank.Application.Account.Commands;
using AwesomeGIC.Bank.Application.Account.Queries;
using AwesomeGIC.Bank.Application.Rule.Commands;
using AwesomeGIC.Bank.Application.Rule.Queries;
using AwesomeGIC.Bank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeGIC.Bank.Infrastructure.Sql
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Domain.Entities.Account, AccountRespDto>();

            CreateMap<Domain.Entities.Transaction, TransactionDto>();

            CreateMap<AccountReqDto, Domain.Entities.Account>();

            CreateMap<AccountReqDto, Domain.Entities.Transaction>()
                .ForMember(m => m.TxnDate, m => m.MapFrom(c => DateTime.ParseExact(c.TxnDate, "yyyyMMdd", CultureInfo.InvariantCulture)));

            CreateMap<RuleReqDto, Domain.Entities.InterestRule>()
                .ForMember(m => m.DateTime, m => m.MapFrom(c => DateTime.ParseExact(c.DateTime, "yyyyMMdd", CultureInfo.InvariantCulture)));

            CreateMap<Domain.Entities.InterestRule, InterestRuleListDto>();
        }
    }
}
