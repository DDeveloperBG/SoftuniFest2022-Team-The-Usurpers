namespace App.Web.ViewModels.CardHolder
{
    using System;

    using App.Data.Models;
    using App.Services.Mapping;

    using AutoMapper;

    public class ActiveDicountViewModel : IMapFrom<Discount>, IHaveCustomMappings
    {
        public string ShopkeeperUsername { get; set; }

        public int DiscountSize { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Discount, ActiveDicountViewModel>()
              .ForMember(x => x.ShopkeeperUsername, y => y.MapFrom(z => z.Shopkeeper.User.UserName));
        }
    }
}
