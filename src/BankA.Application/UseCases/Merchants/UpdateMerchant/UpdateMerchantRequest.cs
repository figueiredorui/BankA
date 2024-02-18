using MediatR;

namespace BankA.Application.UseCases.Merchants.UpdateMerchant
{
    public class UpdateMerchantRequest : IRequest<bool>
    {
        public int MerchantId { get; set; }
        public string MerchantName { get; set; }
    }
}
