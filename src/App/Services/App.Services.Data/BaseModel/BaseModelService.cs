namespace App.Services.Data.BaseModel
{
    using System;
    using System.Linq;

    using App.Data;
    using App.Data.Common.Models;
    using App.Data.Repositories;

    public class BaseModelService : IBaseModelService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public BaseModelService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public DateTime GetLastCreatedTime<T>()
            where T : BaseModel<string>
        {
            var entity = new EfRepository<T>(this.applicationDbContext);
            return entity
                 .AllAsNoTracking()
                 .OrderByDescending(x => x.CreatedOn)
                 .Select(x => x.CreatedOn)
                 .FirstOrDefault();
        }
    }
}
