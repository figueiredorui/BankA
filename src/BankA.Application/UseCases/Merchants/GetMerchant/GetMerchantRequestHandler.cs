using BankA.Application.Interfaces;
using BankA.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Merchants.GetMerchant
{
    class GetMerchantRequestHandler : IRequestHandler<GetMerchantRequest, GetMerchantResponse>
    {
        private readonly IAppDataDbContext _bankADbContext;

        public GetMerchantRequestHandler(IAppDataDbContext bankADbContext)
        {
            _bankADbContext = bankADbContext;
        }

        public async Task<GetMerchantResponse> Handle(GetMerchantRequest request, CancellationToken cancellationToken)
        {
            var merchant = await GetMerchant(request.MerchantId);

            return new GetMerchantResponse()
            {
                MerchantId = merchant.Id,
                MerchantName = merchant.MerchantName
            };
        }

        private async Task<Merchant> GetMerchant(int merchantId)
        {
            var merchant = await _bankADbContext.Merchants.Where(q => q.Id == merchantId).FirstOrDefaultAsync();
            if (merchant == null)
                throw new ApplicationException($"Merchant '{merchantId}' not found");
            return merchant;
        }
    }
}
