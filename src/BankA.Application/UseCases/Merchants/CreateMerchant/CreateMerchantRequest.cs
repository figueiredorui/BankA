using MediatR;

namespace BankA.Application.UseCases.Merchants.CreateMerchant
{
    public class CreateMerchantRequest : IRequest<CreateMerchantResponse>
    {
        public string MerchantName { get; set; }
    }
}
