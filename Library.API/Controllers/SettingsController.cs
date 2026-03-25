using Library.BLL.Interfaces;
using Library.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/Settings")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private ISettingsService settingsService;

        public SettingsController(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        //_______________________________________________________________________________
        [HttpGet("GetSettings")]
        //[Authorize(Roles ="Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Settings> GetSettings()
        {
            Settings? settings = settingsService.GetSettings();

            if (settings is null)
                return NotFound(new { message = "Settings not found in the database." });

            return Ok(new { message = "Settings retrieved successfully.", data = settings });
        }

        //_______________________________________________________________________________
        [HttpPut("UpdateDefaultBorrowDays")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult UpdateDefaultBorrowDays(int defaultBorrowDays)
        {
            if (defaultBorrowDays < 1)
                return BadRequest(new { message = "Default borrow days must be greater than 0." });

            if (defaultBorrowDays > 365)
                return BadRequest(new { message = "Default borrow days cannot exceed 365 days." });

            bool updatedSuccessfully = settingsService.UpdateDefaultBorrowDays(defaultBorrowDays);

            if (updatedSuccessfully)
                return Ok(new { message = "Default borrow days updated successfully.", defaultBorrowDays });
            else
                return Conflict(new { message = "Failed to update default borrow days. Please try again." });
        }

        //_______________________________________________________________________________
        [HttpPut("UpdateDefaultFinePerDay")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult UpdateDefaultFinePerDay(decimal defaultFinePerDay)
        {
            if (defaultFinePerDay < 0)
                return BadRequest(new { message = "Default fine per day cannot be negative." });

            if (defaultFinePerDay > 1000)
                return BadRequest(new { message = "Default fine per day is too high. Maximum allowed is 1000." });

            bool updatedSuccessfully = settingsService.UpdateDefaultFinePerDay(defaultFinePerDay);

            if (updatedSuccessfully)
                return Ok(new { message = "Default fine per day updated successfully.", defaultFinePerDay });
            else
                return Conflict(new { message = "Failed to update default fine per day. Please try again." });
        }

        //_______________________________________________________________________________
        [HttpPut("UpdateSettings")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult UpdateSettings(int defaultBorrowDays, decimal defaultFinePerDay)
        {
            if (defaultBorrowDays < 1)
                return BadRequest(new { message = "Default borrow days must be greater than 0." });

            if (defaultBorrowDays > 365)
                return BadRequest(new { message = "Default borrow days cannot exceed 365 days." });

            if (defaultFinePerDay < 0)
                return BadRequest(new { message = "Default fine per day cannot be negative." });

            if (defaultFinePerDay > 1000)
                return BadRequest(new { message = "Default fine per day is too high. Maximum allowed is 1000." });

            bool borrowDaysUpdated = settingsService.UpdateDefaultBorrowDays(defaultBorrowDays);
            bool finePerDayUpdated = settingsService.UpdateDefaultFinePerDay(defaultFinePerDay);

            if (borrowDaysUpdated && finePerDayUpdated)
                return Ok(new { message = "Settings updated successfully.", defaultBorrowDays, defaultFinePerDay });
            else if (borrowDaysUpdated || finePerDayUpdated)
                return Ok(new { message = "Partial update completed. Some settings may not have been updated.", defaultBorrowDays, defaultFinePerDay });
            else
                return Conflict(new { message = "Failed to update settings. Please try again." });
        }
    }
}
