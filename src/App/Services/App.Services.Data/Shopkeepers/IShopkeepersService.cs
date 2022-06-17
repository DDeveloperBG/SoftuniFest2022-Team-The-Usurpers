﻿namespace App.Services.Data.UpdateRecords
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using App.Data.Models;
    using App.Services.Data.DTOs;

    public interface IShopkeepersService
    {
        Task AddNewRecordsAsync(IEnumerable<ShopkeeperNewRecordDTO> newRecords);

        bool HasToChangePassword(ApplicationUser user);
    }
}