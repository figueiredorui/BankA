using BankA.Domain;
using BankA.Domain.Enums;
using DotNetLib.EfCore.EntityAudit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BankA.Infrastructure.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("BankCategory");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.CategoryName).HasMaxLength(50);
            builder.Property(e => e.CategoryType).HasMaxLength(50).HasConversion(new EnumToStringConverter<CategoryTypeEnum>());
            builder.HasOne(c => c.Parent)
                    .WithMany()
                    .HasForeignKey("ParentId");

            builder.AddAuditingProperties();

            builder.Ignore(e => e.CategoryFullName);

        }

        private void SeedData(EntityTypeBuilder<Category> builder)
        {

            builder.HasData(
                    new { Id = 1, CategoryName = "Income", CategoryType = "Income", IsSystem = false, Color = Colors.Get(1) },
                    new { Id = 20, CategoryName = "Salary", CategoryType = "Income", IsSystem = false, ParentId = 1, Color = Colors.Get(20) },
                    new { Id = 74, CategoryName = "Refund", CategoryType = "Income", IsSystem = false, ParentId = 1, Color = Colors.Get(74) },
                    new { Id = 75, CategoryName = "Interest", CategoryType = "Income", IsSystem = false, ParentId = 1, Color = Colors.Get(75) },
                    new { Id = 76, CategoryName = "Cheque deposit", CategoryType = "Income", IsSystem = false, ParentId = 1, Color = Colors.Get(76) },
                    new { Id = 77, CategoryName = "Cash deposit", CategoryType = "Income", IsSystem = false, ParentId = 1, Color = Colors.Get(77) },
                    new { Id = 78, CategoryName = "Freelancer", CategoryType = "Income", IsSystem = false, ParentId = 1, Color = Colors.Get(78) },
                    new { Id = 79, CategoryName = "Salary M", CategoryType = "Income", IsSystem = false, ParentId = 1, Color = Colors.Get(79) },

                    new { Id = 2, CategoryName = "Taxes", CategoryType = "Expense", IsSystem = false, Color = Colors.Get(2) },
                    new { Id = 46, CategoryName = "Income Tax", CategoryType = "Expense", IsSystem = false, ParentId = 2, Color = Colors.Get(46) },
                    new { Id = 104, CategoryName = "Goverment Taxes", CategoryType = "Expense", IsSystem = false, ParentId = 2, Color = Colors.Get(104) },

                    new { Id = 3, CategoryName = "Pet Care", CategoryType = "Expense", IsSystem = false, Color = Colors.Get(3) },
                    new { Id = 48, CategoryName = "Veterinarian", CategoryType = "Expense", IsSystem = false, ParentId = 3, Color = Colors.Get(48) },
                    new { Id = 49, CategoryName = "Supplies", CategoryType = "Expense", IsSystem = false, ParentId = 3, Color = Colors.Get(49) },
                    new { Id = 50, CategoryName = "Food", CategoryType = "Expense", IsSystem = false, ParentId = 3, Color = Colors.Get(50) },

                    new { Id = 4, CategoryName = "Fees & Charges", CategoryType = "Expense", IsSystem = false, Color = Colors.Get(4) },
                    new { Id = 51, CategoryName = "Bank Fee", CategoryType = "Expense", IsSystem = false, ParentId = 4, Color = Colors.Get(51) },
                    new { Id = 106, CategoryName = "Goverment Fees", CategoryType = "Expense", IsSystem = false, ParentId = 4, Color = Colors.Get(106) },
                    new { Id = 108, CategoryName = "Postal Fees", CategoryType = "Expense", IsSystem = false, ParentId = 4, Color = Colors.Get(108) },

                    new { Id = 5, CategoryName = "Travel", CategoryType = "Expense", IsSystem = false, Color = Colors.Get(5) },
                    new { Id = 52, CategoryName = "Boat", CategoryType = "Expense", IsSystem = false, ParentId = 5, Color = Colors.Get(52) },
                    new { Id = 53, CategoryName = "Holiday", CategoryType = "Expense", IsSystem = false, ParentId = 5, Color = Colors.Get(53) },
                    new { Id = 55, CategoryName = "Rental Car", CategoryType = "Expense", IsSystem = false, ParentId = 5, Color = Colors.Get(55) },
                    new { Id = 56, CategoryName = "Hotel", CategoryType = "Expense", IsSystem = false, ParentId = 5, Color = Colors.Get(56) },
                    new { Id = 57, CategoryName = "Flights", CategoryType = "Expense", IsSystem = false, ParentId = 5, Color = Colors.Get(57) },
                    new { Id = 1134, CategoryName = "Toll Road", CategoryType = "Expense", IsSystem = false, ParentId = 5, Color = Colors.Get(1134) },
                    new { Id = 41, CategoryName = "Taxi", CategoryType = "Expense", IsSystem = false, ParentId = 5, Color = Colors.Get(41) },
                    new { Id = 58, CategoryName = "Public Transport", CategoryType = "Expense", IsSystem = false, ParentId = 5, Color = Colors.Get(58) },

                    new { Id = 6, CategoryName = "Auto", CategoryType = "Expense", IsSystem = false, Color = Colors.Get(6) },
                    new { Id = 38, CategoryName = "Fuel", CategoryType = "Expense", IsSystem = false, ParentId = 6, Color = Colors.Get(38) },
                    new { Id = 40, CategoryName = "Parking", CategoryType = "Expense", IsSystem = false, ParentId = 6, Color = Colors.Get(40) },
                    new { Id = 59, CategoryName = "Car Service & Parts", CategoryType = "Expense", IsSystem = false, ParentId = 6, Color = Colors.Get(59) },
                    new { Id = 109, CategoryName = "Car Taxes", CategoryType = "Expense", IsSystem = false, ParentId = 6, Color = Colors.Get(109) },

                    new { Id = 7, CategoryName = "Bills & Utilities", CategoryType = "Expense", IsSystem = false, Color = Colors.Get(7) },
                    new { Id = 21, CategoryName = "Online Services", CategoryType = "Expense", IsSystem = false, ParentId = 7, Color = Colors.Get(21) },
                    new { Id = 22, CategoryName = "Water", CategoryType = "Expense", IsSystem = false, ParentId = 7, Color = Colors.Get(22) },
                    new { Id = 23, CategoryName = "Electricity", CategoryType = "Expense", IsSystem = false, ParentId = 7, Color = Colors.Get(23) },
                    new { Id = 24, CategoryName = "Mobile Phone", CategoryType = "Expense", IsSystem = false, ParentId = 7, Color = Colors.Get(24) },
                    new { Id = 25, CategoryName = "Internet", CategoryType = "Expense", IsSystem = false, ParentId = 7, Color = Colors.Get(25) },
                    new { Id = 26, CategoryName = "Home Phone", CategoryType = "Expense", IsSystem = false, ParentId = 7, Color = Colors.Get(26) },
                    new { Id = 27, CategoryName = "Television", CategoryType = "Expense", IsSystem = false, ParentId = 7, Color = Colors.Get(27) },

                    new { Id = 8, CategoryName = "Gifts & Donations", CategoryType = "Expense", IsSystem = false, Color = Colors.Get(8) },
                    new { Id = 28, CategoryName = "Donation", CategoryType = "Expense", IsSystem = false, ParentId = 8, Color = Colors.Get(28) },
                    new { Id = 29, CategoryName = "Charity", CategoryType = "Expense", IsSystem = false, ParentId = 8, Color = Colors.Get(29) },
                    new { Id = 30, CategoryName = "Gift", CategoryType = "Expense", IsSystem = false, ParentId = 8, Color = Colors.Get(30) },

                    new { Id = 9, CategoryName = "Expenses", CategoryType = "Expense", IsSystem = false, Color = Colors.Get(9) },
                    new { Id = 42, CategoryName = "Investment", CategoryType = "Expense", IsSystem = false, ParentId = 9, Color = Colors.Get(42) },
                    new { Id = 43, CategoryName = "Monthly Budget", CategoryType = "Expense", IsSystem = false, ParentId = 9, Color = Colors.Get(43) },
                    new { Id = 44, CategoryName = "Others", CategoryType = "Expense", IsSystem = false, ParentId = 9, Color = Colors.Get(44) },
                    new { Id = 45, CategoryName = "ATM", CategoryType = "Expense", IsSystem = false, ParentId = 9, Color = Colors.Get(45) },
                    new { Id = 60, CategoryName = "Freelancer", CategoryType = "Expense", IsSystem = false, ParentId = 9, Color = Colors.Get(60) },

                    new { Id = 10, CategoryName = "Food & Dining", CategoryType = "Expense", IsSystem = false, Color = Colors.Get(10) },
                    new { Id = 31, CategoryName = "Alcohol & Bars", CategoryType = "Expense", IsSystem = false, ParentId = 10, Color = Colors.Get(31) },
                    new { Id = 32, CategoryName = "Restaurants", CategoryType = "Expense", IsSystem = false, ParentId = 10, Color = Colors.Get(32) },
                    new { Id = 33, CategoryName = "Fast Food", CategoryType = "Expense", IsSystem = false, ParentId = 10, Color = Colors.Get(33) },
                    new { Id = 34, CategoryName = "Coffee shops", CategoryType = "Expense", IsSystem = false, ParentId = 10, Color = Colors.Get(34) },
                    new { Id = 35, CategoryName = "Groceries", CategoryType = "Expense", IsSystem = false, ParentId = 10, Color = Colors.Get(35) },

                    new { Id = 11, CategoryName = "Personal Care", CategoryType = "Expense", IsSystem = false, Color = Colors.Get(11) },
                    new { Id = 85, CategoryName = "Spa & Massage", CategoryType = "Expense", IsSystem = false, ParentId = 11, Color = Colors.Get(85) },
                    new { Id = 86, CategoryName = "Laundry", CategoryType = "Expense", IsSystem = false, ParentId = 11, Color = Colors.Get(86) },
                    new { Id = 87, CategoryName = "Hair", CategoryType = "Expense", IsSystem = false, ParentId = 11, Color = Colors.Get(87) },

                    new { Id = 12, CategoryName = "Shopping", CategoryType = "Expense", IsSystem = false, Color = Colors.Get(12) },
                    new { Id = 88, CategoryName = "Online Purchase", CategoryType = "Expense", IsSystem = false, ParentId = 12, Color = Colors.Get(88) },
                    new { Id = 89, CategoryName = "Amazon", CategoryType = "Expense", IsSystem = false, ParentId = 12, Color = Colors.Get(89) },
                    new { Id = 90, CategoryName = "Sporting Goods", CategoryType = "Expense", IsSystem = false, ParentId = 12, Color = Colors.Get(90) },
                    new { Id = 91, CategoryName = "Hobbies", CategoryType = "Expense", IsSystem = false, ParentId = 12, Color = Colors.Get(91) },
                    new { Id = 92, CategoryName = "Books", CategoryType = "Expense", IsSystem = false, ParentId = 12, Color = Colors.Get(92) },
                    new { Id = 93, CategoryName = "Shoes", CategoryType = "Expense", IsSystem = false, ParentId = 12, Color = Colors.Get(93) },
                    new { Id = 94, CategoryName = "Clothing", CategoryType = "Expense", IsSystem = false, ParentId = 12, Color = Colors.Get(94) },

                    new { Id = 13, CategoryName = "Insurance", CategoryType = "Expense", IsSystem = false, Color = Colors.Get(13) },
                    new { Id = 95, CategoryName = "Life Insurance", CategoryType = "Expense", IsSystem = false, ParentId = 13, Color = Colors.Get(95) },
                    new { Id = 96, CategoryName = "Home Insurance", CategoryType = "Expense", IsSystem = false, ParentId = 13, Color = Colors.Get(96) },
                    new { Id = 97, CategoryName = "Health Insurance", CategoryType = "Expense", IsSystem = false, ParentId = 13, Color = Colors.Get(97) },
                    new { Id = 98, CategoryName = "Car Insurance", CategoryType = "Expense", IsSystem = false, ParentId = 13, Color = Colors.Get(98) },

                    new { Id = 14, CategoryName = "Loans", CategoryType = "Expense", IsSystem = false, Color = Colors.Get(14) },
                    new { Id = 83, CategoryName = "Mortgage", CategoryType = "Expense", IsSystem = false, ParentId = 14, Color = Colors.Get(83) },
                    new { Id = 99, CategoryName = "Student loans", CategoryType = "Expense", IsSystem = false, ParentId = 14, Color = Colors.Get(99) },
                    new { Id = 100, CategoryName = "Credit cards", CategoryType = "Expense", IsSystem = false, ParentId = 14, Color = Colors.Get(100) },
                    new { Id = 101, CategoryName = "Car Loan", CategoryType = "Expense", IsSystem = false, ParentId = 14, Color = Colors.Get(101) },

                    new { Id = 15, CategoryName = "Household", CategoryType = "Expense", IsSystem = false, Color = Colors.Get(15) },
                    new { Id = 63, CategoryName = "House Cleaning", CategoryType = "Expense", IsSystem = false, ParentId = 15, Color = Colors.Get(63) },
                    new { Id = 64, CategoryName = "Furniture", CategoryType = "Expense", IsSystem = false, ParentId = 15, Color = Colors.Get(64) },
                    new { Id = 65, CategoryName = "Maintenance", CategoryType = "Expense", IsSystem = false, ParentId = 15, Color = Colors.Get(65) },
                    new { Id = 66, CategoryName = "Construction", CategoryType = "Expense", IsSystem = false, ParentId = 15, Color = Colors.Get(66) },
                    new { Id = 80, CategoryName = "Rent", CategoryType = "Expense", IsSystem = false, ParentId = 15, Color = Colors.Get(80) },
                    new { Id = 82, CategoryName = "Appliances", CategoryType = "Expense", IsSystem = false, ParentId = 15, Color = Colors.Get(82) },

                    new { Id = 16, CategoryName = "Education", CategoryType = "Expense", IsSystem = false, Color = Colors.Get(16) },
                    new { Id = 47, CategoryName = "Kids Activities Fees", CategoryType = "Expense", IsSystem = false, ParentId = 16, Color = Colors.Get(47) },
                    new { Id = 67, CategoryName = "Courses", CategoryType = "Expense", IsSystem = false, ParentId = 16, Color = Colors.Get(67) },
                    new { Id = 68, CategoryName = "School Supplies", CategoryType = "Expense", IsSystem = false, ParentId = 16, Color = Colors.Get(68) },
                    new { Id = 69, CategoryName = "School Fees", CategoryType = "Expense", IsSystem = false, ParentId = 16, Color = Colors.Get(69) },

                    new { Id = 17, CategoryName = "Entertainment", CategoryType = "Expense", IsSystem = false, Color = Colors.Get(17) },
                    new { Id = 70, CategoryName = "Kids Activities", CategoryType = "Expense", IsSystem = false, ParentId = 17, Color = Colors.Get(70) },
                    new { Id = 71, CategoryName = "Music", CategoryType = "Expense", IsSystem = false, ParentId = 17, Color = Colors.Get(71) },
                    new { Id = 72, CategoryName = "Arts", CategoryType = "Expense", IsSystem = false, ParentId = 17, Color = Colors.Get(72) },
                    new { Id = 73, CategoryName = "Cinema", CategoryType = "Expense", IsSystem = false, ParentId = 17, Color = Colors.Get(73) },

                    new { Id = 18, CategoryName = "Health & Fitness", CategoryType = "Expense", IsSystem = false, Color = Colors.Get(18) },
                    new { Id = 36, CategoryName = "Kids Sports", CategoryType = "Expense", IsSystem = false, ParentId = 18, Color = Colors.Get(36) },
                    new { Id = 37, CategoryName = "Sports", CategoryType = "Expense", IsSystem = false, ParentId = 18, Color = Colors.Get(37) },
                    new { Id = 39, CategoryName = "Gym", CategoryType = "Expense", IsSystem = false, ParentId = 18, Color = Colors.Get(39) },
                    new { Id = 61, CategoryName = "Pharmacy", CategoryType = "Expense", IsSystem = false, ParentId = 18, Color = Colors.Get(61) },
                    new { Id = 62, CategoryName = "Doctor", CategoryType = "Expense", IsSystem = false, ParentId = 18, Color = Colors.Get(62) },
                    new { Id = 81, CategoryName = "Eyecare", CategoryType = "Expense", IsSystem = false, ParentId = 18, Color = Colors.Get(81) },
                    new { Id = 84, CategoryName = "Dentist", CategoryType = "Expense", IsSystem = false, ParentId = 18, Color = Colors.Get(84) },

                    new { Id = 19, CategoryName = "Transfer", CategoryType = "Transfer", IsSystem = true, Color = Colors.Get(19) }
            );
        }
    }
}
