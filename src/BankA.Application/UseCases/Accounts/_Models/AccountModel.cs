using BankA.Domain.ValueObjects;

namespace BankA.Application.UseCases.Accounts
{
    public class AccountModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string FileFormat { get; set; }
        public FileFormatConfiguration FileFormatConfiguration { get; set; }
        public bool Closed { get; set; }
    }
}
