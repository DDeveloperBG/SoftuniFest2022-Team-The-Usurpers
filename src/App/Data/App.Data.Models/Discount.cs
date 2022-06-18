namespace App.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using App.Data.Common.Models;

    public class Discount : BaseModel<string>
    {
        public Discount()
        {
            this.Id = Guid.NewGuid().ToString();
            this.DiscountVotes = new HashSet<DiscountVote>();
        }

        public int DiscountSize { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DiscountStatus Status { get; set; }

        [Required]
        [ForeignKey(nameof(Shopkeeper))]
        public string ShopkeeperId { get; set; }

        public Shopkeeper Shopkeeper { get; set; }

        public ICollection<DiscountVote> DiscountVotes { get; set; }
    }
}
