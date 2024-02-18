using BankA.Application.Interfaces;
using BankA.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Merchants.UpdateMerchant
{
    class UpdateMerchantRequestHandler : IRequestHandler<UpdateMerchantRequest, bool>
    {
        private readonly IAppDataDbContext _bankADbContext;

        public UpdateMerchantRequestHandler(IAppDataDbContext bankADbContext)
        {
            _bankADbContext = bankADbContext;
        }

        public async Task<bool> Handle(UpdateMerchantRequest request, CancellationToken cancellationToken)
        {
            Merchant merchant = await GetMerchant(request.MerchantId);

            merchant.Update(request.MerchantName);

            await _bankADbContext.SaveChangesAsync();

            return true;
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
