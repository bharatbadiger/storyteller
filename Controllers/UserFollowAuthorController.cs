using Microsoft.AspNetCore.Mvc;
using Storyteller.DTO;
using Storyteller.Models;
using Storyteller.Response;
using Storyteller.Services;
using System.Net;

namespace Storyteller.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserFollowAuthorController : ControllerBase
    {
        private readonly IUserFollowAuthorService _userFollowAuthorService;

        public UserFollowAuthorController(IUserFollowAuthorService userFollowAuthorService)
        {
            _userFollowAuthorService = userFollowAuthorService;
        }

        [HttpPost("follow")]
        public async Task<ActionResult<ApiResponse<UserFollowAuthor>>> Follow([FromBody] UserAuthorDTO userFollowAuthor)
        {
            if (userFollowAuthor?.UserId == null || userFollowAuthor?.AuthorId == null)
            {
                return BadRequest(new ApiResponse<UserFollowAuthor>
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Invalid input. User and Author must be provided.",
                    Data = null
                });
            }

            try
            {
                var result = await _userFollowAuthorService.FollowAsync(userFollowAuthor.UserId, userFollowAuthor.AuthorId);
                return Ok(new ApiResponse<UserFollowAuthor>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "User followed author successfully",
                    Data = result
                });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<UserFollowAuthor>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = ex.Message,
                    Data = null
                });
            }
            catch (ConflictException ex)
            {
                return Conflict(new ApiResponse<UserFollowAuthor>
                {
                    StatusCode = (int)HttpStatusCode.Conflict,
                    Message = ex.Message,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse<UserFollowAuthor>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = $"An error occurred while processing the request: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpDelete("unfollow")]
        public async Task<ActionResult<ApiResponse<object>>> Unfollow([FromBody] UserAuthorDTO userFollowAuthor)
        {
            if (userFollowAuthor?.UserId == null || userFollowAuthor?.AuthorId == null)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Invalid input. User and Author must be provided.",
                    Data = null
                });
            }

            try
            {
                await _userFollowAuthorService.UnfollowAsync(userFollowAuthor.UserId, userFollowAuthor.AuthorId);
                return Ok(new ApiResponse<object>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "User unfollowed author successfully",
                    Data = null
                });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<UserFollowAuthor>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = ex.Message,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse<object>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = $"An error occurred while processing the request: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Author>>>> GetAuthorsByUserId(long id)
        {
            try
            {
                var authors = await _userFollowAuthorService.GetAuthorsByUserIdAsync(id);
                if (authors == null || !authors.Any())
                {
                    return NotFound(new ApiResponse<IEnumerable<Author>>
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Message = "No authors found for the given user ID",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<IEnumerable<Author>>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Authors retrieved successfully",
                    Data = authors
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse<IEnumerable<Author>>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = $"An error occurred while processing the request: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpGet("author/{id}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<User>>>> GetUsersByAuthorId(long id)
        {
            try
            {
                var users = await _userFollowAuthorService.GetUsersByAuthorIdAsync(id);
                if (users == null || !users.Any())
                {
                    return NotFound(new ApiResponse<IEnumerable<User>>
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Message = "No users found for the given author ID",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<IEnumerable<User>>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Users retrieved successfully",
                    Data = users
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse<IEnumerable<User>>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = $"An error occurred while processing the request: {ex.Message}",
                    Data = null
                });
            }
        }
    }

}
