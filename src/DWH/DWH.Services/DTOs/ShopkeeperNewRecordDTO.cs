namespace DWH.Services.DTOs
{
    public class ShopkeeperNewRecordDTO
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public int PhoneNumber { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime RegisteredOn { get; set; }
    }
}
