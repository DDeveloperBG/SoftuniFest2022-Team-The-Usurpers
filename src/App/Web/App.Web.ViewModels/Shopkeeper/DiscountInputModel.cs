namespace App.Web.ViewModels.Shopkeeper
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class DiscountInputModel
    {
        [DisplayName("Percentage of whole price")]
        [Range(0, 100)]
        public int DiscountSize { get; set; }

        [DisplayName("Valid from")]
        public DateTime StartDate { get; set; }

        [DisplayName("Valid until")]
        public DateTime EndDate { get; set; }
    }
}
