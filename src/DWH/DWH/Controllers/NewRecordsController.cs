namespace DWH.Controllers
{
    using DWH.Services.GetNewRecords;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/api/[action]")]
    public class NewRecordsController : ControllerBase
    {
        /// <summary>
        /// I left it as const because in real application
        /// it can be retrieved from example list of valid access tokens.
        /// </summary>
        private const string AccessToken = "204569a3-3fff-4e62-b3f7-39efaa36c1";
        private readonly INewRecordsService newRecordsService;

        public NewRecordsController(INewRecordsService newRecordsService)
        {
            this.newRecordsService = newRecordsService;
        }

        [HttpGet]
        public IActionResult GetNewRecords(
            [FromQuery]
            string accessToken,
            string type,
            DateTime lastCreationTime)
        {
            if (accessToken != AccessToken)
            {
                return this.BadRequest("Access token is invalid!");
            }

            var newRecordsAsJSON = this.newRecordsService.GetNewRecordsAsJSON(type, lastCreationTime);

            return this.Ok(newRecordsAsJSON);
        }
    }
}
