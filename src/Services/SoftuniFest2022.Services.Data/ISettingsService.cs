namespace SoftuniFest2022.Services.Data
{
    using System.Collections.Generic;

    public interface ISettingsService
    {
        int GetCount();

        IEnumerable<T> GetAll<T>();
    }
}
