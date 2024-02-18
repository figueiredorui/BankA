using BankA.Application.Interfaces;
using BankA.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Transactions.CreateTransaction
{
    class CreateTransactionRequestHandler : IRequestHandler<CreateTransactionRequest, CreateTransactionResponse>
    {
        private readonly IAppDataDbContext _bankADbContext;

        public CreateTransactionRequestHandler(IAppDataDbContext bankADbContext)
        {
            _bankADbContext = bankADbContext;
        }

        public async Task<CreateTransactionResponse> Handle(CreateTransactionRequest request, CancellationToken cancellationToken)
        {
            var account = await _bankADbContext.GetAccountById(request.AccountId);
            if (account == null)
                throw new ApplicationException($"Account '{request.AccountId}' not found");

            var transaction = new Transaction(account, request.TransactionDate, request.Description, request.Balance);

            if (request.CategoryId.HasValue)
            {
                var category = await _bankADbContext.GetCategoryById(request.CategoryId.Value);
                if (category == null)
                    throw new ApplicationException($"Category '{request.CategoryId}' not found");

                transaction.UpdateCategory(category);
            }

            if (request.MerchantId.HasValue && request.MerchantId > 0)
            {
                var merchant = await _bankADbContext.Merchants.Where(q => q.Id == request.MerchantId).FirstOrDefaultAsync();
                if (merchant == null)
                    throw new ApplicationException($"Merchant '{request.MerchantId}' not found");

                transaction.UpdateMerchant(merchant);
            }
            else if (!string.IsNullOrEmpty(request.MerchantName))
            {
                var merchant = await GetOrCreateMerchant(request.MerchantName);
                transaction.UpdateMerchant(merchant);
            }

            _bankADbContext.Transactions.Add(transaction);
            await _bankADbContext.SaveChangesAsync();

            return new CreateTransactionResponse() { Id = transaction.Id };
        }

        private async Task<Merchant> GetOrCreateMerchant(string merchantName)
        {
            merchantName = merchantName.Trim();

            var merchant = await _bankADbContext.Merchants.Where(q => q.MerchantName.ToLower() == merchantName.ToLower()).FirstOrDefaultAsync();
            if (merchant == null)
            {
                merchant = new Merchant(merchantName);
                await _bankADbContext.Merchants.AddAsync(merchant);
                await _bankADbContext.SaveChangesAsync();
            }
            return merchant;
        }
    }
}
