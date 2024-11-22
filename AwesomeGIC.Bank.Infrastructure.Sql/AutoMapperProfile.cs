using AutoMapper;
using AwesomeGIC.Bank.Application.Account.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeGIC.Bank.Infrastructure.Sql
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Domain.Entities.Account, AccountDto>();

            CreateMap<Domain.Entities.Transaction, TransactionDto>();
        }
    }
}
