using BankA.Application.Interfaces;
using BankA.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Rules.CreateRule
{
    class CreateRuleRequestHandler : IRequestHandler<CreateRuleRequest, CreateRuleResponse>
    {
        private readonly IAppDataDbContext _bankADbContext;

        public CreateRuleRequestHandler(IAppDataDbContext bankADbContext)
        {
            _bankADbContext = bankADbContext;
        }

        public async Task<CreateRuleResponse> Handle(CreateRuleRequest request, CancellationToken cancellationToken)
        {
            var merchant = await GetOrCreateMerchant(request.MerchantName);
            var category = await GetCategory(request.CategoryId);

            var rule = new Rule(request.Keywords, category, merchant);

            await _bankADbContext.Rules.AddAsync(rule);
            await _bankADbContext.SaveChangesAsync();

            return new CreateRuleResponse() { Id = rule.Id };
        }

        private async Task<Category> GetCategory(int categoryId)
        {
            var category = await _bankADbContext.Categories.Where(q => q.Id == categoryId).FirstOrDefaultAsync();
            if (category == null)
                throw new ApplicationException($"Category '{categoryId}' not found");
            return category;
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
