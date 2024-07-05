using Microsoft.AspNetCore.Mvc;
using Storyteller.DTO;
using Storyteller.Helpers;
using Storyteller.Models;
using Storyteller.Response;
using Storyteller.Services;
using System.Net;

namespace Storyteller.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoryController : ControllerBase
    {
        private readonly IStoryService _storyService;
        private readonly IAuthorService _authorService;
        private readonly ICategoryService _categoryService;

        public StoryController(IStoryService storyService, IAuthorService authorService, ICategoryService categoryService)
        {
            _storyService = storyService;
            _authorService = authorService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<Story>>>> GetAll()
        {
            var stories = await _storyService.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<Story>>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Stories retrieved successfully",
                Data = stories
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Story>>> GetById(long id)
        {
            var story = await _storyService.GetByIdAsync(id);
            if (story == null)
                return NotFound(new ApiResponse<Story>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Story not found",
                    Data = null
                });

            return Ok(new ApiResponse<Story>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Story retrieved successfully",
                Data = story
            });
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Story>>>> GetByName(string name)
        {
            var story = await _storyService.GetByNameContainingAsync(name);
            if (story == null)
                return NotFound(new ApiResponse<IEnumerable<Story>>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Story not found",
                    Data = null
                });

            return Ok(new ApiResponse<IEnumerable<Story>>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Story retrieved successfully",
                Data = story
            });
        }

        [HttpGet("author/{author}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Story>>>> GetByAuthorName(string author)
        {
            var story = await _storyService.GetByAuthorContainingAsync(author);
            if (story == null)
                return NotFound(new ApiResponse<IEnumerable<Story>>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Story not found",
                    Data = null
                });

            return Ok(new ApiResponse<IEnumerable<Story>>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Story retrieved successfully",
                Data = story
            });
        }

        [HttpGet("tag/{name}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Story>>>> GetByTagName(string name)
        {
            var story = await _storyService.GetByTagsContainingAsync(name);
            if (story == null)
                return NotFound(new ApiResponse<IEnumerable<Story>>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Story not found",
                    Data = null
                });

            return Ok(new ApiResponse<IEnumerable<Story>>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Story retrieved successfully",
                Data = story
            });
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<Story>>> Add([FromBody] StoryRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest(new ApiResponse<Story>
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Invalid request body",
                    Data = null
                });
            }

            try
            {
                var category = await _categoryService.GetByIdAsync(request.Category.Id);
                if (category == null)
                {
                    return NotFound(new ApiResponse<Story>
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Message = "Category not found",
                        Data = null
                    });
                }

                var author = await _authorService.GetByIdAsync(request.Author.Id);
                if (author == null)
                {
                    return NotFound(new ApiResponse<Story>
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Message = "Author not found",
                        Data = null
                    });
                }

                var story = new Story
                {
                    Category = category,
                    Author = author,
                    Name = request.Name,
                    Image = request.Image,
                    UserMe = request.UserMe,
                    UserOther = request.UserOther,
                    Tags = request.Tags
                };

                var createdStory = await _storyService.AddAsync(story);
                return CreatedAtAction(nameof(GetById), new { id = createdStory.Id }, new ApiResponse<Story>
                {
                    StatusCode = (int)HttpStatusCode.Created,
                    Message = "Story created successfully",
                    Data = createdStory
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<Story>
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Story>>> Update(long id, Story story)
        {
            var existingStory = await _storyService.GetByIdAsync(id);
            if (existingStory == null)
            {
                return NotFound(new ApiResponse<Story>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Story not found",
                    Data = null
                });
            }

            PartialUpdateHelper.ApplyPatch(existingStory, story);
            var updatedStory = await _storyService.UpdateAsync(existingStory);
            return Ok(new ApiResponse<Story>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Story updated successfully",
                Data = updatedStory
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(long id)
        {
            var result = await _storyService.DeleteAsync(id);
            if (!result)
                return NotFound(new ApiResponse<bool>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Story not found",
                    Data = false
                });

            return Ok(new ApiResponse<bool>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Story deleted successfully",
                Data = true
            });
        }
    }

}
