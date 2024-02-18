using BankA.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace BankA.Infrastructure.Data.Seed
{
    internal class MerchantSeed
    {

        internal static async Task SeedAsync(AppDataDbContext context)
        {
            var count = context.Merchants.Count();
            if (count > 0)
                return;

            await context.Merchants.AddRangeAsync(
                new Merchant("Absolute Power"),
                new Merchant("Allianz"),
                new Merchant("Amazon"),
                new Merchant("ATM"),
                new Merchant("Bamboo Basket"),
                new Merchant("Bares"),
                new Merchant("Beer"),
                new Merchant("Boehm"),
                new Merchant("Book Store"),
                new Merchant("Bookshop"),
                new Merchant("CafeTasty"),
                new Merchant("Cinema"),
                new Merchant("Coffee Shops"),
                new Merchant("Countryside"),
                new Merchant("Crushed"),
                new Merchant("Fargo"),
                new Merchant("Fay Inc"),
                new Merchant("FitNinja"),
                new Merchant("Flour Power"),
                new Merchant("Fresh Choice"),
                new Merchant("Fuel"),
                new Merchant("Fullhouse Groceries"),
                new Merchant("Gizmo"),
                new Merchant("Haag"),
                new Merchant("HBO"),
                new Merchant("Herman Co"),
                new Merchant("Langosh"),
                new Merchant("Life Mobile"),
                new Merchant("McDonalds"),
                new Merchant("Ming Dynasty Foods"),
                new Merchant("Murphy"),
                new Merchant("Olson"),
                new Merchant("Pharmicus"),
                new Merchant("Purdy, Gorczany and Emard"),
                new Merchant("Restaurant"),
                new Merchant("Schulist"),
                new Merchant("Shine Bright"),
                new Merchant("Side Job"),
                new Merchant("Smile Studio"),
                new Merchant("Spotify"),
                new Merchant("Streich Inc"),
                new Merchant("Sweet Delights"),
                new Merchant("Switch"),
                new Merchant("Taxis"),
                new Merchant("Virgin"),
                new Merchant("Vodafone"),
                new Merchant("White Inc")
                );

            await context.SaveChangesAsync();
        }


    }
}
