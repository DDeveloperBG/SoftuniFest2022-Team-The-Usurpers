namespace App.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using App.Data.Common.Models;

    public class CardHolder : BaseModel<string>
    {
        public CardHolder()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public ulong PaymentCardNumber { get; set; }

        public DateTime PaymentCardValidUntil { get; set; }

        public DateTime RegisteredOn { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
