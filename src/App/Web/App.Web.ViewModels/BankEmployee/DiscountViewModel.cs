namespace App.Web.ViewModels.BankEmployee
{
    using System;

    using App.Data.Models;
    using App.Services.Mapping;

    public class DiscountViewModel : IMapFrom<Discount>
    {
        public string Id { get; set; }

        public int DiscountSize { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DiscountStatus Status { get; set; }

        public bool HasVoted { get; set; }
    }
}
