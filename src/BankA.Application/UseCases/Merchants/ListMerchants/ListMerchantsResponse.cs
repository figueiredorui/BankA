using BankA.Application.Models;

namespace BankA.Application.UseCases.Merchants.ListMerchants
{
    public class ListMerchantsResponse : PagedListResponse<MerchantModel>
    {
        public ListMerchantsResponse(PagedListResponse<MerchantModel> pagedList) : base(pagedList.Data, pagedList.RecordCount, pagedList.PageIndex, pagedList.PageSize)
        {
        }
    }



}
