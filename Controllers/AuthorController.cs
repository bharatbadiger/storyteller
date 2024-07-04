using System.Net;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Storyteller.Helpers;
using Storyteller.Models;
using Storyteller.Response;
using Storyteller.Services;

namespace Storyteller.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<Author>>>> GetAll()
        {
            var authors = await _authorService.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<Author>>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Authors retrieved successfully",
                Data = authors
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Author>>> GetById(long id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author == null)
                return NotFound(new ApiResponse<Author>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Author not found",
                    Data = null
                });

            return Ok(new ApiResponse<Author>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Author retrieved successfully",
                Data = author
            });
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Author>>>> GetByName(string name)
        {
            var authors = await _authorService.GetByNameAsync(name);
            return Ok(new ApiResponse<IEnumerable<Author>>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Authors found",
                Data = authors
            });
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<Author>>> Add(Author author)
        {
            try
            {
                var createdAuthor = await _authorService.AddAsync(author);
                return CreatedAtAction(nameof(GetById), new { id = createdAuthor.Id }, new ApiResponse<Author>
                {
                    StatusCode = (int)HttpStatusCode.Created,
                    Message = "Author created successfully",
                    Data = createdAuthor
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<Author>
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Failed to create author: " + ex.Message,
                    Data = null
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Author>>> Update(long id, Author author)
        {
            var existingAuthor = await _authorService.GetByIdAsync(id);
            if (existingAuthor == null)
            {
                return NotFound(new ApiResponse<Author>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Author not found",
                    Data = null
                });
            }

            PartialUpdateHelper.ApplyPatch(existingAuthor, author);
            var updatedAuthor = await _authorService.UpdateAsync(existingAuthor);
            return Ok(new ApiResponse<Author>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Author updated successfully",
                Data = updatedAuthor
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(long id)
        {
            var result = await _authorService.DeleteAsync(id);
            if (!result)
                return NotFound(new ApiResponse<bool>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Author not found",
                    Data = false
                });

            return Ok(new ApiResponse<bool>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Author deleted successfully",
                Data = true
            });
        }
    }
}
