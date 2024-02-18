namespace BankA.Application.UseCases.Rules
{
    public class RuleModel
    {
        public int RuleId { get; set; }
        public string Keywords { get; set; }
        public int CategoryId { get; set; }
        public string CategoryFullName { get; set; }
        public int MerchantId { get; set; }
        public string MerchantName { get; set; }
    }
}
