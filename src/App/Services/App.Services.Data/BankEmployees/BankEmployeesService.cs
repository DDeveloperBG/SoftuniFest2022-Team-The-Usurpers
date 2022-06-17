namespace App.Services.Data.UpdateRecords
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using App.Data.Common.Repositories;
    using App.Data.Models;
    using App.Services.Data.DTOs;

    public class BankEmployeesService : IBankEmployeesService
    {
        private readonly IRepository<BankEmployee> bankEmployees;

        public BankEmployeesService(IRepository<BankEmployee> bankEmployees)
        {
            this.bankEmployees = bankEmployees;
        }

        public async Task AddNewRecordsAsync(IEnumerable<BankEmployeeNewRecordDTO> newRecords)
        {
            foreach (var record in newRecords)
            {
                var user = new ApplicationUser
                {
                    UserName = record.Username,
                    Email = record.Email,
                    PasswordHash = record.Password,
                };

                var bankEmployee = new BankEmployee
                {
                    Id = record.Id,
                    User = user,
                    CreatedOn = record.CreatedOn,
                };

                await this.bankEmployees.AddAsync(bankEmployee);
            }

            await this.bankEmployees.SaveChangesAsync();
        }
    }
}
