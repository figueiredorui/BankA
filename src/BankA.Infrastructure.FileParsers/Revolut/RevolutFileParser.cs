using BankA.Application.Interfaces;
using BankA.Domain;
using BankA.Domain.Enums;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace BankA.Infrastructure.FileParsers.Revolut
{
    [StatementFileFormat(StatementFileFormatEnum.REVOLUT)]
    public class RevolutFileParser : IStatementFileParser
    {
        public List<Transaction> Parse(Account account, byte[] fileContent)
        {
            try
            {
                var lst = new List<Transaction>();

                using (var csv = InitReader(fileContent))
                {
                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        var balance = 0m;
                        if (csv.GetField(2).Length > 0)
                            balance = Convert.ToDecimal(csv.GetField(2));

                        if (csv.GetField(3).Length > 0)
                            balance = Convert.ToDecimal(csv.GetField(3));


                        var transaction = new Transaction(account,
                                                        csv.GetField<DateTime>(0),
                                                        csv.GetField<string>(1),
                                                        balance
                                                        );

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

        private CsvReader InitReader(byte[] fileContent)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Mode = CsvMode.RFC4180,
                Delimiter = ";",
                TrimOptions = CsvHelper.Configuration.TrimOptions.Trim
            };

            var reader = new StreamReader(new MemoryStream(fileContent));
            var csv = new CsvReader(reader, config);

            return csv;
        }
    }
}
