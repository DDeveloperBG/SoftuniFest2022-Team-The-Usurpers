namespace App.Services.Data.UpdateRecords
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using App.Data.Models;
    using App.Services.Data.DTOs;
    using App.Web.ViewModels.Shopkeeper;

    public interface IShopkeepersService
    {
        Task AddNewRecordsAsync(IEnumerable<ShopkeeperNewRecordDTO> newRecords);

        bool HasToChangePassword(ApplicationUser user);

        Task ChangeHasToChangePasswordStateAsync(string userId);

        Task AddDiscountAsync(DiscountInputModel input, string userId);
    }
}
