using BankA.Application.Models;

namespace BankA.Application.UseCases.Rules.ListRules
{
    public class ListRulesResponse : PagedListResponse<RuleModel>
    {
        public ListRulesResponse(PagedListResponse<RuleModel> pagedList) : base(pagedList.Data, pagedList.RecordCount, pagedList.PageIndex, pagedList.PageSize)
        {
        }
    }



}
