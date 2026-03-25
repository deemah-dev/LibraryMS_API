using Library.BLL.Interfaces;
using Library.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/Fines")]
    [ApiController]
    public class FinesController : ControllerBase
    {
        private IFinesService finesService;

        public FinesController(IFinesService finesService)
        {
            this.finesService = finesService;
        }

        //_______________________________________________________________________________
        [HttpGet("GetAllFines")]
        //[Authorize(Roles ="Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Fine>> GetAllFines()
        {
            IEnumerable<Fine>? fines = finesService.GetAllFines();

            if (fines is null)
                return NotFound(new { message = "No fines found in the database." });

            if (fines.Count() == 0)
                return NoContent();

            return Ok(new { message = "Fines retrieved successfully.", data = fines, count = fines.Count() });
        }
    }
}
