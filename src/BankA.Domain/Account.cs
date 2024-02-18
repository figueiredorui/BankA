using BankA.Domain.Enums;
using BankA.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankA.Domain
{
    public class Account : EntityBase
    {
        public string Description { get; private set; }
        public StatementFileFormatEnum FileFormat { get; private set; }

        public FileFormatConfiguration FileFormatConfiguration { get; private set; }

        public bool Closed { get; private set; }

        private Account()
        {
        }

        public Account(string description, string fileFormat)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException("description");

            Description = description;
            FileFormat = ParseAccountFileFormat(fileFormat);
            Closed = false;
        }

        public void UpdateAccount(string description, string fileFormat)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException("description");

            Description = description;
            FileFormat = ParseAccountFileFormat(fileFormat);
        }

        public void Close()
        {
            this.Closed = true;
        }
        public void Open()
        {
            this.Closed = false;
        }

        public void UpdateFileFormatConfig(string fileFormatConfig)
        {
            if (FileFormat == StatementFileFormatEnum.CSV)
            {
                if (string.IsNullOrEmpty(fileFormatConfig))
                {
                    FileFormatConfiguration = null;
                }
                else
                {
                    FileFormatConfiguration = new FileFormatConfiguration(fileFormatConfig);
                }
            }
            else
            {
                throw new ApplicationException($"File Format Config is only required for CSV format");
            }

        }



        private StatementFileFormatEnum ParseAccountFileFormat(string fileFormat)
        {
            try
            {
                var fileFormatEnum = (StatementFileFormatEnum)Enum.Parse(typeof(StatementFileFormatEnum), fileFormat, true);
                return fileFormatEnum;
            }
            catch (Exception)
            {
                throw new ApplicationException($"Invalid File Format");
            }
        }

        public static List<string> GetFileFormats()
        {
            var result = Enum.GetNames(typeof(StatementFileFormatEnum)).OrderBy(s => s).ToList();
            return result;
        }


    }
}
