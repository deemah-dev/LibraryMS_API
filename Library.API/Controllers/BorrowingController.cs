using Library.BLL.Interfaces;
using Library.Core.Dtos.BorrowingBookDtos;
using Library.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/Borrowing")]
    [ApiController]
    public class BorrowingController : ControllerBase
    {
        private IBorrowingService borrowingService;

        public BorrowingController(IBorrowingService borrowingService)
        {
            this.borrowingService = borrowingService;
        }

        //_______________________________________________________________________________
        [HttpPost("BorrowBook")]
        //[Authorize(Roles ="User")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<ReadBorrowingRecordDto> BorrowBook(BorrowBookDto borrowBook)
        {
            if (borrowBook is null)
                return BadRequest(new { message = "Borrowing record cannot be null." });

            if (borrowBook.UserId < 1)
                return BadRequest(new { message = "Valid user ID is required." });

            if (borrowBook.CopyId < 1)
                return BadRequest(new { message = "Valid copy ID is required." });

            if (borrowBook.BorrowingDate == default)
                return BadRequest(new { message = "Valid borrowing date is required." });

            int newRecordId = borrowingService.BorrowBook(borrowBook);

            if (newRecordId == -1)
                return Conflict(new { message = "Failed to create borrowing record. Please try again." });

            return Created(nameof(GetBorrowingRecord), new { message = "Book borrowed successfully.", data = borrowBook, recordId = newRecordId });
        }

        //_______________________________________________________________________________
        [HttpPut("ReturnBook")]
        //[Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult ReturnBook(ReturnBookDto returnBook)
        {
            if (returnBook.BorrowingRecordId < 1)
                return BadRequest(new { message = "Invalid borrowing record ID. ID must be greater than 0." });

            if (returnBook.UserId < 1)
                return BadRequest(new { message = "Invalid user ID. ID must be greater than 0." });

            if (returnBook.ActualReturnDate == default)
                return BadRequest(new { message = "Valid return date is required." });

            bool returnedSuccessfully = borrowingService.ReturnBook(returnBook);

            if (returnedSuccessfully)
                return Ok(new { message = "Book returned successfully.", returnBook.BorrowingRecordId, returnBook.ActualReturnDate });
            else
                return Conflict(new { message = "Failed to process book return. Please try again." });
        }

        //_______________________________________________________________________________
        [HttpGet("CheckLateFine")]
        //[Authorize(Roles ="User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult CheckLateFine(int borrowingRecordId, DateTime actualReturnDate)
        {
            if (borrowingRecordId < 1)
                return BadRequest(new { message = "Invalid borrowing record ID. ID must be greater than 0." });

            if (actualReturnDate == default)
                return BadRequest(new { message = "Valid return date is required." });

            decimal fineAmount = borrowingService.HasLateFine(borrowingRecordId, actualReturnDate);

            if (fineAmount == 0)
                return Ok(new { message = "No late fine applicable.", hasLateFine = false, fineAmount, borrowingRecordId });

            return Ok(new { message = "Late fine has been calculated.", hasLateFine = true, fineAmount, borrowingRecordId });
        }

        //_______________________________________________________________________________
        [HttpGet("GetBorrowingRecord")]
        //[Authorize(Roles ="User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ReadBorrowingRecordDto> GetBorrowingRecord(int borrowingRecordId)
        {
            if (borrowingRecordId < 1)
                return BadRequest(new { message = "Invalid borrowing record ID. ID must be greater than 0." });

            ReadBorrowingRecordDto? record = borrowingService.GetBorrowingRecord(borrowingRecordId);

            if (record is null)
                return NotFound(new { message = "Borrowing record not found.", borrowingRecordId });

            return Ok(new { message = "Borrowing record retrieved successfully.", data = record });
        }

        //_______________________________________________________________________________
        [HttpGet("GetAllBorrowingRecords")]
        //[Authorize(Roles ="Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ReadBorrowingRecordDto>> GetAllBorrowingRecords()
        {
            IEnumerable<ReadBorrowingRecordDto>? records = borrowingService.GetAllBorrowingRecords();

            if (records is null)
                return NotFound(new { message = "No borrowing records found in the database." });

            if (records.Count() == 0)
                return NoContent();

            return Ok(new { message = "Borrowing records retrieved successfully.", data = records, count = records.Count() });
        }
    }
}
