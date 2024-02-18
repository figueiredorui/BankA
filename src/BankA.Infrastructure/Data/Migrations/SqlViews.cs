using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Infrastructure.Data.Migrations
{
    internal class SqlViews
    {
        public static string TransactionsView()
        {
            var v = @"CREATE OR ALTER VIEW [dbo].[BankTransactionsView]
                    AS
                    SELECT BankTransaction.AccountId, BankTransaction.Id, TransactionDate, Description, CreditAmount, DebitAmount, (CreditAmount - DebitAmount) as Balance,
                    SUM (CreditAmount - DebitAmount) OVER (partition by BankTransaction.AccountId ORDER BY TransactionDate, BankTransaction.Id) AS RunningBalance, BankCategory.CategoryType, BankTransaction.CategoryId, BankCategory.CategoryName, BankCategory.ParentId as CategoryParentId, BankTransaction.MerchantId, BankMerchant.MerchantName
                    FROM BankTransaction LEFT JOIN BankCategory ON BankTransaction.CategoryId = BankCategory.Id
                    LEFT JOIN BankMerchant ON BankTransaction.MerchantId = BankMerchant.Id
                    GO
                    ";

            return v;
        }


    }
}
