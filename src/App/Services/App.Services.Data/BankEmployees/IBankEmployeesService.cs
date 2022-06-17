namespace App.Services.Data.UpdateRecords
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using App.Services.Data.DTOs;

    public interface IBankEmployeesService
    {
        Task AddNewRecordsAsync(IEnumerable<BankEmployeeNewRecordDTO> newRecords);
    }
}
