using BankA.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Accounts.GetAccountBalance
{
    class GetAccountBalanceRequestHandler : IRequestHandler<GetAccountBalanceRequest, GetAccountBalanceResponse>
    {
        private readonly IAppDataDbContext _bankADbContext;
        public GetAccountBalanceRequestHandler(IAppDataDbContext bankADbContext)
        {
            _bankADbContext = bankADbContext;
        }

        public async Task<GetAccountBalanceResponse> Handle(GetAccountBalanceRequest request, CancellationToken cancellationToken)
        {
            var result = _bankADbContext.Accounts.AsNoTracking()
                            .Where(q => q.Closed == false)
                             .Select(x => new AccountBalanceModel
                             {
                                 Id = x.Id,
                                 Description = x.Description,
                                 Balance = _bankADbContext.Transactions.Where(q => q.Account.Id == x.Id).Sum(t => t.CreditAmount - t.DebitAmount),
                             })
                             .OrderBy(q => q.Description)
                             .ToList();

            return new GetAccountBalanceResponse(result);
        }
    }
}
