namespace App.Web.ViewModels.Shopkeeper
{
    using System;

    public class AllDiscountsFilterInputModel
    {
        public int Status { get; set; } = -1;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
