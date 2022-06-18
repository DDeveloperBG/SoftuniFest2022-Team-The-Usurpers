namespace App.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using App.Data.Common.Models;

    public class DiscountVote : BaseModel<int>
    {
        [Range(-1, 1)]
        public sbyte Vote { get; set; }

        [Required]
        [ForeignKey(nameof(Discount))]
        public string DiscountId { get; set; }

        public Discount Discount { get; set; }

        [Required]
        [ForeignKey(nameof(BankEmployeeUser))]
        public string BankEmployeeUserId { get; set; }

        public ApplicationUser BankEmployeeUser { get; set; }
    }
}
