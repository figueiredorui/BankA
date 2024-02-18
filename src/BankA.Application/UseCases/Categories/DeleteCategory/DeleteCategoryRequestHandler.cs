using BankA.Application.Interfaces;
using BankA.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Categories.DeleteCategory
{
    class DeleteCategoryRequestHandler : IRequestHandler<DeleteCategoryRequest, bool>
    {
        private readonly IAppDataDbContext _bankADbContext;

        public DeleteCategoryRequestHandler(IAppDataDbContext bankADbContext)
        {
            _bankADbContext = bankADbContext;
        }

        public async Task<bool> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = await _bankADbContext.GetCategoryById(request.CategoryId);
            if (category == null)
                throw new ApplicationException($"Category '{request.CategoryId}' not found");

            var transactionList = await GetTransactionsByCategoryId(request.CategoryId);
            foreach (var item in transactionList)
            {
                item.ClearCategory();
            }

            _bankADbContext.Categories.Remove(category);
            await _bankADbContext.SaveChangesAsync();
            return true;
        }

        private async Task<List<Transaction>> GetTransactionsByCategoryId(int categoryId)
        {
            return await _bankADbContext.Transactions.Include(i => i.Account)
                        .Where(q => q.Category.Id == categoryId).ToListAsync();
        }
    }
}
