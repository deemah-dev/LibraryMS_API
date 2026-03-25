using Library.BLL.Interfaces;
using Library.Core.Dtos.BookCopyDtos;
using Library.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/BooksCopies")]
    [ApiController]
    public class BooksCopiesController : ControllerBase
    {
        private IBooksCopiesService copiesService;

        public BooksCopiesController(IBooksCopiesService copiesService)
        {
            this.copiesService = copiesService;
        }

        //_______________________________________________________________________________
        [HttpPost("AddBookCopy")]
        //[Authorize(Roles ="Admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<ReadBookCopyDto> AddBookCopy(AddBookCopyDto bookCopy)
        {
            if (bookCopy is null)
                return BadRequest(new { message = "Book copy cannot be null." });

            if (bookCopy.BookId < 1)
                return BadRequest(new { message = "Valid book ID is required." });

            int newCopyId = copiesService.AddCopy(bookCopy);

            if (newCopyId == -1)
                return Conflict(new { message = "Failed to create book copy. Please try again." });

            bookCopy.CopyId = newCopyId;
            return Created(nameof(GetBookCopy), new { message = "Book copy created successfully.", data = bookCopy, copyId = newCopyId });
        }

        //_______________________________________________________________________________
        [HttpDelete("{Id}", Name = "DeleteBookCopy")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteBookCopy(int Id)
        {
            if (Id < 1)
                return BadRequest(new { message = "Invalid copy ID. ID must be greater than 0." });

            bool deletedSuccessfully = copiesService.RemoveCopy(Id);

            if (deletedSuccessfully)
                return Ok(new { message = "Book copy deleted successfully.", copyId = Id });
            else
                return NotFound(new { message = "Book copy not found. Unable to delete." });
        }

        //_______________________________________________________________________________
        [HttpGet("GetBookCopy")]
        //[Authorize(Roles ="User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ReadBookCopyDto> GetBookCopy(int copyId)
        {
            if (copyId < 1)
                return BadRequest(new { message = "Invalid copy ID. ID must be greater than 0." });


            ReadBookCopyDto? copy = copiesService.GetCopy(copyId);

            if (copy is null)
                return NotFound(new { message = "Book copy not found.", copyId });

            return Ok(new { message = "Book copy retrieved successfully.", data = copy });
        }

        //_______________________________________________________________________________
        [HttpGet("GetCopyByBook")]
        //[Authorize(Roles ="User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetCopyIdByBookId(int bookId)
        {
            if (bookId < 1)
                return BadRequest(new { message = "Invalid book ID. ID must be greater than 0." });

            int? copyId = copiesService.GetCopyIdByBookId(bookId);

            if (copyId is null)
                return NotFound(new { message = "No available copies found for this book.", bookId });

            return Ok(new { message = "Book copy ID retrieved successfully.", copyId, bookId });
        }

        //_______________________________________________________________________________
        [HttpGet("GetAllBookCopies")]
        //[Authorize(Roles ="User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ReadBookCopyDto>> GetAllBookCopies()
        {
            IEnumerable<ReadBookCopyDto>? copies = copiesService.GetAllCopies();

            if (copies is null)
                return NotFound(new { message = "No book copies found in the database." });

            if (copies.Count() == 0)
                return NoContent();

            return Ok(new { message = "Book copies retrieved successfully.", data = copies, count = copies.Count() });
        }
    }
}
