using BankA.Domain.Enums;
using System;

namespace BankA.Infrastructure.FileParsers
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class StatementFileFormatAttribute : Attribute
    {
        private readonly StatementFileFormatEnum _fileFormat;
        public StatementFileFormatAttribute(StatementFileFormatEnum fileFormat)
        {
            _fileFormat = fileFormat;
        }
        public string GetFileFormat()
        {
            return _fileFormat.ToString();
        }
    }
}
