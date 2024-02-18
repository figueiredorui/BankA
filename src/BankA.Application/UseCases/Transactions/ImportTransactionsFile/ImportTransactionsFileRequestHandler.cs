using BankA.Application.Interfaces;
using BankA.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BankA.Application.UseCases.Transactions.ImportTransactionsFile
{
    class ImportTransactionsFileRequestHandler : IRequestHandler<ImportTransactionsFileRequest, bool>
    {

        private readonly IAppDataDbContext _bankADbContext;
        private readonly IStatementFileParserFactory _bankFileFactory;
        public ImportTransactionsFileRequestHandler(
            IAppDataDbContext bankADbContext,
            IStatementFileParserFactory bankFileFactory)
        {
            _bankADbContext = bankADbContext;
            _bankFileFactory = bankFileFactory;
        }

        public async Task<bool> Handle(ImportTransactionsFileRequest request, CancellationToken cancellationToken)
        {

            var account = await _bankADbContext.GetAccountById(request.AccountId);
            if (account == null)
                throw new ApplicationException($"AccountId '{request.AccountId}' not found");

            if (account.FileFormat == Domain.Enums.StatementFileFormatEnum.CSV)
            {
                if (account.FileFormatConfiguration == null)
                    throw new ApplicationException($"Invalid CSV Format Configuration .");
            }

            var fileParser = _bankFileFactory.Create(account.FileFormat);
            var transactionList = fileParser.Parse(account, request.FileContent);

            await SaveTransactions(account, request, transactionList);
            return true;

        }

        private async Task SaveTransactions(Account account, ImportTransactionsFileRequest fileImport, List<Transaction> fileTransactions)
        {
            fileTransactions = await FilterTransactions(account.Id, fileTransactions);
            await AddTransactions(account, fileImport, fileTransactions);

            await _bankADbContext.SaveChangesAsync();
        }

        private async Task<List<Transaction>> FilterTransactions(int accountId, List<Transaction> transactionList)
        {
            DateTime lastTransactionDate = DateTime.MinValue;

            var hasTransactions = await GetTransactionByAccountId(accountId);
            if (hasTransactions.Any())
                lastTransactionDate = hasTransactions.Max(m => m.TransactionDate);

            var filteredTransactionList = transactionList.Where(q => q.TransactionDate > lastTransactionDate)
                                                        .ToList();

            return filteredTransactionList;
        }

        private async Task AddTransactions(Account account, ImportTransactionsFileRequest fileImport, List<Transaction> transactionList)
        {
            var file = new TransactionFile(account,
                                            fileImport.FileName,
                                            fileImport.ContentType,
                                            fileImport.FileContent,
                                            transactionList);

            _bankADbContext.Files.Add(file);

            await Task.CompletedTask;
        }

        public async Task<List<Transaction>> GetTransactionByAccountId(int accountId)
        {
            return await _bankADbContext.Transactions.Include(i => i.Account)
                        .Where(q => q.Account.Id == accountId).ToListAsync();
        }
    }
}
