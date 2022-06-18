namespace App.Web.ViewModels.Register
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using App.Common;

    public class RegisterInputModel
    {
        [Required]
        [MaxLength(ValidationConstants.MaxUsernameLength)]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required]
        [MinLength(ValidationConstants.MinPasswordLength)]
        [MaxLength(ValidationConstants.MaxPasswordLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Text)]
        public ulong PaymentCardNumber { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string PaymentCardValidUntil { get; set; }
    }
}
