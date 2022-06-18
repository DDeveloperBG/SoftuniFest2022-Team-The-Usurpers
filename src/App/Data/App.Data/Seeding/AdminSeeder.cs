namespace App.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using App.Common;
    using App.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class AdminSeeder : ISeeder
    {
        private const string AdminUsernamePath = "Admin:Username";
        private const string AdminEmailPath = "Admin:Email";
        private const string AdminPasswordKeyPath = "Admin:Password";

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetService<IConfiguration>();

            var adminUsername = configuration[AdminUsernamePath];
            if (!dbContext.Users.Any(user => user.UserName == adminUsername))
            {
                var adminEmail = configuration[AdminEmailPath];
                var adminPassword = configuration[AdminPasswordKeyPath];

                var user = new ApplicationUser { UserName = adminUsername, Email = adminEmail };
                var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                await userManager.CreateAsync(user, adminPassword);
                await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);

                var userId = await userManager.GetUserIdAsync(user);
                var code = await userManager.GenerateEmailConfirmationTokenAsync(user);

                await userManager.ConfirmEmailAsync(user, code);
            }
        }
    }
}
