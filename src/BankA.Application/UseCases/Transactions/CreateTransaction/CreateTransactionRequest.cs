using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace BankA.Application.UseCases.Transactions.CreateTransaction
{
    public class CreateTransactionRequest : IRequest<CreateTransactionResponse>
    {
        [Required]
        public int AccountId { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Balance { get; set; }

        public int? CategoryId { get; set; }
        public int? MerchantId { get; set; }
        public string MerchantName { get; set; }
    }
}
