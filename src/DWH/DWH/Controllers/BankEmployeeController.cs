namespace DWH.Controllers
{
    using System.Threading.Tasks;

    using DWH.Data.Common.Repositories;
    using DWH.Data.Models;
    using DWH.Services.DTOs;
    using DWH.Services.Hash;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class BankEmployeeController : ControllerBase
    {
        private const string AccessToken = "204569a39efaa36c1";
        private readonly IRepository<BankEmployee> bankEmployees;
        private readonly IHashService hashService;

        public BankEmployeeController(
            IRepository<BankEmployee> bankEmployees,
            IHashService hashService)
        {
            this.bankEmployees = bankEmployees;
            this.hashService = hashService;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewAsync(
            [FromQuery]
            string accessToken,
            [FromBody]
            BankEmployeeInputModel input)
        {
            if (accessToken != AccessToken)
            {
                return this.BadRequest("Access token is invalid!");
            }

            var hashedPassword = this.hashService.HashText(input.Password);
            var bankEmployee = new BankEmployee
            {
                Username = input.Username,
                Email = input.Email,
                Password = hashedPassword,
            };

            await this.bankEmployees.AddAsync(bankEmployee);
            await this.bankEmployees.SaveChangesAsync();

            return this.Ok();
        }
    }
}
