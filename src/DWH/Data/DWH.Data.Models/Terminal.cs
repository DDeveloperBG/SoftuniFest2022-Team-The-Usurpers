﻿namespace DWH.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using DWH.Data.Common.Models;

    public class Terminal : BaseModel<string>
    {
        public Terminal()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [ForeignKey(nameof(Shopkeeper))]
        public string ShopkeeperId { get; set; }

        public Shopkeeper Shopkeeper { get; set; }
    }
}
