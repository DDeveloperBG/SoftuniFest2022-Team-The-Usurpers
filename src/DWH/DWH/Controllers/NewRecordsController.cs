namespace DWH.Controllers
{
    using DWH.Services.GetNewRecords;

    using Microsoft.AspNetCore.Mvc;
    using System.Globalization;

    [ApiController]
    [Route("/api/[action]")]
    public class NewRecordsController : ControllerBase
    {
        /// <summary>
        /// AccessToken is const because in real application
        /// it can be retrieved from example list of valid access tokens.
        /// For now it is good enough abstraction.
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
            string lastTimeAsString)
        {
            if (accessToken != AccessToken)
            {
                return this.BadRequest("Access token is invalid!");
            }

            var lastTime = DateTime.ParseExact(lastTimeAsString, "s",
                              CultureInfo.InvariantCulture,
                              DateTimeStyles.AdjustToUniversal);
            var newRecordsAsJSON = this.newRecordsService.GetNewRecordsAsJSON(type, lastTime);

            return this.Ok(newRecordsAsJSON);
        }
    }
}
