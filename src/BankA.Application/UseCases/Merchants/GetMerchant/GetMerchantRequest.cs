using MediatR;

namespace BankA.Application.UseCases.Merchants.GetMerchant
{
    public class GetMerchantRequest : IRequest<GetMerchantResponse>
    {
        public GetMerchantRequest(int merchantId)
        {
            MerchantId = merchantId;
        }

        public int MerchantId { get; set; }
    }
}
