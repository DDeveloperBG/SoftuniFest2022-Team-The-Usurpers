namespace DWH.Data.Models
{
    using System;

    public class BankEmployee : User
    {
        public BankEmployee()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
