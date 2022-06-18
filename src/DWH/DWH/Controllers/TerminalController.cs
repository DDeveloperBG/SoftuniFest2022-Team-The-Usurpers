namespace DWH.Controllers
{
    using System.Threading.Tasks;

    using DWH.Data.Common.Repositories;
    using DWH.Data.Models;
    using DWH.Services.DTOs;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class TerminalController : ControllerBase
    {
        private const string AccessToken = "204569a39efaa36c1";
        private readonly IRepository<Terminal> terminals;

        public TerminalController(IRepository<Terminal> terminals)
        {
            this.terminals = terminals;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewAsync(
            [FromQuery]
            string accessToken,
            [FromBody]
            TerminalInputModel input)
        {
            if (accessToken != AccessToken)
            {
                return this.BadRequest("Access token is invalid!");
            }

            var terminal = new Terminal
            {
                ShopkeeperId = input.ShopkeeperId,
            };

            await this.terminals.AddAsync(terminal);
            await this.terminals.SaveChangesAsync();

            return this.Ok();
        }
    }
}
