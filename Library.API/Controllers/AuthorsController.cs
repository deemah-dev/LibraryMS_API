using Library.BLL.Interfaces;
using Library.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/Authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private IAuthorsService authorsService;

        public AuthorsController(IAuthorsService authorsService)
        {
            this.authorsService = authorsService;
        }

        //_______________________________________________________________________________
        [HttpPost("AddAuthor")]
        //[Authorize(Roles ="Admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Author> AddAuthor(Author author)
        {
            if (author is null)
                return BadRequest(new { message = "Author cannot be null." });

            if (string.IsNullOrWhiteSpace(author.Name))
                return BadRequest(new { message = "Author name is required." });

            int newAuthorId = authorsService.AddAuthor(author);

            if (newAuthorId == -1)
                return Conflict(new { message = "Failed to create author. Please try again." });

            author.AuthorId = newAuthorId;
            return Created(nameof(GetAuthor), new { message = "Author created successfully.", data = author, authorId = newAuthorId });
        }

        //_______________________________________________________________________________
        [HttpPut("UpdateAuthor")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<Author> UpdateAuthor(int authorId, Author updatedAuthor)
        {
            if (authorId < 1)
                return BadRequest(new { message = "Invalid author ID. ID must be greater than 0." });

            if (updatedAuthor is null)
                return BadRequest(new { message = "Updated author data cannot be null." });

            if (string.IsNullOrWhiteSpace(updatedAuthor.Name))
                return BadRequest(new { message = "Author name is required." });

            Author? author = authorsService.GetAuthorByName(updatedAuthor.Name);

            // If author doesn't exist by that name, we need to get the author by ID
            // Since we don't have a GetAuthorById in the service, we'll check if name exists
            if (author is null)
            {
                // Create a new author with the given ID and name
                author = new Author { AuthorId = authorId, Name = updatedAuthor.Name };
            }
            else if (author.AuthorId != authorId)
            {
                return NotFound(new { message = "Author not found.", authorId });
            }

            author.Name = updatedAuthor.Name;

            bool updatedSuccessfully = authorsService.UpdateAuthor(author);

            if (updatedSuccessfully)
                return Ok(new { message = "Author updated successfully.", data = author, authorId });
            else
                return Conflict(new { message = "Failed to update author. Please try again." });
        }

        //_______________________________________________________________________________
        [HttpGet("GetAuthorByName")]
        //[Authorize(Roles ="User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Author> GetAuthorByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest(new { message = "Author name is required." });

            Author? author = authorsService.GetAuthorByName(name);

            if (author is null)
                return NotFound(new { message = "Author not found.", name });

            return Ok(new { message = "Author retrieved successfully.", data = author });
        }

        //_______________________________________________________________________________
        [HttpGet("GetAuthor")]
        //[Authorize(Roles ="User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Author> GetAuthor(int authorId)
        {
            if (authorId < 1)
                return BadRequest(new { message = "Invalid author ID. ID must be greater than 0." });

            IEnumerable<Author>? allAuthors = authorsService.GetAllAuthors();

            if (allAuthors is null)
                return NotFound(new { message = "No authors found." });

            Author? author = allAuthors.FirstOrDefault(a => a.AuthorId == authorId);

            if (author is null)
                return NotFound(new { message = "Author not found.", authorId });

            return Ok(new { message = "Author retrieved successfully.", data = author });
        }

        //_______________________________________________________________________________
        [HttpGet("GetAllAuthors")]
        //[Authorize(Roles ="User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Author>> GetAllAuthors()
        {
            IEnumerable<Author>? authors = authorsService.GetAllAuthors();

            if (authors is null)
                return NotFound(new { message = "No authors found in the database." });

            if (authors.Count() == 0)
                return NoContent();

            return Ok(new { message = "Authors retrieved successfully.", data = authors, count = authors.Count() });
        }
    }
}
