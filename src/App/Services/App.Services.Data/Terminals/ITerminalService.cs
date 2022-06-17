namespace App.Services.Data.UpdateRecords
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using App.Services.Data.DTOs;

    public interface ITerminalService
    {
        Task AddNewRecordsAsync(IEnumerable<TerminalNewRecordDTO> newRecords);
    }
}
