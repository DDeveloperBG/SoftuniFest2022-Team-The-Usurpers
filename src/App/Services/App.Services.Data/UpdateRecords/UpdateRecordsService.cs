namespace App.Services.Data.UpdateRecords
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    using App.Data.Common.Models;
    using App.Data.Models;
    using App.Services.Data.BaseModel;
    using App.Services.Data.DTOs;
    using Newtonsoft.Json;

    public class UpdateRecordsService : IUpdateRecordsService
    {
        private readonly IBaseModelService baseModelService;
        private readonly string hwtUrl;
        private readonly string accessToken;

        public UpdateRecordsService(string hwtUrl, string accessToken)
        {
            this.hwtUrl = hwtUrl;
            this.accessToken = accessToken;
        }

        public void UpdateBankEmployees()
        {
            var newRecords = this.GetNewRecordsAsync<BankEmployee, BankEmployeeNewRecordDTO>();
            ;
        }

        public void UpdateShopkeepers()
        {
            var newRecords = this.GetNewRecordsAsync<Shopkeeper, ShopkeeperNewRecordDTO>();

        }

        public void UpdateTerminals()
        {
            var newRecords = this.GetNewRecordsAsync<Terminal, TerminalNewRecordDTO>();

        }

        public async Task<IEnumerable<TResponceType>> GetNewRecordsAsync<TEntityType, TResponceType>()
            where TEntityType : BaseModel<string>
            where TResponceType : class
        {
            HttpClient httpClient = new HttpClient();
            var lastCreationTime = this.baseModelService.GetLastCreatedTime<TEntityType>();

            string url = $"{this.hwtUrl}api/getNewRecords?from={lastCreationTime}&type={nameof(TEntityType)}&accessToken={this.accessToken}";
            var responce = await httpClient.GetAsync(url);

            var content = await responce.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<TResponceType>>(content);
        }
    }
}
