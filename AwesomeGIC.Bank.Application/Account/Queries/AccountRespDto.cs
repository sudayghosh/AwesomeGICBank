﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeGIC.Bank.Application.Account.Queries
{
    public class AccountRespDto
    {
        public string AccountNo { get; set; }

        public IEnumerable<TransactionDto> Transactions { get; set; }
    }
}
