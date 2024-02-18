using MediatR;

namespace BankA.Application.UseCases.Transactions.ImportTransactionsFile
{
    public class ImportTransactionsFileRequest : IRequest<bool>
    {
        public int AccountId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileContent { get; set; }
    }
}
