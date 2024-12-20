﻿using AwesomeGIC.Bank.UI.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeGIC.Bank.UI.Service
{
    public interface IAccountService
    {
        Task<AccountRespDto> UpsertAccount(AccountReqDto reqDto);

        Task<AccountRespDto?> GetAccountStatement(AccountStatementReqDto reqDto);
    }
}
