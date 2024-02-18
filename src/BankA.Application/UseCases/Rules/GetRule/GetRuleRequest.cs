using MediatR;

namespace BankA.Application.UseCases.Rules.GetRule
{
    public class GetRuleRequest : IRequest<GetRuleResponse>
    {
        public GetRuleRequest(int ruleId)
        {
            RuleId = ruleId;
        }

        public int RuleId { get; set; }
    }
}
