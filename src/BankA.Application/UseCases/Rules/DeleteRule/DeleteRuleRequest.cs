using MediatR;

namespace BankA.Application.UseCases.Rules.DeleteRule
{
    public class DeleteRuleRequest : IRequest<bool>
    {
        public DeleteRuleRequest(int ruleId)
        {
            RuleId = ruleId;
        }

        public int RuleId { get; set; }
    }
}
