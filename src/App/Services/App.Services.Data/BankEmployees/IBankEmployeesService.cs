namespace App.Services.Data.UpdateRecords
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using App.Services.Data.DTOs;
    using App.Web.ViewModels.BankEmployee;

    public interface IBankEmployeesService
    {
        Task AddNewRecordsAsync(IEnumerable<BankEmployeeNewRecordDTO> newRecords);

        IEnumerable<DiscountViewModel> GetAllDiscounts(string bankEmployeeUserId);

        Task ChangeDiscountStatusAsync(string discountId, string bankEmployeeUserId, sbyte vote);

        IEnumerable<T> GetAllShopkeepers<T>();

        IEnumerable<T> GetAllTerminals<T>();
    }
}
