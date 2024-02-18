
using System;

namespace BankA.Domain
{
    public class Rule : EntityBase
    {
        public string Keywords { get; private set; }
        public Category Category { get; private set; }
        public Merchant Merchant { get; private set; }

        private Rule()
        {

        }

        public Rule(string keywords, Category category, Merchant merchant)
        {
            Keywords = keywords ?? throw new ArgumentNullException(nameof(keywords));
            Category = category ?? throw new ArgumentNullException(nameof(category));
            Merchant = merchant ?? throw new ArgumentNullException(nameof(merchant));
        }

        public void Update(string keywords, Category category, Merchant merchant)
        {
            Keywords = keywords ?? throw new ArgumentNullException(nameof(keywords));
            Category = category ?? throw new ArgumentNullException(nameof(category));
            Merchant = merchant ?? throw new ArgumentNullException(nameof(merchant));
        }

        public void ClearMerchant()
        {
            Merchant = null;
        }

        public bool KeywordMatch(string description)
        {
            return description.ToLower().Contains(Keywords.ToLower());
        }

        public bool KeywordMatch(string description, decimal balance)
        {
            if (balance >= 0)
                return description.ToLower().Contains(Keywords.ToLower()) && (Category.IsIncome || Category.IsTransfer);
            else
                return description.ToLower().Contains(Keywords.ToLower()) && (Category.IsExpense || Category.IsTransfer);
        }
    }
}
