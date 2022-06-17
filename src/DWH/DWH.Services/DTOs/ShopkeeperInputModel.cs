namespace DWH.DTOs
{
    using System.ComponentModel.DataAnnotations;

    using DWH.Common;

    public class ShopkeeperInputModel
    {
        [Required]
        [MaxLength(ValidationConstants.MaxUsernameLength)]
        public string Username { get; set; }

        [Required]
        [MinLength(ValidationConstants.MinPasswordLength)]
        [MaxLength(ValidationConstants.MaxPasswordLength)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
