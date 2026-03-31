using Library.BLL.Interfaces;
using Library.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.API.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        //_______________________________________________________________________________
        [HttpPost("AddUser")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<User> AddUser(User user)
        {
            if (user is null)
                return BadRequest(new { message = "User cannot be null." });

            if (string.IsNullOrWhiteSpace(user.Username))
                return BadRequest(new { message = "Username is required." });

            if (string.IsNullOrWhiteSpace(user.PasswordHash))
                return BadRequest(new { message = "Password is required." });

            if (user.RoleId < 1)
                return BadRequest(new { message = "Valid role ID is required." });

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            int newUserId = usersService.AddUser(user);

            if (newUserId == -1)
                return Conflict(new { message = "Failed to create user. Username may already exist." });

            user.UserId = newUserId;
            return Created(nameof(GetUser), new { message = "User created successfully.", data = user, userId = newUserId });
        }

        //_______________________________________________________________________________
        [HttpPut("UpdateUser")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<User> UpdateUser(int userId, User updatedUser)
        {
            if (userId < 1)
                return BadRequest(new { message = "Invalid user ID. ID must be greater than 0." });

            if (updatedUser is null)
                return BadRequest(new { message = "Updated user data cannot be null." });

            if (string.IsNullOrWhiteSpace(updatedUser.PasswordHash))
                return BadRequest(new { message = "Password is required." });

            if (updatedUser.RoleId < 1)
                return BadRequest(new { message = "Valid role ID is required." });

            User? user = usersService.GetUser(userId);

            if (user is null)
                return NotFound(new { message = "User not found.", userId });

            user.PasswordHash = updatedUser.PasswordHash;
            user.RoleId = updatedUser.RoleId;

            bool updatedSuccessfully = usersService.UpdateUser(user);

            if (updatedSuccessfully)
                return Ok(new { message = "User updated successfully.", data = user, userId });
            else
                return Conflict(new { message = "Failed to update user. Please try again." });
        }

        //_______________________________________________________________________________
        [HttpDelete("{Id}", Name = "DeleteUser")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteUser(int Id)
        {
            if (Id < 1)
                return BadRequest(new { message = "Invalid user ID. ID must be greater than 0." });

            bool deletedSuccessfully = usersService.RemoveUser(Id);

            if (deletedSuccessfully)
                return Ok(new { message = "User deleted successfully.", userId = Id });
            else
                return NotFound(new { message = "User not found. Unable to delete." });
        }

        //_______________________________________________________________________________
        [HttpGet("GetUser")]
        [Authorize(Roles = "Staff,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<User> GetUser(int userId)
        {
            if (userId < 1)
                return BadRequest(new { message = "Invalid user ID. ID must be greater than 0." });

            User? user = usersService.GetUser(userId);

            if (user is null)
                return NotFound(new { message = "User not found.", userId });

            return Ok(new { message = "User retrieved successfully.", data = user });
        }

        //_______________________________________________________________________________
        [HttpGet("GetUserByUsername")]
        [Authorize(Roles = "Staff,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<User> GetUserByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return BadRequest(new { message = "Username is required." });

            User? user = usersService.GetUserByUsername(username);

            if (user is null)
                return NotFound(new { message = "User not found.", username });

            return Ok(new { message = "User retrieved successfully.", data = user });
        }

        //_______________________________________________________________________________
        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "Staff,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            IEnumerable<User>? users = usersService.GetAllUsers();

            if (users is null)
                return NotFound(new { message = "No users found in the database." });

            if (users.Count() == 0)
                return NoContent();

            return Ok(new { message = "Users retrieved successfully.", data = users, count = users.Count() });
        }
    }
}
