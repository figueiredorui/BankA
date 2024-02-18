using BankA.Domain.Enums;

namespace BankA.Application.Interfaces
{
    public interface IStatementFileParserFactory
    {
        IStatementFileParser Create(StatementFileFormatEnum accountFileFormat);
    }
}
