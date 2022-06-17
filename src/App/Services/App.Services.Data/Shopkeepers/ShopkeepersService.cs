namespace App.Services.Data.UpdateRecords
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using App.Data.Common.Repositories;
    using App.Data.Models;
    using App.Services.Data.DTOs;

    public class ShopkeepersService : IShopkeepersService
    {
        private readonly IRepository<Shopkeeper> shopkeepers;

        public ShopkeepersService(IRepository<Shopkeeper> shopkeepers)
        {
            this.shopkeepers = shopkeepers;
        }

        public async Task AddNewRecordsAsync(IEnumerable<ShopkeeperNewRecordDTO> newRecords)
        {
            foreach (var record in newRecords)
            {
                var user = new ApplicationUser
                {
                    UserName = record.Username,
                    Email = record.Email,
                    PhoneNumber = record.PhoneNumber.ToString(),
                };

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
        }
    }
}
