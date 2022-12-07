using EnergyPlatform.Repository.Entitys;
using EnergyPlatformProgram.BusinessLogic.Constants;
using EnergyPlatformProgram.Repository.Models;
using EnergyPlatformProject.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyPlatform.BusinessLogic.Data
{
    public static class SeedDB

    {
        public async static Task Initialize(ApplicationDbContext context, RoleManager<IdentityRole<Guid>> roleManager, UserManager<UserEntity> userManager)
        {
            context.Database.EnsureCreated();

            await CreateRolesAsync(roleManager, context);
            await CreateAdminAsync(context, userManager);
           // await CreateConsumptionAsync(context);
        }

        #region Private


        private async static Task CreateRolesAsync(RoleManager<IdentityRole<Guid>> roleManager, ApplicationDbContext context)
        {
            var isUserRoleExistent = await roleManager.RoleExistsAsync(RoleConstants.UserRole);

            var isAdminRoleExistent = await roleManager.RoleExistsAsync(RoleConstants.AdminRole);


            if (!isUserRoleExistent)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(RoleConstants.UserRole));
            }

            if (!isAdminRoleExistent)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(RoleConstants.AdminRole));
            }

            context.SaveChanges();
        }

        private static async Task CreateAdminAsync(ApplicationDbContext context, UserManager<UserEntity> userManager)
        {
            //var result = await context.Users.FirstOrDefaultAsync(e => e.Email.Equals("miruhulpe+Admin@yahoo.com"));

            //if (result != null)
            //{
            //    return;
            //}

            var user = new UserEntity
            {

                Email = "miruhulpe+Admin2@yahoo.com",
                EmailConfirmed = true,
                LockoutEnabled = true,
                NormalizedEmail = "MIRUHULPE+ADMIN2@YAHOO.COM",
                NormalizedUserName = "MIRUNAHULPEADMIN2",
                UserName = "MirunaHulpeAdmin2",
                FirstName = "Miruna2",
                LastName = "Hulpe2"
            };

            var insertResult = await userManager.CreateAsync(user, "Aaa123!");

            if (!insertResult.Succeeded)
            {
                return;
            }

            context.SaveChanges();

            UserEntity dbUser = context.ApplicationUsers.FirstOrDefault(element => element.Email.Equals(user.Email));

            await userManager.AddToRoleAsync(user, RoleConstants.AdminRole);

            context.SaveChanges();
        }

        private static async Task CreateConsumptionAsync(ApplicationDbContext context)
        {
            var result = await context.Consumtion.AnyAsync();

           if (result)
            {
                return;
            }

            var consumptions = new List<ConsumptionEntity>();

            for (int i = 1; i < 24; i++)
            {
                var consumption = new ConsumptionEntity()
                {
                    Id = Guid.NewGuid(),
                    Consumtion = i * (float)5.2,
                    DeviceId = new Guid("1568B32E-DAD5-4EBA-F73A-08DABE386E3F"),
                    Date = new DateTime(2022, 07, 01, i, 0, 0)
                };

                consumptions.Add(consumption);
            }

            await context.Consumtion.AddRangeAsync(consumptions);
            context.SaveChanges();
        }

        #endregion

    }
}
