namespace App.Services.Data.UpdateRecords
{
    using System;
    using System.Linq;

    using App.Data.Common.Repositories;
    using App.Data.Models;

    public class BankEmployeesService : IBankEmployeesService
    {
        private readonly IRepository<BankEmployee> bankEmployees;

        public BankEmployeesService(IRepository<BankEmployee> bankEmployees)
        {
            this.bankEmployees = bankEmployees;
        }

        public bool CheckIfLastRegisteredWasBeforeOneHour()
        {
            var lastCreatedOneTime = this.bankEmployees
                 .AllAsNoTracking()
                 .OrderBy(x => x.CreatedOn)
                 .Select(x => x.CreatedOn)
                 .FirstOrDefault();

            if (lastCreatedOneTime == default)
            {
                return true;
            }

            double minutesDiference = (DateTime.UtcNow - lastCreatedOneTime).TotalMinutes;

            return minutesDiference > 50;
        }
    }
}
