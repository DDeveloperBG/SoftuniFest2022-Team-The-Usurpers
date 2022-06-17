namespace App.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using App.Data.Common.Models;

    public class BankEmployee : BaseModel<string>
    {
        public BankEmployee()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
