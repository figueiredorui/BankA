using System;
using System.Collections.Generic;

namespace BankA.Domain.ValueObjects
{
    public class Money : ValueObject
    {
        public string Currency { get; private set; }
        public decimal Amount { get; private set; }

        public Money(decimal amount, string currency)
        {
            Currency = currency;
            Amount = amount;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Currency.ToUpper();
            yield return Math.Round(Amount, 2);
        }
    }
}
