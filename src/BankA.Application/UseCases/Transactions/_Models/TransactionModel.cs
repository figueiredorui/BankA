using BankA.Domain;
using System;

namespace BankA.Application.UseCases.Transactions.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string AccountDescription { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public string Description { get; set; }
        public TransactionCategoryModel Category { get; set; }
        public TransactionMerchantModel Merchant { get; set; }
        public decimal Balance { get; set; }

        public TransactionModel(Transaction transaction)
        {
            Id = transaction.Id;
            AccountId = transaction.Account.Id;
            AccountDescription = transaction.Account.Description;
            TransactionDate = transaction.TransactionDate;
            TransactionType = transaction.TransactionType;
            Description = transaction.Description;
            Category = new TransactionCategoryModel(transaction.Category);
            Merchant = new TransactionMerchantModel(transaction.Merchant);
            Balance = transaction.Balance;
        }
    }
}
