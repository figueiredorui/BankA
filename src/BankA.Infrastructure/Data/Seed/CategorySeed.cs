using BankA.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace BankA.Infrastructure.Data.Seed
{
    internal class CategorySeed
    {
        internal static async Task SeedAsync(AppDataDbContext context)
        {
            var count = context.Categories.Count();
            if (count > 0)
                return;

            var transfer = new Category("Transfer", Domain.Enums.CategoryTypeEnum.Transfer, true);
            var income = new Category("Income", Domain.Enums.CategoryTypeEnum.Income, false);
            var auto = new Category("Auto", Domain.Enums.CategoryTypeEnum.Expense, false);
            var billsutilities = new Category("Bills & Utilities", Domain.Enums.CategoryTypeEnum.Expense, false);
            var education = new Category("Education", Domain.Enums.CategoryTypeEnum.Expense, false);
            var entertainment = new Category("Entertainment", Domain.Enums.CategoryTypeEnum.Expense, false);
            var expenses = new Category("Expenses", Domain.Enums.CategoryTypeEnum.Expense, false);
            var feescharges = new Category("Fees & Charges", Domain.Enums.CategoryTypeEnum.Expense, false);
            var fooddining = new Category("Food & Dining", Domain.Enums.CategoryTypeEnum.Expense, false);
            var giftsdonations = new Category("Gifts & Donations", Domain.Enums.CategoryTypeEnum.Expense, false);
            var healthfitness = new Category("Health & Fitness", Domain.Enums.CategoryTypeEnum.Expense, false);
            var household = new Category("Household", Domain.Enums.CategoryTypeEnum.Expense, false);
            var insurance = new Category("Insurance", Domain.Enums.CategoryTypeEnum.Expense, false);
            var loans = new Category("Loans", Domain.Enums.CategoryTypeEnum.Expense, false);
            var personalcare = new Category("Personal Care", Domain.Enums.CategoryTypeEnum.Expense, false);
            var petcare = new Category("Pet Care", Domain.Enums.CategoryTypeEnum.Expense, false);
            var shopping = new Category("Shopping", Domain.Enums.CategoryTypeEnum.Expense, false);
            var taxes = new Category("Taxes", Domain.Enums.CategoryTypeEnum.Expense, false);
            var travel = new Category("Travel", Domain.Enums.CategoryTypeEnum.Expense, false);


            await context.Categories.AddRangeAsync(transfer,
                                                    income,
                                                    auto,
                                                    billsutilities,
                                                    education,
                                                    entertainment,
                                                    expenses,
                                                    feescharges,
                                                    fooddining,
                                                    giftsdonations,
                                                    healthfitness,
                                                    household,
                                                    insurance,
                                                    loans,
                                                    personalcare,
                                                    petcare,
                                                    shopping,
                                                    taxes,
                                                    travel);

            await context.SaveChangesAsync();

            await context.Categories.AddRangeAsync(
                new Category("Transfer", transfer),
                new Category("Cash deposit", income),
                new Category("Cheque deposit", income),
                new Category("Interest", income),
                new Category("Refund", income),
                new Category("Salary", income),
                new Category("Government Taxes", taxes),
                new Category("Income Tax", taxes),
                new Category("Pet Food", petcare),
                new Category("Veterinarian", petcare),
                new Category("Bank Fee", feescharges),
                new Category("Government Fees", feescharges),
                new Category("Postal Fees", feescharges),
                new Category("Flights", travel),
                new Category("Holiday", travel),
                new Category("Hotel", travel),
                new Category("Public Transport", travel),
                new Category("Rental Car", travel),
                new Category("Taxi", travel),
                new Category("Car Service & Parts", auto),
                new Category("Car Taxes", auto),
                new Category("Fuel", auto),
                new Category("Parking", auto),
                new Category("Toll Road", auto),
                new Category("Electricity", billsutilities),
                new Category("Internet", billsutilities),
                new Category("Mobile Phone", billsutilities),
                new Category("Online Services", billsutilities),
                new Category("Television", billsutilities),
                new Category("Water", billsutilities),
                new Category("Charity", giftsdonations),
                new Category("Donation", giftsdonations),
                new Category("Gift", giftsdonations),
                new Category("ATM", expenses),
                new Category("Electronics", expenses),
                new Category("Expenses", expenses),
                new Category("Others", expenses),
                new Category("Alcohol & Bars", fooddining),
                new Category("Coffee shops", fooddining),
                new Category("Fast Food", fooddining),
                new Category("Groceries", fooddining),
                new Category("Restaurants", fooddining),
                new Category("Hair", personalcare),
                new Category("Laundry", personalcare),
                new Category("Spa & Massage", personalcare),
                new Category("Amazon", shopping),
                new Category("Books", shopping),
                new Category("Clothing", shopping),
                new Category("Hobbies", shopping),
                new Category("Shoes", shopping),
                new Category("Car Insurance", insurance),
                new Category("Health Insurance", insurance),
                new Category("Home Insurance", insurance),
                new Category("Life Insurance", insurance),
                new Category("Car Loan", loans),
                new Category("Credit cards", loans),
                new Category("House loan", loans),
                new Category("Student loans", loans),
                new Category("Appliances", household),
                new Category("Furniture", household),
                new Category("House Cleaning", household),
                new Category("Maintenance", household),
                new Category("Courses", education),
                new Category("School Fees", education),
                new Category("School Supplies", education),
                new Category("Arts", entertainment),
                new Category("Cinema", entertainment),
                new Category("Music", entertainment),
                new Category("Dentist", healthfitness),
                new Category("Doctor", healthfitness),
                new Category("Eyecare", healthfitness),
                new Category("Gym", healthfitness),
                new Category("Pharmacy", healthfitness)
                );

            await context.SaveChangesAsync();
        }


    }
}
