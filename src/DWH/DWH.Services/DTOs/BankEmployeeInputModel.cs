using DWH.Common;
using System.ComponentModel.DataAnnotations;

namespace DWH.Services.DTOs
{
    public class BankEmployeeInputModel
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
    }
}
