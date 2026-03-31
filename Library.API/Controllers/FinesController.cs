using Library.BLL.Interfaces;
using Library.Core.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Staff,Admin")]
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

        //_______________________________________________________________________________
        [HttpGet("GetFineById")]
        [Authorize(Roles = "Staff,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Fine> GetFineById(int fineId)
        {
            if (fineId < 1)
                return BadRequest(new { message = "Invalid fine ID. ID must be greater than 0." });

            IEnumerable<Fine>? allFines = finesService.GetAllFines();

            if (allFines is null)
                return NotFound(new { message = "No fines found." });

            Fine? fine = allFines.FirstOrDefault(f => f.FineId == fineId);

            if (fine is null)
                return NotFound(new { message = "Fine not found.", fineId });

            return Ok(new { message = "Fine retrieved successfully.", data = fine });
        }

        //_______________________________________________________________________________
        [HttpGet("GetUserFines")]
        [Authorize(Roles = "Staff,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Fine>> GetUserFines(int userId)
        {
            if (userId < 1)
                return BadRequest(new { message = "Invalid user ID. ID must be greater than 0." });

            IEnumerable<Fine>? allFines = finesService.GetAllFines();

            if (allFines is null)
                return NotFound(new { message = "No fines found in the database." });

            var userFines = allFines.Where(f => f.UserId == userId);

            if (userFines.Count() == 0)
                return NoContent();

            decimal totalFineAmount = userFines.Sum(f => f.FineAmount);

            return Ok(new { message = "User fines retrieved successfully.", data = userFines, count = userFines.Count(), totalAmount = totalFineAmount, userId });
        }
    }
}
