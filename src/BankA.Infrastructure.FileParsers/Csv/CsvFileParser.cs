using BankA.Application.Interfaces;
using BankA.Domain;
using BankA.Domain.Enums;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;

namespace BankA.Infrastructure.FileParsers.Csv
{
    [StatementFileFormat(StatementFileFormatEnum.CSV)]
    public class CsvFileParser : IStatementFileParser
    {



        public List<Transaction> Parse(Account account, byte[] fileContent)
        {
            try
            {
                var lst = new List<Transaction>();

                var options = new TypeConverterOptions { Formats = new[] { "dd/MM/yyyy", "yyyy-MM-dd" } };

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                    Mode = CsvMode.RFC4180,
                    TrimOptions = CsvHelper.Configuration.TrimOptions.Trim

                };

                if (account.FileFormatConfiguration.HasHeaders)
                {
                    config.HasHeaderRecord = true;
                }

                using (var csv = InitReader(config, fileContent))
                {
                    csv.Context.TypeConverterOptionsCache.AddOptions<DateTime>(options);

                    if (account.FileFormatConfiguration.HasHeaders)
                    {
                        csv.Read();
                        csv.ReadHeader();
                    }

                    while (csv.Read())
                    {

                        var date = csv.GetField<DateTime>(account.FileFormatConfiguration.Date_ColumnIndex);
                        var type = ParseTransactionType(csv, account.FileFormatConfiguration.Type_ColumnIndex);
                        var description = csv.GetField<string>(account.FileFormatConfiguration.Description_ColumnIndex);

                        decimal amount = 0;
                        if (account.FileFormatConfiguration.Amount_ColumnIndex > -1)
                        {
                            amount = ParseAmount(csv, account.FileFormatConfiguration.Amount_ColumnIndex);
                        }
                        else
                        {
                            var creditAmount = ParseAmount(csv, account.FileFormatConfiguration.CreditAmount_ColumnIndex);
                            var debitAmount = ParseAmount(csv, account.FileFormatConfiguration.DebitAmount_ColumnIndex);

                            if (creditAmount > debitAmount)
                                amount = creditAmount;
                            else
                                amount = -debitAmount;
                        }

                        var transaction = new Transaction(account, date, description, amount);
                        lst.Add(transaction);
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private string ParseTransactionType(CsvReader csv, int index)
        {
            var str = "";

            if (index > -1)
                str = csv.GetField(index);

            return str;
        }

        private decimal ParseAmount(CsvReader csv, int index)
        {
            var str = csv.GetField(index);
            str = str.Trim('"');
            str = str.Trim('\'');

            decimal.TryParse(str, out decimal amount);
            return amount;
        }


        private CsvReader InitReader(CsvConfiguration config, byte[] fileContent)
        {

            var reader = new StreamReader(new MemoryStream(fileContent));
            var csv = new CsvReader(reader, config);

            return csv;
        }
    }
}
