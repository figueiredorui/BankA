using BankA.Application.Interfaces;
using BankA.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Rules.GetRule
{
    class GetRuleRequestHandler : IRequestHandler<GetRuleRequest, GetRuleResponse>
    {
        private readonly IAppDataDbContext _bankADbContext;

        public GetRuleRequestHandler(IAppDataDbContext bankADbContext)
        {
            _bankADbContext = bankADbContext;
        }

        public async Task<GetRuleResponse> Handle(GetRuleRequest request, CancellationToken cancellationToken)
        {
            var rule = await GetRule(request.RuleId);

            return new GetRuleResponse()
            {
                RuleId = rule.Id,
                Keywords = rule.Keywords,
                CategoryId = rule.Category.Id,
                CategoryFullName = rule.Category.CategoryFullName,
                MerchantId = rule.Merchant.Id,
                MerchantName = rule.Merchant.MerchantName,
            };
        }

        private async Task<Rule> GetRule(int ruleId)
        {
            var rule = await _bankADbContext.Rules.Include(i => i.Category).ThenInclude(i => i.Parent).Include(i => i.Merchant).Where(q => q.Id == ruleId).FirstOrDefaultAsync();
            if (rule == null)
                throw new ApplicationException($"Rule '{ruleId}' not found");
            return rule;
        }
    }
}
