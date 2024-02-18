using System.Globalization;

namespace BankA.Application.UseCases.Reports._Models
{
    public class CashFlowMonthlyModel
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthYear { get { return string.Format("{0}/{1}", CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month).Substring(0, 3), Year); } }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
    }
}
