namespace App.Services.Data.UpdateRecords
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    using App.Data.Common.Models;
    using App.Data.Models;
    using App.Services.Data.BaseModel;
    using App.Services.Data.DTOs;
    using App.Web.ViewModels.DWH;

    using Newtonsoft.Json;

    public class UpdateRecordsService : IUpdateRecordsService
    {
        private readonly IBaseModelService baseModelService;
        private readonly DWHKeys dwhKeys;

        private readonly IBankEmployeesService bankEmployeesService;
        private readonly IShopkeepersService shopkeepersService;
        private readonly ITerminalService terminalService;

        public UpdateRecordsService(
            IBaseModelService baseModelService,
            DWHKeys dwhKeys,
            IBankEmployeesService bankEmployeesService,
            IShopkeepersService shopkeepersService,
            ITerminalService terminalService)
        {
            this.baseModelService = baseModelService;
            this.dwhKeys = dwhKeys;
            this.bankEmployeesService = bankEmployeesService;
            this.shopkeepersService = shopkeepersService;
            this.terminalService = terminalService;
        }

        public async Task UpdateRecordsAsync()
        {
            await this.UpdateBankEmployees();
            await this.UpdateTerminals();
            await this.UpdateShopkeepers();
        }

        public async Task UpdateBankEmployees()
        {
            var newRecords = await this.GetNewRecordsAsync<BankEmployee, BankEmployeeNewRecordDTO>();
            if (newRecords.Any())
            {
                await this.bankEmployeesService.AddNewRecordsAsync(newRecords);
            }
        }

        public async Task UpdateTerminals()
        {
            var newRecords = await this.GetNewRecordsAsync<Terminal, TerminalNewRecordDTO>();
            if (newRecords.Any())
            {
                await this.terminalService.AddNewRecordsAsync(newRecords);
            }
        }

        public async Task UpdateShopkeepers()
        {
            var newRecords = await this.GetNewRecordsAsync<Shopkeeper, ShopkeeperNewRecordDTO>();
            if (newRecords.Any())
            {
                await this.shopkeepersService.AddNewRecordsAsync(newRecords);
            }
        }

        private async Task<IEnumerable<TResponceType>> GetNewRecordsAsync<TEntityType, TResponceType>()
            where TEntityType : BaseModel<string>
            where TResponceType : class
        {
            HttpClient httpClient = new HttpClient();
            var lastCreationTime = this.baseModelService.GetLastCreatedTime<TEntityType>();
            string lastTimeAsString = lastCreationTime.ToString("s", CultureInfo.InvariantCulture);

            string url = $"{this.dwhKeys.Url}api/getNewRecords?lastTimeAsString={lastTimeAsString}&type={typeof(TEntityType).Name}&accessToken={this.dwhKeys.AccessToken}";
            var responce = await httpClient.GetAsync(url);

            var content = await responce.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<TResponceType>>(content);
        }
    }
}
