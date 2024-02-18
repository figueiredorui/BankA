using BankA.Application.UseCases.Merchants.GetMerchant;
using BankA.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace BankA.Infrastructure.Data.Seed
{
    internal class RuleSeed
    {

        internal static async Task SeedAsync(AppDataDbContext context)
        {
            var count = context.Rules.Count();
            if (count > 0)
                return;

            await context.Rules.AddRangeAsync(
                new Rule("Absolute Power Electric", GetCategory("Electricity"), GetMerchant("Absolute Power")),
                new Rule("Amazon", GetCategory("Groceries"), GetMerchant("Amazon")),
                new Rule("ATM", GetCategory("ATM"), GetMerchant("ATM")),
                new Rule("Bamboo Basket", GetCategory("Groceries"), GetMerchant("Bamboo Basket")),
                new Rule("Bar", GetCategory("Alcohol & Bars"), GetMerchant("Bares")),
                new Rule("Beer LLC", GetCategory("Doctor"), GetMerchant("Beer")),
                new Rule("Boehm Group", GetCategory("Hotel"), GetMerchant("Boehm")),
                new Rule("Book", GetCategory("Books"), GetMerchant("Book Store")),
                new Rule("Cafe", GetCategory("Coffee shops"), GetMerchant("CafeTasty")),
                new Rule("Car Loan", GetCategory("Car Loan"), GetMerchant("Fargo")),
                new Rule("Cash deposit", GetCategory("Cash deposit"), GetMerchant("Side Job")),
                new Rule("Chopsticks Choice", GetCategory("Restaurants"), GetMerchant("Restaurant")),
                new Rule("Cinema", GetCategory("Cinema"), GetMerchant("Cinema")),
                new Rule("Coffee Shop", GetCategory("Coffee shops"), GetMerchant("Coffee Shops")),
                new Rule("Crushed grapes", GetCategory("Alcohol & Bars"), GetMerchant("Crushed")),
                new Rule("Curry Cove Foods", GetCategory("Restaurants"), GetMerchant("Restaurant")),
                new Rule("Fay Inc", GetCategory("Flights"), GetMerchant("Fay Inc")),
                new Rule("Flour Power", GetCategory("Groceries"), GetMerchant("Flour Power")),
                new Rule("Fresh Choice", GetCategory("Groceries"), GetMerchant("Fresh Choice")),
                new Rule("Fuel", GetCategory("Fuel"), GetMerchant("Fuel")),
                new Rule("Fullhouse Groceries", GetCategory("Groceries"), GetMerchant("Fullhouse Groceries")),
                new Rule("Gizmo Geeks", GetCategory("Electronics"), GetMerchant("Gizmo")),
                new Rule("Grab & Go", GetCategory("Restaurants"), GetMerchant("Restaurant")),
                new Rule("Gym", GetCategory("Gym"), GetMerchant("FitNinja")),
                new Rule("Haag", GetCategory("Flights"), GetMerchant("Haag")),
                new Rule("HBO", GetCategory("Online Services"), GetMerchant("HBO")),
                new Rule("Herman Co Payroll Salary", GetCategory("Salary"), GetMerchant("Herman Co")),
                new Rule("House insurance", GetCategory("Home Insurance"), GetMerchant("Allianz")),
                new Rule("Internet and TV", GetCategory("Internet"), GetMerchant("Virgin")),
                new Rule("Langosh", GetCategory("Eyecare"), GetMerchant("Langosh")),
                new Rule("Life Mobile", GetCategory("Mobile Phone"), GetMerchant("Life Mobile")),
                new Rule("McDonalds", GetCategory("Restaurants"), GetMerchant("McDonalds")),
                new Rule("Ming Dynasty Foods", GetCategory("Groceries"), GetMerchant("Ming Dynasty Foods")),
                new Rule("Murphy Ltd Salary", GetCategory("Salary"), GetMerchant("Murphy")),
                new Rule("Olson ", GetCategory("Rental Car"), GetMerchant("Olson")),
                new Rule("Pharmicus", GetCategory("Pharmacy"), GetMerchant("Pharmicus")),
                new Rule("Purdy, Gorczany and Emard", GetCategory("Veterinarian"), GetMerchant("Purdy, Gorczany and Emard")),
                new Rule("Restaurant", GetCategory("Restaurants"), GetMerchant("Restaurant")),
                new Rule("Schulist", GetCategory("Pet Food"), GetMerchant("Schulist")),
                new Rule("Shine Bright", GetCategory("Spa & Massage"), GetMerchant("Shine Bright")),
                new Rule("Smile Studio Dental", GetCategory("Dentist"), GetMerchant("Smile Studio")),
                new Rule("Spotify", GetCategory("Online Services"), GetMerchant("Spotify")),
                new Rule("Streich Inc", GetCategory("Car Service & Parts"), GetMerchant("Streich Inc")),
                new Rule("sushi home", GetCategory("Restaurants"), GetMerchant("Restaurant")),
                new Rule("Sweet Delights", GetCategory("Coffee shops"), GetMerchant("Sweet Delights")),
                new Rule("Switch My Stuff", GetCategory("Electronics"), GetMerchant("Switch")),
                new Rule("Taxis", GetCategory("Taxi"), GetMerchant("Taxis")),
                new Rule("Vodafone", GetCategory("Mobile Phone"), GetMerchant("Vodafone")),
                new Rule("White Inc", GetCategory("Maintenance"), GetMerchant("White Inc")),
                new Rule("winery", GetCategory("Alcohol & Bars"), GetMerchant("Countryside"))
                );

            await context.SaveChangesAsync();

            Merchant GetMerchant(string v)
            {
                return context.Merchants.Where(q => q.MerchantName.Equals(v)).FirstOrDefault();
            }

            Category GetCategory(string v)
            {
                return context.Categories.Where(q => q.CategoryName.Equals(v)).FirstOrDefault();
            }
        }


    }
}
