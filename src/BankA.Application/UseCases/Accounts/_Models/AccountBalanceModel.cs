namespace BankA.Application.UseCases.Accounts
{
    public class AccountBalanceModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Balance { get; set; }
    }
}
