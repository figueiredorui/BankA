
using System;

namespace BankA.Domain
{
    public class AccountBalance
    {
        public decimal CreditAmount { get; private set; }
        public decimal DebitAmount { get; private set; }
        public decimal Balance { get { return CreditAmount - DebitAmount; } }

        public AccountBalance(decimal creditAmount, decimal debitAmount)
        {
            if (creditAmount < 0)
                throw new ApplicationException("The Credit Amount cannot be negative");

            if (debitAmount < 0)
                throw new ApplicationException("The Debit Amount cannot be negative");

            this.CreditAmount = creditAmount;
            this.DebitAmount = debitAmount;
        }
    }
}
