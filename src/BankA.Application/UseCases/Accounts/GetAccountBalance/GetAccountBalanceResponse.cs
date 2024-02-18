using System;
using System.Collections.Generic;

namespace BankA.Application.UseCases.Accounts.GetAccountBalance
{
    public class GetAccountBalanceResponse
    {
        public GetAccountBalanceResponse(List<AccountBalanceModel> data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public List<AccountBalanceModel> Data { get; set; }
    }
}
