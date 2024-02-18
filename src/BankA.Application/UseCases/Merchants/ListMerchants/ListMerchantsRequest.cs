using BankA.Application.Models;
using MediatR;

namespace BankA.Application.UseCases.Merchants.ListMerchants
{
    public class ListMerchantsRequest : PagedListRequest, IRequest<ListMerchantsResponse>
    {
        public string Search { get; set; }
    }
}
