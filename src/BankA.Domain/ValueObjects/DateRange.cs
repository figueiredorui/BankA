using System;
using System.Collections.Generic;

namespace BankA.Domain.ValueObjects
{
    public class DateRange : ValueObject
    {
        public DateTime StartDate { get; private set; }

        public DateTime EndDate { get; private set; }

        public DateRange(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new ApplicationException("startDate cannot be greater than endDate");

            StartDate = startDate;
            EndDate = endDate;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.StartDate;
            yield return this.EndDate;
        }
    }
}
