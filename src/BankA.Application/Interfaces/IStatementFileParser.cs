using BankA.Domain;
using System.Collections.Generic;

namespace BankA.Application.Interfaces
{
    public interface IStatementFileParser
    {
        List<Transaction> Parse(Account account, byte[] fileContent);
    }
}
