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
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<Role> _roleManager)
        {
            var roleCount = await _roleManager.Roles.CountAsync();
            if (roleCount <= 0)
            {
                await _roleManager.CreateAsync(new Role()
                {
                    Name="Admin"
                });
                await _roleManager.CreateAsync(new Role()
                {
                    Name="User"
                });


            }
        }
    }
}
