namespace App.Services.Data.DTOs
{
    using System;

    public class TerminalNewRecordDTO
    {
        public string TerminalId { get; set; }

        public string ShopkeeperId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
