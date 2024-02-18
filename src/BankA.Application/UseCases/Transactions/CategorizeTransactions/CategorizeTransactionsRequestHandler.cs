using BankA.Application.Interfaces;
using BankA.Application.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Transactions.CategorizeTransactions
{
    class CategorizeTransactionsRequestHandler : IRequestHandler<CategorizeTransactionsRequest, CategorizeTransactionsResponse>
    {

        private readonly IAppDataDbContext _bankADbContext;
        private readonly CategoriseTransactionsService _categoriseService;
        public CategorizeTransactionsRequestHandler(
            IAppDataDbContext accountRepository,
            CategoriseTransactionsService bankADbContext)
        {
            _bankADbContext = accountRepository;
            _categoriseService = bankADbContext;
        }

        public async Task<CategorizeTransactionsResponse> Handle(CategorizeTransactionsRequest request, CancellationToken cancellationToken)
        {
            var account = await _bankADbContext.GetAccountById(request.AccountId);
            if (account == null)
                throw new ApplicationException($"AccountId '{request.AccountId}' not found");

            var report = await _categoriseService.Categorise(request.AccountId);

            return new CategorizeTransactionsResponse() { Categorised = report.Categorised, Uncategorised = report.Uncategorised };
        }
    }
}
