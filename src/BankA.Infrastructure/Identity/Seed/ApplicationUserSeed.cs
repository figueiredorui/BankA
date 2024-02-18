using BankA.Domain;
using BankA.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace BankA.Infrastructure.Identity.Seed
{
    internal class ApplicationUserSeed
    {

        internal static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var administrator = new ApplicationUser { UserName = "admin", Email = "admin@localhost" };
            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "Admin1234!");
            }
            var demo = new ApplicationUser { UserName = "demo", Email = "demo@localhost" };
            if (userManager.Users.All(u => u.UserName != demo.UserName))
            {
                var result = await userManager.CreateAsync(demo, "Demo1234!");
            }

        }


    }
}
