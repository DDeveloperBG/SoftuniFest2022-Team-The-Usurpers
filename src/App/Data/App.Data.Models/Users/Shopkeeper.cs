namespace App.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using App.Data.Common.Models;

    public class Shopkeeper : BaseModel<string>
    {
        public Shopkeeper()
        {
            this.Id = Guid.NewGuid().ToString();
            this.HasToChangePassword = true;
        }

        public DateTime RegisteredOn { get; set; }

        public bool HasToChangePassword { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
