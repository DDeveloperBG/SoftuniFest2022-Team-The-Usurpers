namespace App.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using App.Data.Common.Models;

    public class CardHolder : BaseModel<string>
    {
        public CardHolder()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int PaymentCardNumber { get; set; }

        public DateTime PaymentCardValidUntil { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
