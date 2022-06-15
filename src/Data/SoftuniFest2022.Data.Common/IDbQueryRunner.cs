namespace SoftuniFest2022.Data.Common
{
    using System;
    using System.Threading.Tasks;

    public interface IDbQueryRunner : IDisposable
    {
        Task RunQueryAsync(string query, params object[] parameters);
    }
}
