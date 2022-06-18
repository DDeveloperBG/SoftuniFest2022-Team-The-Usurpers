namespace App.Web.ViewModels.BankEmployee
{
    using App.Data.Models;
    using App.Services.Mapping;

    using AutoMapper;

    public class TerminalViewModel : IMapFrom<Terminal>, IHaveCustomMappings
    {
        public string TerminalId { get; set; }

        public string ShopkeeperId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Terminal, TerminalViewModel>()
                .ForMember(x => x.TerminalId, y => y.MapFrom(z => z.Id));
        }
    }
}
