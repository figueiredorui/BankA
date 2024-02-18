namespace BankA.Domain
{
    public class Merchant : EntityBase
    {
        public string MerchantName { get; private set; }
        private Merchant()
        {

        }
        public Merchant(string merchantName)
        {
            MerchantName = merchantName;
        }

        public void Update(string merchantName)
        {
            MerchantName = merchantName;
        }
    }
}
