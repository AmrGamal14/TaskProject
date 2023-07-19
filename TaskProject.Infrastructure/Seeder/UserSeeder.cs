using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Data.Entities.Identity;

namespace TaskProject.Infrastructure.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<User> _userManager)
        {
            var usersCount = await _userManager.Users.CountAsync();
            if (usersCount <= 0)
            {
                var defultuser = new User
                { UserName ="admin",
                    Email = "admin@project.com",
                    FullName="TaskProject",
                    PhoneNumber="123123",
                    Country="Egypt",
                    Adderss="Egypt",
                    EmailConfirmed=true,
                    PhoneNumberConfirmed=true
                };
                await _userManager.CreateAsync(defultuser,"Aaa123!");
                await _userManager.AddToRoleAsync(defultuser, "Admin");

        }
        }
    }
}
