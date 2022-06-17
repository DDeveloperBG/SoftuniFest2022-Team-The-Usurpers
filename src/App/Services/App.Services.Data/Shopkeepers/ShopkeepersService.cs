namespace App.Services.Data.UpdateRecords
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using App.Common;
    using App.Data.Common.Repositories;
    using App.Data.Models;
    using App.Services.Data.DTOs;

    using Microsoft.AspNetCore.Identity;

    public class ShopkeepersService : IShopkeepersService
    {
        private readonly IRepository<Shopkeeper> shopkeepers;
        private readonly UserManager<ApplicationUser> userManager;

        public ShopkeepersService(
            IRepository<Shopkeeper> shopkeepers,
            UserManager<ApplicationUser> userManager)
        {
            this.shopkeepers = shopkeepers;
            this.userManager = userManager;
        }

        public async Task AddNewRecordsAsync(IEnumerable<ShopkeeperNewRecordDTO> newRecords)
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            foreach (var record in newRecords)
            {
                var user = new ApplicationUser
                {
                    UserName = record.Username,
                    Email = record.Email,
                    PhoneNumber = record.PhoneNumber,
                    PasswordHash = record.Password,
                };
                users.Add(user);

                var shopkeeper = new Shopkeeper
                {
                    Id = record.Id,
                    User = user,
                    RegisteredOn = record.RegisteredOn,
                    CreatedOn = record.CreatedOn,
                };

                await this.shopkeepers.AddAsync(shopkeeper);
            }

            await this.shopkeepers.SaveChangesAsync();

            foreach (var user in users)
            {
                await this.userManager.AddToRoleAsync(user, GlobalConstants.BankEmployeeRoleName);
            }
        }

        public bool HasToChangePassword(ApplicationUser user)
        {
            return this.shopkeepers
                .AllAsNoTracking()
                .Where(x => x.UserId == user.Id)
                .Select(x => x.HasToChangePassword)
                .Single();
        }
    }
}
