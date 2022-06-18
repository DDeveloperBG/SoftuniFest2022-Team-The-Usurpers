namespace App.Web.ViewModels.BankEmployee
{
    using App.Data.Models;
    using App.Services.Mapping;

    public class TerminalViewModel : IMapFrom<Terminal>
    {
        public string TerminalId { get; set; }

        public string ShopkeeperId { get; set; }
    }
}
