using BankA.Application.Interfaces;
using BankA.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Transactions.UpdateTransaction
{
    class UpdateTransactionRequestHandler : IRequestHandler<UpdateTransactionRequest, bool>
    {
        private readonly IAppDataDbContext _bankADbContext;

        public UpdateTransactionRequestHandler(IAppDataDbContext bankADbContext)
        {
            _bankADbContext = bankADbContext;
        }

        public async Task<bool> Handle(UpdateTransactionRequest request, CancellationToken cancellationToken)
        {
            var transaction = await GetTransaction(request.TransactionId);

            transaction.Update(request.TransactionDate, request.Description, request.Balance);

            if (request.CategoryId.HasValue && request.CategoryId > 0)
            {
                var category = await GetCategory(request.CategoryId.Value);

                transaction.UpdateCategory(category);
            }
            else
            {
                transaction.ClearCategory();
            }

            if (request.MerchantId.HasValue && request.MerchantId > 0)
            {
                var merchant = await GetMerchant(request.MerchantId.Value);

                transaction.UpdateMerchant(merchant);
            }
            else if (!string.IsNullOrEmpty(request.MerchantName))
            {
                var merchant = await GetOrCreateMerchant(request.MerchantName);
                transaction.UpdateMerchant(merchant);
            }
            else if (request.MerchantId == null)
            {
                transaction.ClearMerchant();
            }

            await _bankADbContext.SaveChangesAsync();

            return true;
        }

        private async Task<Merchant> GetMerchant(int merchantId)
        {
            var merchant = await _bankADbContext.Merchants.Where(q => q.Id == merchantId).FirstOrDefaultAsync();
            if (merchant == null)
                throw new ApplicationException($"Merchant '{merchantId}' not found");
            return merchant;
        }

        private async Task<Category> GetCategory(int categoryId)
        {
            var category = await _bankADbContext.GetCategoryById(categoryId);
            if (category == null)
                throw new ApplicationException($"Category '{categoryId}' not found");
            return category;
        }

        private async Task<Transaction> GetTransaction(int transactionId)
        {
            var transaction = await _bankADbContext.GetTransactionById(transactionId);
            if (transaction == null)
                throw new ApplicationException($"Transaction '{transactionId}' not found");
            return transaction;
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
