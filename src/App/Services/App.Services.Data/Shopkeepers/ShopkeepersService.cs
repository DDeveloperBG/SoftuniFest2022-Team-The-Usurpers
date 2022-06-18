namespace App.Services.Data.UpdateRecords
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using App.Common;
    using App.Data.Common.Repositories;
    using App.Data.Models;
    using App.Services.Data.DTOs;
    using App.Services.Mapping;
    using App.Web.ViewModels.Shopkeeper;

    using Microsoft.AspNetCore.Identity;

    public class ShopkeepersService : IShopkeepersService
    {
        private readonly IRepository<Shopkeeper> shopkeepers;
        private readonly IRepository<Discount> discounts;
        private readonly UserManager<ApplicationUser> userManager;

        public ShopkeepersService(
            IRepository<Shopkeeper> shopkeepers,
            IRepository<Discount> discounts,
            UserManager<ApplicationUser> userManager)
        {
            this.shopkeepers = shopkeepers;
            this.discounts = discounts;
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
                };

                await this.shopkeepers.AddAsync(shopkeeper);
            }

            await this.shopkeepers.SaveChangesAsync();

            foreach (var user in users)
            {
                await this.userManager.AddToRoleAsync(user, GlobalConstants.ShopkeeperRoleName);
            }
        }

        public Task ChangeHasToChangePasswordStateAsync(string userId)
        {
            var shopkeeper = this.shopkeepers
                .All()
                .Where(x => x.UserId == userId)
                .Single();

            shopkeeper.HasToChangePassword = false;

            return this.shopkeepers.SaveChangesAsync();
        }

        public bool HasToChangePassword(ApplicationUser user)
        {
            return this.shopkeepers
                .AllAsNoTracking()
                .Where(x => x.UserId == user.Id)
                .Select(x => x.HasToChangePassword)
                .Single();
        }

        public async Task AddDiscountAsync(DiscountInputModel input, string userId)
        {
            var shopkeeperId = this.shopkeepers
                .AllAsNoTracking()
                .Where(x => x.UserId == userId)
                .Select(x => x.Id)
                .Single();

            Discount discount = new Discount
            {
                DiscountSize = input.DiscountSize,
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                ShopkeeperId = shopkeeperId,
            };

            await this.discounts.AddAsync(discount);
            await this.discounts.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllDiscoundsMapped<T>(AllDiscountsFilterInputModel filter, string userId)
        {
            var query = this.discounts
                .AllAsNoTracking()
                .Where(x => x.Shopkeeper.UserId == userId);

            if (filter.Status > -1)
            {
                var status = (DiscountStatus)filter.Status;
                if (Enum.IsDefined<DiscountStatus>(status))
                {
                    if (status == DiscountStatus.Expired)
                    {
                        var utcNow = DateTime.UtcNow;
                        query = query
                            .Where(x => x.EndDate < utcNow);
                    }
                    else
                    {
                        query = query
                            .Where(x => x.Status == status);
                    }
                }
            }

            if (filter.StartDate != default && filter.EndDate != default)
            {
                query = query
                       .Where(x => x.StartDate >= filter.StartDate)
                       .Where(x => x.EndDate <= filter.EndDate);
            }

            return query
                .To<T>()
                .ToList();
        }
    }
}
