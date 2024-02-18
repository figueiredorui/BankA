using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace BankA.Application.UseCases.Transactions.UpdateTransaction
{
    public class UpdateTransactionRequest : IRequest<bool>
    {
        [Required]
        public int TransactionId { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Balance { get; set; }

        public int? CategoryId { get; set; }
        public int? MerchantId { get; set; }
        public string MerchantName { get; set; }
        public int AccountId { get; set; }
    }
}
