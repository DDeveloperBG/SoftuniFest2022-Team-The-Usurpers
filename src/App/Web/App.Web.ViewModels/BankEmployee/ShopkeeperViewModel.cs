namespace App.Web.ViewModels.BankEmployee
{
    using System;

    using App.Data.Models;
    using App.Services.Mapping;

    using AutoMapper;

    public class ShopkeeperViewModel : IMapFrom<Shopkeeper>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime RegisteredOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Shopkeeper, ShopkeeperViewModel>()
                .ForMember(x => x.Username, y => y.MapFrom(z => z.User.UserName))
                .ForMember(x => x.PhoneNumber, y => y.MapFrom(z => z.User.PhoneNumber))
                .ForMember(x => x.Email, y => y.MapFrom(z => z.User.Email));
        }
    }
}
