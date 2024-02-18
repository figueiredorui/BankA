using BankA.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace BankA.Infrastructure.Data.Seed
{
    internal class TransactionSeed
    {
        internal static async Task SeedAsync(AppDataDbContext context)
        {
            var count = context.Transactions.Count();
            if (count > 0)
                return;

            int[] ar = { 12, 8, 4, 0 };
            foreach (var m in ar)
            {
                await context.Transactions.AddRangeAsync(
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "01"), "Vodafone", 0.00m, 39.99m, GetCategoryId("Mobile Phone"), GetMerchantId("Vodafone")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "01"), "Olson - Gutkowski using card ending with ***7067", 0.00m, GenerateAmount(), GetCategoryId("Rental Car"), GetMerchantId("Olson")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "02"), "Flour Power", 0.00m, 29.90m, GetCategoryId("Groceries"), GetMerchantId("Flour Power")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "02"), "Absolute Power Electric", 0.00m, 123.00m, GetCategoryId("Electricity"), GetMerchantId("Absolute Power")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "04"), "White Inc using card ending with ***3272", 0.00m, GenerateAmount(), GetCategoryId("Maintenance"), GetMerchantId("White Inc")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "05"), "Cash deposit transaction", 100.00m, 0.00m, GetCategoryId("Cash deposit"), GetMerchantId("Side Job")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "05"), "FitNinja Gym", 0.00m, 29.90m, GetCategoryId("Gym"), GetMerchantId("FitNinja")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "07"), "Life Mobile", 0.00m, 25.40m, GetCategoryId("Mobile Phone"), GetMerchantId("Life Mobile")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "08"), "BBVA Car Loan", 0.00m, 350.00m, GetCategoryId("Car Loan"), GetMerchantId("Fargo")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "10"), "Sweet Delights", 0.00m, 15.73m, GetCategoryId("Coffee shops"), GetMerchantId("Sweet Delights")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "10"), "Boehm Group using card ending with ***7067", 0.00m, GenerateAmount(), GetCategoryId("Hotel"), GetMerchantId("Boehm")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "11"), "Coffee Shop main Street", 0.00m, 4.80m, GetCategoryId("Coffee shops"), GetMerchantId("Coffee Shops")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "11"), "Thailand Restaurant", 0.00m, 68.00m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "12"), "CafeTasty", 0.00m, 7.00m, GetCategoryId("Coffee shops"), GetMerchantId("CafeTasty")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "12"), "Streich Inc using card ending with ***7067", 0.00m, GenerateAmount(), GetCategoryId("Car Service & Parts"), GetMerchantId("Streich Inc")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "13"), "withdrawal transaction at ATM", 0.00m, 100.00m, GetCategoryId("ATM"), GetMerchantId("ATM")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "15"), "Internet and TV Provider Limited", 0.00m, 99.00m, GetCategoryId("Internet"), GetMerchantId("Virgin")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "16"), "House insurance", 0.00m, 54.00m, GetCategoryId("Home Insurance"), GetMerchantId("Allianz")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "16"), "CafeTasty", 0.00m, 13.42m, GetCategoryId("Coffee shops"), GetMerchantId("CafeTasty")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "16"), "Haag, Larkin and Hessel using card ending ***3272", 0.00m, GenerateAmount(), GetCategoryId("Flights"), GetMerchantId("Haag")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "17"), "Highstreet Bar", 0.00m, 30.00m, GetCategoryId("Alcohol & Bars"), GetMerchantId("Bares")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "17"), "Spotify Subscription", 0.00m, 17.90m, GetCategoryId("Online Services"), GetMerchantId("Spotify")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "18"), "Coffee Shop main Street", 0.00m, 14.80m, GetCategoryId("Coffee shops"), GetMerchantId("Coffee Shops")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "18"), "Schulist, Erdman and Cassin using card ending with ***0386", 0.00m, GenerateAmount(), GetCategoryId("Pet Food"), GetMerchantId("Schulist")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "20"), "Herman Co Payroll Salary", 2287.90m, 0.00m, GetCategoryId("Salary"), GetMerchantId("Herman Co")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "20"), "Purdy, Gorczany and Emard using card ending with ***0386", 0.00m, GenerateAmount(), GetCategoryId("Veterinarian"), GetMerchantId("Purdy, Gorczany and Emard")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "20"), "HBO", 0.00m, 24.00m, GetCategoryId("Online Services"), GetMerchantId("HBO")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "21"), "CafeTasty", 0.00m, 56.60m, GetCategoryId("Coffee shops"), GetMerchantId("CafeTasty")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "21"), "Crushed grapes", 0.00m, 80.00m, GetCategoryId("Alcohol & Bars"), GetMerchantId("Crushed")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "24"), "Cash deposit transaction", 80.00m, 0.00m, GetCategoryId("Cash deposit"), GetMerchantId("Side Job")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "25"), "Fullhouse Groceries", 0.00m, 145.60m, GetCategoryId("Groceries"), GetMerchantId("Fullhouse Groceries")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "25"), "BP Fuel", 0.00m, 60.00m, GetCategoryId("Fuel"), GetMerchantId("Fuel")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "26"), "Coffee Shop main Street", 0.00m, 14.80m, GetCategoryId("Coffee shops"), GetMerchantId("Coffee Shops")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "27"), "Murphy Ltd Salary", 1875.00m, 0.00m, GetCategoryId("Salary"), GetMerchantId("Murphy")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "27"), "Coffee Shop main Street", 0.00m, 14.80m, GetCategoryId("Coffee shops"), GetMerchantId("Coffee Shops")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "28"), "White Inc using card ending with ***3272", 0.00m, GenerateAmount(), GetCategoryId("Maintenance"), GetMerchantId("White Inc")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-4 - m, "29"), "Grab & Go", 0.00m, 14.50m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),

                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "01"), "Vodafone", 0.00m, 39.99m, GetCategoryId("Mobile Phone"), GetMerchantId("Vodafone")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "02"), "Absolute Power Electric", 0.00m, 123.00m, GetCategoryId("Electricity"), GetMerchantId("Absolute Power")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "02"), "Fay Inc using card ending with ***0386", 0.00m, GenerateAmount(), GetCategoryId("Flights"), GetMerchantId("Fay Inc")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "04"), "withdrawal transaction at ATM", 0.00m, 200.00m, GetCategoryId("ATM"), GetMerchantId("ATM")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "04"), "Ming Dynasty Foods", 0.00m, 158.00m, GetCategoryId("Groceries"), GetMerchantId("Ming Dynasty Foods")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "05"), "FitNinja Gym", 0.00m, 29.90m, GetCategoryId("Gym"), GetMerchantId("FitNinja")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "06"), "BP Fuel", 0.00m, 60.00m, GetCategoryId("Fuel"), GetMerchantId("Fuel")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "06"), "Yellow Express Taxis", 0.00m, 30.00m, GetCategoryId("Taxi"), GetMerchantId("Taxis")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "06"), "Thailand Restaurant", 0.00m, 59.20m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "06"), "Langosh - Rutherford using card ending with ****3272", 0.00m, GenerateAmount(), GetCategoryId("Eyecare"), GetMerchantId("Langosh")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "07"), "Cash deposit transaction", 80.00m, 0.00m, GetCategoryId("Cash deposit"), GetMerchantId("Side Job")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "07"), "Boehm Group using card ending with ***7067", 0.00m, GenerateAmount(), GetCategoryId("Hotel"), GetMerchantId("Boehm")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "07"), "Switch My Stuff", 0.00m, 24.00m, GetCategoryId("Electronics"), GetMerchantId("Switch")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "07"), "BBVA Car Loan", 0.00m, 350.00m, GetCategoryId("Car Loan"), GetMerchantId("Fargo")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "07"), "Life Mobile", 0.00m, 25.40m, GetCategoryId("Mobile Phone"), GetMerchantId("Life Mobile")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "09"), "McDonalds Street", 0.00m, 25.11m, GetCategoryId("Restaurants"), GetMerchantId("McDonalds")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "11"), "Fullhouse Groceries", 0.00m, 131.40m, GetCategoryId("Groceries"), GetMerchantId("Fullhouse Groceries")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "11"), "Coffee Shop main Street", 0.00m, 13.20m, GetCategoryId("Coffee shops"), GetMerchantId("Coffee Shops")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "11"), "Fresh Choice", 0.00m, 91.84m, GetCategoryId("Groceries"), GetMerchantId("Fresh Choice")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "12"), "Switch My Stuff", 0.00m, 48.00m, GetCategoryId("Electronics"), GetMerchantId("Switch")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "14"), "Countryside winery", 0.00m, 21.00m, GetCategoryId("Alcohol & Bars"), GetMerchantId("Countryside")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "14"), "Streich Inc using card ending with ***7067", 0.00m, GenerateAmount(), GetCategoryId("Car Service & Parts"), GetMerchantId("Streich Inc")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "15"), "Internet and TV Provider Limited", 0.00m, 99.00m, GetCategoryId("Internet"), GetMerchantId("Virgin")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "16"), "withdrawal transaction at ATM", 0.00m, 50.00m, GetCategoryId("ATM"), GetMerchantId("ATM")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "17"), "Sweet Delights", 0.00m, 25.00m, GetCategoryId("Coffee shops"), GetMerchantId("Sweet Delights")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "17"), "BP Fuel", 0.00m, 70.00m, GetCategoryId("Fuel"), GetMerchantId("Fuel")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "17"), "sushi home", 0.00m, 24.00m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "17"), "sushi home", 0.00m, 22.00m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "17"), "sushi home", 0.00m, 22.00m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "17"), "sushi home", 0.00m, 19.00m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "17"), "Spotify Subscription", 0.00m, 17.90m, GetCategoryId("Online Services"), GetMerchantId("Spotify")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "18"), "House insurance", 0.00m, 54.00m, GetCategoryId("Home Insurance"), GetMerchantId("Allianz")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "19"), "Burger King Restaurant", 0.00m, 17.00m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "19"), "Flour Power", 0.00m, 16.40m, GetCategoryId("Groceries"), GetMerchantId("Flour Power")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "19"), "withdrawal transaction at ATM", 0.00m, 90.00m, GetCategoryId("ATM"), GetMerchantId("ATM")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "20"), "Yellow Express Taxis", 0.00m, 22.22m, GetCategoryId("Taxi"), GetMerchantId("Taxis")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "20"), "Herman Co Payroll Salary", 2287.90m, 0.00m, GetCategoryId("Salary"), GetMerchantId("Herman Co")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "20"), "HBO", 0.00m, 24.00m, GetCategoryId("Online Services"), GetMerchantId("HBO")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "21"), "Shine Bright", 0.00m, 0.28m, GetCategoryId("Spa & Massage"), GetMerchantId("Shine Bright")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "21"), "withdrawal transaction at ATM", 0.00m, 20.00m, GetCategoryId("ATM"), GetMerchantId("ATM")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "22"), "Flour Power", 0.00m, 15.00m, GetCategoryId("Groceries"), GetMerchantId("Flour Power")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "22"), "Book Store", 0.00m, 24.00m, GetCategoryId("Books"), GetMerchantId("Book Store")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "22"), "Coffee Shop main Street", 0.00m, 5.40m, GetCategoryId("Coffee shops"), GetMerchantId("Coffee Shops")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "25"), "Fresh Choice", 0.00m, 24.56m, GetCategoryId("Groceries"), GetMerchantId("Fresh Choice")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "25"), "Pharmicus", 0.00m, 20.00m, GetCategoryId("Pharmacy"), GetMerchantId("Pharmicus")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "25"), "Amazon.com", 0.00m, 35.50m, GetCategoryId("Groceries"), GetMerchantId("Amazon")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "25"), "Chopsticks Choice", 0.00m, 13.76m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "25"), "Beer LLC using card ending with ***0386", 0.00m, GenerateAmount(), GetCategoryId("Doctor"), GetMerchantId("Beer")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "26"), "Smile Studio Dental", 0.00m, 240.00m, GetCategoryId("Dentist"), GetMerchantId("Smile Studio")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "26"), "Gizmo Geeks", 0.00m, 10.96m, GetCategoryId("Electronics"), GetMerchantId("Gizmo")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "27"), "Murphy Ltd Salary", 1875.00m, 0.00m, GetCategoryId("Salary"), GetMerchantId("Murphy")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "28"), "Pizza Restaurant", 0.00m, 27.80m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "29"), "sushi home", 0.00m, 18.00m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "29"), "sushi home", 0.00m, 19.00m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-3 - m, "29"), "Thailand Restaurant", 0.00m, 58.70m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),

                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "01"), "Vodafone", 0.00m, 39.99m, GetCategoryId("Mobile Phone"), GetMerchantId("Vodafone")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "01"), "Yellow Express Taxis", 0.00m, 20.80m, GetCategoryId("Taxi"), GetMerchantId("Taxis")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "01"), "Smile Studio Dental", 0.00m, 50.00m, GetCategoryId("Dentist"), GetMerchantId("Smile Studio")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "01"), "Hollywood Cinema", 0.00m, 18.00m, GetCategoryId("Cinema"), GetMerchantId("Cinema")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "02"), "Fullhouse Groceries", 0.00m, 111.40m, GetCategoryId("Groceries"), GetMerchantId("Fullhouse Groceries")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "02"), "Coffee Shop main Street", 0.00m, 13.20m, GetCategoryId("Coffee shops"), GetMerchantId("Coffee Shops")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "02"), "Absolute Power Electric", 0.00m, 123.00m, GetCategoryId("Electricity"), GetMerchantId("Absolute Power")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "03"), "Fay Inc using card ending with ***0386", 0.00m, GenerateAmount(), GetCategoryId("Flights"), GetMerchantId("Fay Inc")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "03"), "sushi home", 0.00m, 24.00m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "03"), "sushi home", 0.00m, 21.00m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "04"), "Pizza Restaurant", 0.00m, 47.10m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "05"), "Yellow Express Taxis", 0.00m, 45.90m, GetCategoryId("Taxi"), GetMerchantId("Taxis")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "05"), "FitNinja Gym", 0.00m, 29.90m, GetCategoryId("Gym"), GetMerchantId("FitNinja")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "05"), "BP Fuel", 0.00m, 70.00m, GetCategoryId("Fuel"), GetMerchantId("Fuel")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "05"), "Grab & Go", 0.00m, 14.00m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "05"), "withdrawal transaction at ATM", 0.00m, 20.00m, GetCategoryId("ATM"), GetMerchantId("ATM")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "06"), "McDonalds Street", 0.00m, 23.12m, GetCategoryId("Restaurants"), GetMerchantId("McDonalds")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "07"), "Life Mobile", 0.00m, 25.40m, GetCategoryId("Mobile Phone"), GetMerchantId("Life Mobile")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "07"), "Purdy, Gorczany and Emard using card ending with ***0386", 0.00m, GenerateAmount(), GetCategoryId("Veterinarian"), GetMerchantId("Purdy, Gorczany and Emard")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "07"), "Highstreet Bar", 0.00m, 14.00m, GetCategoryId("Alcohol & Bars"), GetMerchantId("Bares")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "08"), "withdrawal transaction at ATM", 0.00m, 230.00m, GetCategoryId("ATM"), GetMerchantId("ATM")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "08"), "Coffee Shop main Street", 0.00m, 13.60m, GetCategoryId("Coffee shops"), GetMerchantId("Coffee Shops")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "08"), "Switch My Stuff", 0.00m, 24.00m, GetCategoryId("Electronics"), GetMerchantId("Switch")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "08"), "Yellow Express Taxis", 0.00m, 15.40m, GetCategoryId("Taxi"), GetMerchantId("Taxis")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "09"), "BBVA Car Loan", 0.00m, 350.00m, GetCategoryId("Car Loan"), GetMerchantId("Fargo")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "09"), "Coffee Shop main Street", 0.00m, 12.20m, GetCategoryId("Coffee shops"), GetMerchantId("Coffee Shops")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "10"), "Bamboo Basket", 0.00m, 87.12m, GetCategoryId("Groceries"), GetMerchantId("Bamboo Basket")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "10"), "Grab & Go", 0.00m, 4.00m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "11"), "Shine Bright", 0.00m, 5.00m, GetCategoryId("Spa & Massage"), GetMerchantId("Shine Bright")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "11"), "Curry Cove Foods", 0.00m, 7.80m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "11"), "McDonalds Street", 0.00m, 21.25m, GetCategoryId("Restaurants"), GetMerchantId("McDonalds")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "11"), "White Inc using card ending with ***3272", 0.00m, GenerateAmount(), GetCategoryId("Maintenance"), GetMerchantId("White Inc")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "12"), "Chopsticks Choice", 0.00m, 14.00m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "12"), "Curry Cove Foods", 0.00m, 9.80m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "12"), "Grab & Go", 0.00m, 8.40m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "12"), "Ming Dynasty Foods", 0.00m, 24.00m, GetCategoryId("Groceries"), GetMerchantId("Ming Dynasty Foods")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "13"), "Haag, Larkin and Hessel using card ending ***3272", 0.00m, GenerateAmount(), GetCategoryId("Flights"), GetMerchantId("Haag")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "15"), "Langosh - Rutherford using card ending with ****3272", 0.00m, GenerateAmount(), GetCategoryId("Eyecare"), GetMerchantId("Langosh")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "15"), "House insurance", 0.00m, 54.00m, GetCategoryId("Home Insurance"), GetMerchantId("Allianz")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "15"), "Burger King Restaurant", 0.00m, 24.70m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "15"), "Internet and TV Provider Limited", 0.00m, 99.00m, GetCategoryId("Internet"), GetMerchantId("Virgin")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "16"), "Yellow Express Taxis", 0.00m, 29.00m, GetCategoryId("Taxi"), GetMerchantId("Taxis")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "16"), "Shine Bright", 0.00m, 12.56m, GetCategoryId("Spa & Massage"), GetMerchantId("Shine Bright")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "16"), "withdrawal transaction at ATM", 0.00m, 20.00m, GetCategoryId("ATM"), GetMerchantId("ATM")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "17"), "Amazon.com", 0.00m, 11.90m, GetCategoryId("Groceries"), GetMerchantId("Amazon")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "17"), "Spotify Subscription", 0.00m, 17.90m, GetCategoryId("Online Services"), GetMerchantId("Spotify")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "17"), "withdrawal transaction at ATM", 0.00m, 30.00m, GetCategoryId("ATM"), GetMerchantId("ATM")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "18"), "Olson - Gutkowski using card ending with ***7067", 0.00m, GenerateAmount(), GetCategoryId("Rental Car"), GetMerchantId("Olson")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "18"), "Book Store", 0.00m, 43.14m, GetCategoryId("Books"), GetMerchantId("Book Store")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "18"), "Fullhouse Groceries", 0.00m, 109.40m, GetCategoryId("Groceries"), GetMerchantId("Fullhouse Groceries")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "19"), "Switch My Stuff", 0.00m, 28.40m, GetCategoryId("Electronics"), GetMerchantId("Switch")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "19"), "Amazon.com", 0.00m, 120.99m, GetCategoryId("Groceries"), GetMerchantId("Amazon")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "19"), "Gizmo Geeks", 0.00m, 15.00m, GetCategoryId("Electronics"), GetMerchantId("Gizmo")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "19"), "Cash deposit transaction", 500.00m, 0.00m, GetCategoryId("Cash deposit"), GetMerchantId("Side Job")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "19"), "Schulist, Erdman and Cassin using card ending with ***0386", 0.00m, GenerateAmount(), GetCategoryId("Pet Food"), GetMerchantId("Schulist")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "20"), "HBO", 0.00m, 24.00m, GetCategoryId("Online Services"), GetMerchantId("HBO")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "20"), "Shine Bright", 0.00m, 126.00m, GetCategoryId("Spa & Massage"), GetMerchantId("Shine Bright")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "20"), "BP Fuel", 0.00m, 90.00m, GetCategoryId("Fuel"), GetMerchantId("Fuel")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "20"), "Herman Co Payroll Salary", 2287.90m, 0.00m, GetCategoryId("Salary"), GetMerchantId("Herman Co")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "22"), "Bamboo Basket", 0.00m, 20.00m, GetCategoryId("Groceries"), GetMerchantId("Bamboo Basket")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "22"), "Schulist, Erdman and Cassin using card ending with ***0386", 0.00m, GenerateAmount(), GetCategoryId("Pet Food"), GetMerchantId("Schulist")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "23"), "Crushed grapes", 0.00m, 18.00m, GetCategoryId("Alcohol & Bars"), GetMerchantId("Crushed")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "26"), "Sweet Delights", 0.00m, 24.80m, GetCategoryId("Coffee shops"), GetMerchantId("Sweet Delights")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "26"), "Bookshop", 0.00m, 22.60m, GetCategoryId("Books"), GetMerchantId("Book Store")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "26"), "Grab & Go", 0.00m, 4.50m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "26"), "CafeTasty", 0.00m, 14.00m, GetCategoryId("Coffee shops"), GetMerchantId("CafeTasty")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "26"), "withdrawal transaction at ATM", 0.00m, 240.00m, GetCategoryId("ATM"), GetMerchantId("ATM")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "27"), "BP Fuel", 0.00m, 80.00m, GetCategoryId("Fuel"), GetMerchantId("Fuel")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "27"), "Murphy Ltd Salary", 1875.00m, 0.00m, GetCategoryId("Salary"), GetMerchantId("Murphy")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "28"), "Pharmicus", 0.00m, 36.70m, GetCategoryId("Pharmacy"), GetMerchantId("Pharmicus")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-2 - m, "30"), "Coffee Shop main Street", 0.00m, 12.60m, GetCategoryId("Coffee shops"), GetMerchantId("Coffee Shops")),

                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "01"), "Vodafone", 0.00m, 39.99m, GetCategoryId("Mobile Phone"), GetMerchantId("Vodafone")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "01"), "Book Store", 0.00m, 15.70m, GetCategoryId("Books"), GetMerchantId("Book Store")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "01"), "Highstreet Bar", 0.00m, 34.00m, GetCategoryId("Alcohol & Bars"), GetMerchantId("Bares")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "02"), "Absolute Power Electric", 0.00m, 123.00m, GetCategoryId("Electricity"), GetMerchantId("Absolute Power")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "03"), "Countryside winery", 0.00m, 74.43m, GetCategoryId("Alcohol & Bars"), GetMerchantId("Countryside")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "04"), "Amazon.com", 0.00m, 24.00m, GetCategoryId("Groceries"), GetMerchantId("Amazon")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "04"), "Pharmicus", 0.00m, 25.99m, GetCategoryId("Pharmacy"), GetMerchantId("Pharmicus")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "05"), "Fresh Choice", 0.00m, 63.64m, GetCategoryId("Groceries"), GetMerchantId("Fresh Choice")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "05"), "FitNinja Gym", 0.00m, 29.90m, GetCategoryId("Gym"), GetMerchantId("FitNinja")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "05"), "Flour Power", 0.00m, 59.09m, GetCategoryId("Groceries"), GetMerchantId("Flour Power")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "06"), "Thailand Restaurant", 0.00m, 23.00m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "07"), "BBVA Car Loan", 0.00m, 350.00m, GetCategoryId("Car Loan"), GetMerchantId("Fargo")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "07"), "Life Mobile", 0.00m, 25.40m, GetCategoryId("Mobile Phone"), GetMerchantId("Life Mobile")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "07"), "Schulist, Erdman and Cassin using card ending with ***0386", 0.00m, GenerateAmount(), GetCategoryId("Pet Food"), GetMerchantId("Schulist")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "08"), "withdrawal transaction at ATM", 0.00m, 30.00m, GetCategoryId("ATM"), GetMerchantId("ATM")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "08"), "Sweet Delights", 0.00m, 21.60m, GetCategoryId("Coffee shops"), GetMerchantId("Sweet Delights")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "08"), "Chopsticks Choice", 0.00m, 43.88m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "09"), "Burger King Restaurant", 0.00m, 12.00m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "09"), "Bamboo Basket", 0.00m, 75.30m, GetCategoryId("Groceries"), GetMerchantId("Bamboo Basket")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "11"), "Crushed grapes", 0.00m, 96.80m, GetCategoryId("Alcohol & Bars"), GetMerchantId("Crushed")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "11"), "Boehm Group using card ending with ***7067", 0.00m, GenerateAmount(), GetCategoryId("Hotel"), GetMerchantId("Boehm")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "12"), "Hollywood Cinema", 0.00m, 18.00m, GetCategoryId("Cinema"), GetMerchantId("Cinema")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "13"), "Bamboo Basket", 0.00m, 18.50m, GetCategoryId("Groceries"), GetMerchantId("Bamboo Basket")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "13"), "Countryside winery", 0.00m, 123.26m, GetCategoryId("Alcohol & Bars"), GetMerchantId("Countryside")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "14"), "Switch My Stuff", 0.00m, 42.00m, GetCategoryId("Electronics"), GetMerchantId("Switch")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "14"), "Pizza Restaurant", 0.00m, 28.00m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "15"), "Internet and TV Provider Limited", 0.00m, 99.00m, GetCategoryId("Internet"), GetMerchantId("Virgin")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "15"), "Cash deposit transaction", 150.00m, 0.00m, GetCategoryId("Cash deposit"), GetMerchantId("Side Job")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "16"), "Ming Dynasty Foods", 0.00m, 34.00m, GetCategoryId("Groceries"), GetMerchantId("Ming Dynasty Foods")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "16"), "Fresh Choice", 0.00m, 14.20m, GetCategoryId("Groceries"), GetMerchantId("Fresh Choice")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "16"), "House insurance", 0.00m, 54.00m, GetCategoryId("Home Insurance"), GetMerchantId("Allianz")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "16"), "sushi home", 0.00m, 38.00m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "17"), "CafeTasty", 0.00m, 17.70m, GetCategoryId("Coffee shops"), GetMerchantId("CafeTasty")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "17"), "Spotify Subscription", 0.00m, 17.90m, GetCategoryId("Online Services"), GetMerchantId("Spotify")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "17"), "White Inc using card ending with ***3272", 0.00m, GenerateAmount(), GetCategoryId("Maintenance"), GetMerchantId("White Inc")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "18"), "withdrawal transaction at ATM", 0.00m, 30.00m, GetCategoryId("ATM"), GetMerchantId("ATM")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "19"), "Streich Inc using card ending with ***7067", 0.00m, GenerateAmount(), GetCategoryId("Car Service & Parts"), GetMerchantId("Streich Inc")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "19"), "Smile Studio Dental", 0.00m, 100.00m, GetCategoryId("Dentist"), GetMerchantId("Smile Studio")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "19"), "Book Store", 0.00m, 24.00m, GetCategoryId("Books"), GetMerchantId("Book Store")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "20"), "Gizmo Geeks", 0.00m, 9.80m, GetCategoryId("Electronics"), GetMerchantId("Gizmo")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "20"), "Curry Cove Foods", 0.00m, 9.40m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "20"), "HBO", 0.00m, 24.00m, GetCategoryId("Online Services"), GetMerchantId("HBO")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "20"), "Herman Co Payroll Salary", 2287.90m, 0.00m, GetCategoryId("Salary"), GetMerchantId("Herman Co")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "21"), "Grab & Go", 0.00m, 8.00m, GetCategoryId("Restaurants"), GetMerchantId("Restaurant")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "24"), "Purdy, Gorczany and Emard using card ending with ***0386", 0.00m, GenerateAmount(), GetCategoryId("Veterinarian"), GetMerchantId("Purdy, Gorczany and Emard")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "27"), "Beer LLC using card ending with ***0386", 0.00m, GenerateAmount(), GetCategoryId("Doctor"), GetMerchantId("Beer")),
                        new Transaction(GetDefaultAccount(), GenerateDate(-1 - m, "27"), "Murphy Ltd Salary", 1875.00m, 0.00m, GetCategoryId("Salary"), GetMerchantId("Murphy"))
                    );

                await context.SaveChangesAsync();
            }

            Account GetDefaultAccount()
            {
                return GetAccount("Demo Account");
            }

            Account GetAccount(string v)
            {
                return context.Accounts.Where(q => q.Description.Equals(v)).FirstOrDefault();
            }

            int GetMerchantId(string v)
            {
                return context.Merchants.Where(q => q.MerchantName.Equals(v)).Select(s => s.Id).FirstOrDefault();
            }

            int GetCategoryId(string v)
            {
                return context.Categories.Where(q => q.CategoryName.Equals(v)).Select(s => s.Id).FirstOrDefault();
            }

            DateTime GenerateDate(int offset, string dayString)
            {
                try
                {
                    var day = int.Parse(dayString);
                    var monthOffset = DateTime.Now.AddMonths(offset);
                    if (monthOffset.Month == 2 && day > 28)
                        day = 28;

                    return new DateTime(monthOffset.Year, monthOffset.Month, day);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            decimal GenerateAmount()
            {
                Random random = new Random();
                int r = random.Next(14142, 41416); //+1 as end is excluded.
                Double result = (Double)r / 100.00;

                return Convert.ToDecimal(result);
            }
        }
    }
}
