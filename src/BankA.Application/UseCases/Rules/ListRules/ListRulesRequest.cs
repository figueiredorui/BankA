using BankA.Application.Models;
using MediatR;

namespace BankA.Application.UseCases.Rules.ListRules
{
    public class ListRulesRequest : PagedListRequest, IRequest<ListRulesResponse>
    {
        public string Search { get; set; }
    }
}
