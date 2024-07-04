using Microsoft.AspNetCore.Mvc;
using Storyteller.Helpers;
using Storyteller.Models;
using Storyteller.Response;
using Storyteller.Services;
using System.Net;

namespace Storyteller.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<User>>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<User>>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Users retrieved successfully",
                Data = users
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<User>>> GetById(long id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound(new ApiResponse<User>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "User not found",
                    Data = null
                });

            return Ok(new ApiResponse<User>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "User retrieved successfully",
                Data = user
            });
        }

        [HttpGet("firebaseAuthId/{id}")]
        public async Task<ActionResult<ApiResponse<User>>> GetByFirebaseAuthId(string id)
        {
            var user = await _userService.GetByFirebaseAuthIdAsync(id);
            if (user == null)
                return NotFound(new ApiResponse<User>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "User not found",
                    Data = null
                });

            return Ok(new ApiResponse<User>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "User retrieved successfully",
                Data = user
            });
        }

        [HttpGet("mobile/{mobile}")]
        public async Task<ActionResult<ApiResponse<User>>> GetByMobileNumber(string mobile)
        {
            var user = await _userService.GetByMobileNumberAsync(mobile);
            if (user == null)
                return NotFound(new ApiResponse<User>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "User not found",
                    Data = null
                });

            return Ok(new ApiResponse<User>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "User retrieved successfully",
                Data = user
            });
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<User>>>> GetByName(string name)
        {
            var user = await _userService.GetByNameAsync(name);
            if (user == null)
                return NotFound(new ApiResponse<User>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "User not found",
                    Data = null
                });

            return Ok(new ApiResponse<IEnumerable<User>>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "User retrieved successfully",
                Data = user
            });
        }        

        [HttpPost]
        public async Task<ActionResult<ApiResponse<User>>> Add(User user)
        {
            try
            {
                var createdUser = await _userService.AddAsync(user);
                return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, new ApiResponse<User>
                {
                    StatusCode = (int)HttpStatusCode.Created,
                    Message = "User created successfully",
                    Data = createdUser
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<User>
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<User>>> Update(long id, User user)
        {
            var existingUser = await _userService.GetByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound(new ApiResponse<User>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "User not found",
                    Data = null
                });
            }

            PartialUpdateHelper.ApplyPatch(existingUser, user);
            var updatedUser = await _userService.UpdateAsync(existingUser);
            return Ok(new ApiResponse<User>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "User updated successfully",
                Data = updatedUser
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(long id)
        {
            var result = await _userService.DeleteAsync(id);
            if (!result)
                return NotFound(new ApiResponse<bool>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "User not found",
                    Data = false
                });

            return Ok(new ApiResponse<bool>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "User deleted successfully",
                Data = true
            });
        }
    }
}
