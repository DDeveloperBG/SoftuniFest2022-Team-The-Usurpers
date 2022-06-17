namespace DWH.Data.Models
{
    using System;

    public class Shopkeeper : User
    {
        public Shopkeeper()
        {
            this.Id = Guid.NewGuid().ToString();
            this.RegisteredOn = DateTime.UtcNow;
        }

        public string PhoneNumber { get; set; }

        public DateTime RegisteredOn { get; set; }
    }
}
