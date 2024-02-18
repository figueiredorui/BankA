using BankA.Application.Interfaces;
using BankA.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Merchants.CreateMerchant
{
    class CreateMerchantRequestHandler : IRequestHandler<CreateMerchantRequest, CreateMerchantResponse>
    {
        private readonly IAppDataDbContext _bankADbContext;

        public CreateMerchantRequestHandler(IAppDataDbContext bankADbContext)
        {
            _bankADbContext = bankADbContext;
        }

        public async Task<CreateMerchantResponse> Handle(CreateMerchantRequest request, CancellationToken cancellationToken)
        {
            var merchant = new Merchant(request.MerchantName);
            await _bankADbContext.Merchants.AddAsync(merchant);
            await _bankADbContext.SaveChangesAsync();

            return new CreateMerchantResponse() { Id = merchant.Id };
        }
    }
}
