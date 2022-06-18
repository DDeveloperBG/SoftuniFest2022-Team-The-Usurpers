namespace App.Services.Data.UpdateRecords
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using App.Common;
    using App.Data.Common.Repositories;
    using App.Data.Models;
    using App.Services.Data.DTOs;

    using Microsoft.AspNetCore.Identity;

    public class BankEmployeesService : IBankEmployeesService
    {
        private readonly IRepository<BankEmployee> bankEmployees;
        private readonly UserManager<ApplicationUser> userManager;

        public BankEmployeesService(
            IRepository<BankEmployee> bankEmployees,
            UserManager<ApplicationUser> userManager)
        {
            this.bankEmployees = bankEmployees;
            this.userManager = userManager;
        }

        public async Task AddNewRecordsAsync(IEnumerable<BankEmployeeNewRecordDTO> newRecords)
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            foreach (var record in newRecords)
            {
                var user = new ApplicationUser
                {
                    UserName = record.Username,
                    Email = record.Email,
                    PasswordHash = record.Password,
                };
                users.Add(user);

                var bankEmployee = new BankEmployee
                {
                    Id = record.Id,
                    User = user,
                };

                await this.bankEmployees.AddAsync(bankEmployee);
            }

            await this.bankEmployees.SaveChangesAsync();

            foreach (var user in users)
            {
                await this.userManager.AddToRoleAsync(user, GlobalConstants.BankEmployeeRoleName);
            }
        }
    }
}
