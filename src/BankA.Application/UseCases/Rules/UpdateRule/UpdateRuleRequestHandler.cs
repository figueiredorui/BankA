using BankA.Application.Interfaces;
using BankA.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Rules.UpdateRule
{
    class UpdateRuleRequestHandler : IRequestHandler<UpdateRuleRequest, bool>
    {
        private readonly IAppDataDbContext _bankADbContext;

        public UpdateRuleRequestHandler(IAppDataDbContext bankADbContext)
        {
            _bankADbContext = bankADbContext;
        }

        public async Task<bool> Handle(UpdateRuleRequest request, CancellationToken cancellationToken)
        {
            var merchant = await GetOrCreateMerchant(request.MerchantName);
            var category = await GetCategory(request.CategoryId);

            var rule = await GetRule(request.RuleId);
            rule.Update(request.Keywords, category, merchant);

            await _bankADbContext.SaveChangesAsync();

            return true;
        }
        private async Task<Rule> GetRule(int ruleId)
        {
            var rule = await _bankADbContext.Rules.Where(q => q.Id == ruleId).FirstOrDefaultAsync();
            if (rule == null)
                throw new ApplicationException($"Rule '{ruleId}' not found");
            return rule;
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
