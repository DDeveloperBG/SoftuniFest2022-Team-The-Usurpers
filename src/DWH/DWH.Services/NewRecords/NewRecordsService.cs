namespace DWH.Services.GetNewRecords
{
    using DWH.Data;
    using DWH.Data.Models;
    using DWH.Data.Repositories;
    using Newtonsoft.Json;
    using Services.DTOs;

    public class NewRecordsService : INewRecordsService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public NewRecordsService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public string GetNewRecordsAsJSON(string type, DateTime lastCreationTime)
        {
            object newRecords = null;
            lastCreationTime = lastCreationTime.AddSeconds(1);
            if (type == "BankEmployee")
            {
                var entity = GetEfRepositoryOfType<BankEmployee>();

                newRecords = entity
                     .AllAsNoTracking()
                     .Where(x => x.CreatedOn > lastCreationTime)
                     .Select(x => new BankEmployeeNewRecordDTO
                     {
                         Id = x.Id,
                         Email = x.Email,
                         Password = x.Password,
                         Username = x.Username,
                     })
                     .ToList();
            }
            else if (type == "Shopkeeper")
            {
                var entity = GetEfRepositoryOfType<Shopkeeper>();

                newRecords = entity
                     .AllAsNoTracking()
                     .Where(x => x.CreatedOn > lastCreationTime)
                     .Select(x => new ShopkeeperNewRecordDTO
                     {
                         Id = x.Id,
                         Email = x.Email,
                         Password = x.Password,
                         Username = x.Username,
                         PhoneNumber = x.PhoneNumber,
                         RegisteredOn = x.RegisteredOn,
                     })
                     .ToList();
            }
            else if (type == "Terminal")
            {
                var entity = GetEfRepositoryOfType<Terminal>();

                newRecords = entity
                    .AllAsNoTracking()
                    .Where(x => x.CreatedOn > lastCreationTime)
                    .Select(x => new TerminalNewRecordDTO
                    {
                        TerminalId = x.Id,
                        ShopkeeperId = x.ShopkeeperId,
                    })
                    .ToList();
            }
            else
            {
                throw new ArgumentException("Type doesn't exist!");
            }

            return JsonConvert.SerializeObject(newRecords);
        }

        public EfRepository<T> GetEfRepositoryOfType<T>()
            where T : class
        {
            return new EfRepository<T>(this.applicationDbContext);
        }
    }
}
