using MediatR;

namespace BankA.Application.UseCases.Accounts.FileFormatConfig
{
    public class UpdateFileFormatConfigurationRequest : IRequest<bool>
    {
        public int AccountId { get; set; }
        public string FileFormatConfiguration { get; set; }
    }
}
