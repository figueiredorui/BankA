using System;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace BankA.Domain
{
    public class Transaction : EntityBase
    {
        public Account Account { get; private set; }
        public DateTime TransactionDate { get; private set; }
        public string TransactionType { get; private set; }
        public string Description { get; private set; }
        public decimal DebitAmount { get; private set; }
        public decimal CreditAmount { get; private set; }

        private int? CategoryId { get; set; }
        public Category Category { get; private set; }

        private int? MerchantId { get; set; }
        public Merchant Merchant { get; private set; }
        public int? FileId { get; private set; }
        public decimal Balance { get { return CreditAmount - DebitAmount; } }

        private Transaction()
        {

        }

        public Transaction(Account account, DateTime transactionDate, string description, decimal balance)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException("description");

            if (account == null)
                throw new ArgumentNullException("account");

            if (balance == 0)
                throw new ArgumentNullException("balance cannot be zero");

            if (transactionDate <= new DateTime(2000, 1, 1))
                throw new ApplicationException($"Invalid date '{transactionDate}'");

            Account = account;
            TransactionDate = transactionDate;
            Description = RemoveDoubleSpaces(description);

            if (balance > 0)
                CreditAmount = Math.Abs(balance);

            if (balance < 0)
                DebitAmount = Math.Abs(balance);
        }

        public Transaction(Account account, DateTime transactionDate, string description, decimal creditAmount, decimal debitAmount, int categoryId, int merchantId)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException("description");

            if (account == null)
                throw new ArgumentNullException("account");

            Account = account;
            TransactionDate = transactionDate;
            Description = RemoveDoubleSpaces(description);

            CreditAmount = creditAmount;

            DebitAmount = debitAmount;

            CategoryId = categoryId;
            MerchantId = merchantId;

        }

        public void Update(DateTime transactionDate, string description, decimal balance)
        {
            if (transactionDate <= new DateTime(2000, 1, 1))
                throw new ApplicationException($"Invalid date '{transactionDate}'");

            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException("description");

            if (balance == 0)
                throw new ArgumentNullException("balance cannot be zero");

            TransactionDate = transactionDate;
            Description = RemoveDoubleSpaces(description);

            if (balance > 0)
            {
                CreditAmount = Math.Abs(balance);
                DebitAmount = 0;
            }

            if (balance < 0)
            {
                DebitAmount = Math.Abs(balance);
                CreditAmount = 0;
            }
        }


        public void ApplyRule(Rule rule)
        {
            if (rule == null)
                throw new ArgumentNullException("rule");

            UpdateCategory(rule.Category);

            if (rule.Merchant != null)
            {
                MerchantId = rule.Merchant.Id;
            }
            else
            {
                Merchant = null;
            }
        }



        public void ClearCategory()
        {
            Category = null;
        }

        private string RemoveDoubleSpaces(string input)
        {
            var result = Regex.Replace(input, @"\s+", " "); ;
            return result;
        }

        public void ClearMerchant()
        {
            Merchant = null;
        }

        public void UpdateMerchant(Merchant merchant)
        {
            if (MerchantId != merchant?.Id)
            {
                MerchantId = merchant.Id;
            }

        }

        public void UpdateCategory(Category category)
        {
            if (category == null)
                Category = null;

            CategoryId = category.Id;
        }


    }
}
