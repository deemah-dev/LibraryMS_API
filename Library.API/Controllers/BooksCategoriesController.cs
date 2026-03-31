using Library.BLL.Interfaces;
using Library.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/BooksCategories")]
    [ApiController]
    public class BooksCategoriesController : ControllerBase
    {
        private IBooksCategoriesService categoriesService;

        public BooksCategoriesController(IBooksCategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        //_______________________________________________________________________________
        [HttpDelete("{Id}", Name = "DeleteBookCategory")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteBookCategory(int Id)
        {
            if (Id < 1)
                return BadRequest(new { message = "Invalid category ID. ID must be greater than 0." });
            bool deletedSuccessfully = categoriesService.RemoveCategory(Id);
            if (deletedSuccessfully)
                return Ok(new { message = "Category deleted successfully.", categoryId = Id });
            else
                return NotFound(new { message = "Category not found. Unable to delete." });
        }

        //_______________________________________________________________________________

        [HttpGet("GetAllCategories")]
        [Authorize(Roles = "Staff,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<BookCategory>> GetAllCategories()
        {
            IEnumerable<BookCategory>? bookCategories = categoriesService.GetAllCategories();
            if (bookCategories is null)
                return NotFound(new { message = "No categories found in the database." });
            if (bookCategories.Count() == 0)
                return NoContent();
            else
                return Ok(new { message = "Categories retrieved successfully.", data = bookCategories, count = bookCategories.Count() });
        }

        //__________________________________________________________________________________

        [HttpGet("GetBookCategoryById")]
        [Authorize(Roles = "Staff,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<BookCategory> GetBookCategoryById(int categoryId)
        {
            if (categoryId < 1)
                return BadRequest(new { message = "Invalid category ID. ID must be greater than 0." });

            BookCategory? bookCategory = categoriesService.GetCategory(categoryId);
            if (bookCategory is null)
                return NotFound(new { message = "Category not found.", categoryId });

            else
                return Ok(new { message = "Category retrieved successfully.", data = bookCategory });
        }

        //__________________________________________________________________________________
        [HttpPost("AddBookCategory")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public ActionResult<BookCategory> AddBookCategory(BookCategory bookCategory)
        {
            if (bookCategory is null)
                return BadRequest(new { message = "Category cannot be null." });

            if (string.IsNullOrWhiteSpace(bookCategory.Name))
                return BadRequest(new { message = "Category name is required." });

            int newCategoryId = categoriesService.AddCategory(bookCategory);

            if (newCategoryId == -1)
                return Conflict(new { message = "Failed to create category. Please try again." });

            else
            {
                bookCategory.CategoryId = newCategoryId;
                return Created(nameof(GetBookCategoryById), new { message = "Category created successfully.", data = bookCategory, categoryId = newCategoryId });
            }

        }
        //____________________________________________________________________________________

        [HttpPut("UpdateBookCategory")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<BookCategory> UpdateBookCategory(int categoryId, BookCategory updatedBookCategory)
        {
            if (categoryId < 1)
                return BadRequest(new { message = "Invalid category ID. ID must be greater than 0." });

            if (updatedBookCategory is null)
                return BadRequest(new { message = "Updated category data cannot be null." });

            if (string.IsNullOrWhiteSpace(updatedBookCategory.Name))
                return BadRequest(new { message = "Category name is required." });

            BookCategory? bookCategory = categoriesService.GetCategory(categoryId);

            if (bookCategory is null)
                return NotFound(new { message = "Category not found.", categoryId });

            bookCategory.Name = updatedBookCategory.Name;

            bool updatedSuccessfully = categoriesService.UpdateCategory(bookCategory);

            if (updatedSuccessfully)
                return Ok(new { message = "Category updated successfully.", data = bookCategory, categoryId });
            else
                return Conflict(new { message = "Failed to update category. Please try again." });
        }

    }
}