using BankA.Application.Interfaces;
using BankA.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Rules.DeleteRule
{
    class DeleteRuleRequestHandler : IRequestHandler<DeleteRuleRequest, bool>
    {
        private readonly IAppDataDbContext _bankADbContext;

        public DeleteRuleRequestHandler(IAppDataDbContext bankADbContext)
        {
            _bankADbContext = bankADbContext;
        }

        public async Task<bool> Handle(DeleteRuleRequest request, CancellationToken cancellationToken)
        {
            var merchant = await GetRule(request.RuleId);
            await DeleteRule(merchant);

            return true;
        }

        private async Task<Rule> GetRule(int ruleId)
        {
            var merchant = await _bankADbContext.Rules.Where(q => q.Id == ruleId).FirstOrDefaultAsync();
            if (merchant == null)
                throw new ApplicationException($"Rule '{ruleId}' not found");
            return merchant;
        }

        private async Task DeleteRule(Rule merchant)
        {
            _bankADbContext.Rules.Remove(merchant);
            await _bankADbContext.SaveChangesAsync();
        }
    }
}
