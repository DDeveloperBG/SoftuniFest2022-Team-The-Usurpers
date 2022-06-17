namespace DWH.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using DWH.Common;
    using DWH.Data.Common.Models;

    public class User : BaseModel<string>
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
