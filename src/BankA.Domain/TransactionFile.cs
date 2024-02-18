using System.Collections.Generic;

namespace BankA.Domain
{
    public class TransactionFile : EntityBase
    {
        public Account Account { get; private set; }
        public string FileName { get; private set; }
        public byte[] FileContent { get; private set; }
        public string ContentType { get; private set; }

        public ICollection<Transaction> Transactions { get; private set; }

        private TransactionFile()
        {
        }

        public TransactionFile(Account account, string fileName, string contentType, byte[] fileContent, List<Transaction> transactionList)
        {
            Account = account;
            FileContent = fileContent;
            ContentType = contentType;
            FileName = fileName;
            Transactions = new List<Transaction>();

            foreach (var transaction in transactionList)
            {
                Transactions.Add(transaction);
            }
        }
    }
}
