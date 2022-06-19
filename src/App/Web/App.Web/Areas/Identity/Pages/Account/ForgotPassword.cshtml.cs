// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
namespace App.Web.Areas.Identity.Pages.Account
{
#nullable disable

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using App.Common;
    using App.Data.Models;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;

    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailSender emailSender;

        public ForgotPasswordModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [MaxLength(ValidationConstants.MaxUsernameLength)]
            [DataType(DataType.Text)]
            public string Username { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userManager.FindByNameAsync(this.Input.Username);
                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return this.RedirectToPage("./ForgotPasswordConfirmation");
                }

                string newPassword = this.GenerateRandomPassword();
                var resetToken = await this.userManager.GeneratePasswordResetTokenAsync(user);
                await this.userManager.ResetPasswordAsync(user, resetToken, newPassword);

                var loginUrl = this.Url.Page(
                    "/Account/Login",
                    pageHandler: null,
                    values: new { area = "Identity" },
                    protocol: this.Request.Scheme);

                await this.emailSender.SendEmailAsync(
                    user.Email,
                    "Reset Password",
                    $"Your new password is {newPassword}. Please log in to your profile with the new password and change it password by <a href='{HtmlEncoder.Default.Encode(loginUrl)}'>clicking here</a>.");

                return this.RedirectToPage("./ForgotPasswordConfirmation");
            }

            return this.Page();
        }

        private string GenerateRandomPassword()
        {
            var lowerLetters = Enumerable.Range('a', 'z' - 'a' + 1).Select(c => (char)c).ToList();
            var upperLetters = Enumerable.Range('A', 'Z' - 'A' + 1).Select(c => (char)c).ToList();
            var digits = Enumerable.Range('0', '9' - '0' + 1).Select(c => (char)c).ToList();
            var specialSigns = new[] { '%', '.', '/', '-', '@', '&' };

            Random random = new Random();
            char[] password = new char[10];

            // Add one upper letter
            this.SetSignAtRandomPosition(password, upperLetters);

            // Add one digit
            bool wasAdded;
            do
            {
                wasAdded = this.SetSignAtRandomPosition(password, digits);
            }
            while (!wasAdded);

            // Add one digit
            do
            {
                wasAdded = this.SetSignAtRandomPosition(password, specialSigns);
            }
            while (!wasAdded);

            // Add random count special sign
            var count = this.GetRandomIndex(password) + 1;
            for (int i = 0; i < count; i++)
            {
                this.SetSignAtRandomPosition(password, specialSigns);
            }

            List<char> allowedCharacters = new List<char>();
            allowedCharacters.AddRange(lowerLetters);
            allowedCharacters.AddRange(upperLetters);
            allowedCharacters.AddRange(digits);
            allowedCharacters.AddRange(specialSigns);

            for (int i = 0; i < password.Length; i++)
            {
                if (password[i] == default)
                {
                    var allowedCharactersIndex = this.GetRandomIndex(allowedCharacters);
                    password[i] = allowedCharacters[allowedCharactersIndex];
                }
            }

            return string.Join(string.Empty, password);
        }

        private bool SetSignAtRandomPosition(IList<char> password, IList<char> signs)
        {
            var passwordIndex = this.GetRandomIndex(password);
            var signsIndex = this.GetRandomIndex(signs);
            if (password[passwordIndex] != default)
            {
                return false;
            }

            password[passwordIndex] = signs[signsIndex];
            return true;
        }

        private int GetRandomIndex(ICollection<char> allowedCharacters)
        {
            Random random = new Random();
            return random.Next(0, allowedCharacters.Count);
        }
    }
}
