using Library.BLL.Interfaces;
using Library.Core.Dtos.BookDtos;
using Library.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/Books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IBooksService booksService;

        public BooksController(IBooksService booksService)
        {
            this.booksService = booksService;
        }

        //_______________________________________________________________________________
        [HttpPost("AddBook")]
        //[Authorize(Roles ="Admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<ReadBookDto> AddBook(AddBookDTO book)
        {
            if (book is null)
                return BadRequest(new { message = "Book cannot be null." });

            if (string.IsNullOrWhiteSpace(book.Title))
                return BadRequest(new { message = "Book title is required." });

            if (book.AuthorId < 1)
                return BadRequest(new { message = "Valid author ID is required." });

            if (book.CategoryId < 1)
                return BadRequest(new { message = "Valid category ID is required." });

            int newBookId = booksService.AddBook(book);

            if (newBookId == -1)
                return Conflict(new { message = "Failed to create book. Please try again." });

            book.BookId = newBookId;
            return Created(nameof(GetBook), new { message = "Book created successfully.", data = book, bookId = newBookId });
        }

        //_______________________________________________________________________________
        [HttpPut("UpdateBook")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<ReadBookDto> UpdateBook(int bookId, UpdateBookDto updatedBook)
        {
            if (bookId < 1)
                return BadRequest(new { message = "Invalid book ID. ID must be greater than 0." });

            if (updatedBook is null)
                return BadRequest(new { message = "Updated book data cannot be null." });

            if (string.IsNullOrWhiteSpace(updatedBook.Title))
                return BadRequest(new { message = "Book title is required." });

            ReadBookDto? book = booksService.GetBook(bookId);

            if (book is null)
                return NotFound(new { message = "Book not found.", bookId });

            bool updatedSuccessfully = booksService.UpdateBook(bookId, updatedBook);

            if (updatedSuccessfully)
                return Ok(new { message = "Book updated successfully.", data = book, bookId });
            else
                return Conflict(new { message = "Failed to update book. Please try again." });
        }

        //_______________________________________________________________________________
        [HttpDelete("{Id}", Name = "DeleteBook")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteBook(int Id)
        {
            if (Id < 1)
                return BadRequest(new { message = "Invalid book ID. ID must be greater than 0." });

            bool deletedSuccessfully = booksService.RemoveBook(Id);

            if (deletedSuccessfully)
                return Ok(new { message = "Book deleted successfully.", bookId = Id });
            else
                return NotFound(new { message = "Book not found. Unable to delete." });
        }

        //_______________________________________________________________________________
        [HttpGet("GetBook")]
        //[Authorize(Roles ="Staff")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ReadBookDto> GetBook(int bookId)
        {
            if (bookId < 1)
                return BadRequest(new { message = "Invalid book ID. ID must be greater than 0." });

            ReadBookDto? book = booksService.GetBook(bookId);

            if (book is null)
                return NotFound(new { message = "Book not found.", bookId });

            return Ok(new { message = "Book retrieved successfully.", data = book });
        }

        //_______________________________________________________________________________
        [HttpGet("GetAllBooks")]
        //[Authorize(Roles ="Staff")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ReadBookDto>> GetAllBooks()
        {
            IEnumerable<ReadBookDto>? books = booksService.GetAllBooks();

            if (books is null)
                return NotFound(new { message = "No books found in the database." });

            if (books.Count() == 0)
                return NoContent();

            return Ok(new { message = "Books retrieved successfully.",
                data = books, count = books.Count() });
        }
    }
}