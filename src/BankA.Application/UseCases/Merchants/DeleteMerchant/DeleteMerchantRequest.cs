using MediatR;

namespace BankA.Application.UseCases.Merchants.DeleteMerchant
{
    public class DeleteMerchantRequest : IRequest<bool>
    {
        public DeleteMerchantRequest(int merchantId)
        {
            MerchantId = merchantId;
        }

        public int MerchantId { get; set; }
    }
}
