using BankA.Domain;

namespace BankA.Application.UseCases.Transactions.Models
{
    public class TransactionMerchantModel
    {
        public TransactionMerchantModel(Merchant merchant)
        {
            if (merchant == null)
                return;

            Id = merchant.Id;
            MerchantName = merchant.MerchantName;
        }

        public int Id { get; set; }
        public string MerchantName { get; set; }

    }


}
