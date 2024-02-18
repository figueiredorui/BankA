using BankA.Application.Interfaces;
using BankA.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Merchants.DeleteMerchant
{
    class DeleteMerchantRequestHandler : IRequestHandler<DeleteMerchantRequest, bool>
    {
        private readonly IAppDataDbContext _bankADbContext;

        public DeleteMerchantRequestHandler(IAppDataDbContext bankADbContext)
        {
            _bankADbContext = bankADbContext;
        }

        public async Task<bool> Handle(DeleteMerchantRequest request, CancellationToken cancellationToken)
        {
            var merchant = await GetMerchant(request.MerchantId);

            var transactionList = await GetTransactionsByMerchantId(request.MerchantId);
            foreach (var item in transactionList)
            {
                item.ClearMerchant();
            }

            var rules = await _bankADbContext.Rules.Where(q => q.Merchant.Id == request.MerchantId).ToListAsync();
            foreach (var item in rules)
            {
                item.ClearMerchant();
            }

            await _bankADbContext.SaveChangesAsync();

            await DeleteMerchant(merchant);

            return true;
        }

        private async Task<Merchant> GetMerchant(int merchantId)
        {
            var merchant = await _bankADbContext.Merchants.Where(q => q.Id == merchantId).FirstOrDefaultAsync();
            if (merchant == null)
                throw new ApplicationException($"Merchant '{merchantId}' not found");
            return merchant;
        }

        private async Task<List<Transaction>> GetTransactionsByMerchantId(int merchantId)
        {
            return await _bankADbContext.Transactions.Include(i => i.Account)
                        .Where(q => q.Merchant.Id == merchantId).ToListAsync();
        }

        private async Task DeleteMerchant(Merchant merchant)
        {
            _bankADbContext.Merchants.Remove(merchant);
            await _bankADbContext.SaveChangesAsync();
        }
    }
}
