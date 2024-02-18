using System;

namespace BankA.Domain
{
    public class TransactionView
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public decimal Balance { get; set; }
        public decimal RunningBalance { get; set; }
        public string CategoryType { get; set; }
        public int? CategoryId { get; set; }
        public int? CategoryParentId { get; set; }
        public string CategoryName { get; set; }
        public int? MerchantId { get; set; }
        public string MerchantName { get; set; }
    }
}
