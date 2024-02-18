using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace BankA.Domain.ValueObjects
{
    public class FileFormatConfiguration : ValueObject
    {
        string[] DelimiterOptions = new string[2] { ",", ";" };
        public bool HasHeaders { get; set; }
        public string Delimiter { get; set; }
        public int Date_ColumnIndex { get; set; }
        public int Type_ColumnIndex { get; set; }
        public int Description_ColumnIndex { get; set; }
        public int DebitAmount_ColumnIndex { get; set; }
        public int CreditAmount_ColumnIndex { get; set; }
        public int Amount_ColumnIndex { get; set; }

        public FileFormatConfiguration() { }
        public FileFormatConfiguration(string json)
        {
            if (json == null)
                return;

            var obj = JsonSerializer.Deserialize<FileFormatConfiguration>(json);

            if (string.IsNullOrEmpty(obj.Delimiter))
                throw new Exception("Delimiter is required");

            if (!DelimiterOptions.Contains(obj.Delimiter))
                throw new Exception($"Invalid Delimiter '{obj.Delimiter}'");

            if (obj.Date_ColumnIndex < 0)
                throw new Exception("TransactionDate mapping is required");

            if (obj.Description_ColumnIndex < 0)
                throw new Exception("Description mapping is required");

            if (obj.Amount_ColumnIndex < 0 && obj.CreditAmount_ColumnIndex < 0 && obj.DebitAmount_ColumnIndex < 0)
                throw new Exception("Amount or CreditAmount/DebitAmount mapping is required");

            HasHeaders = obj.HasHeaders;
            Date_ColumnIndex = obj.Date_ColumnIndex;
            Type_ColumnIndex = obj.Type_ColumnIndex;
            Description_ColumnIndex = obj.Description_ColumnIndex;
            DebitAmount_ColumnIndex = obj.DebitAmount_ColumnIndex;
            CreditAmount_ColumnIndex = obj.CreditAmount_ColumnIndex;
            Amount_ColumnIndex = obj.Amount_ColumnIndex;
            Delimiter = obj.Delimiter;

        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return HasHeaders;
            yield return Date_ColumnIndex;
            yield return Type_ColumnIndex;
            yield return Description_ColumnIndex;
            yield return DebitAmount_ColumnIndex;
            yield return CreditAmount_ColumnIndex;
            yield return Amount_ColumnIndex;
            yield return Delimiter;

        }
    }
}
