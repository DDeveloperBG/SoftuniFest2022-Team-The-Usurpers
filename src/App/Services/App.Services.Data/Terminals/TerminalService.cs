namespace App.Services.Data.UpdateRecords
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using App.Data.Common.Repositories;
    using App.Data.Models;
    using App.Services.Data.DTOs;

    public class TerminalService : ITerminalService
    {
        private readonly IRepository<Terminal> terminals;

        public TerminalService(IRepository<Terminal> terminals)
        {
            this.terminals = terminals;
        }

        public async Task AddNewRecordsAsync(IEnumerable<TerminalNewRecordDTO> newRecords)
        {
            foreach (var record in newRecords)
            {
                var terminal = new Terminal
                {
                    Id = record.TerminalId,
                    ShopkeeperId = record.ShopkeeperId,
                    CreatedOn = record.CreatedOn,
                };

                await this.terminals.AddAsync(terminal);
            }

            await this.terminals.SaveChangesAsync();
        }
    }
}
