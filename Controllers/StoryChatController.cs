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
    public class StoryChatController : ControllerBase
    {
        private readonly IStoryService _storyService;
        private readonly IStoryChatService _storyChatService;

        public StoryChatController(IStoryChatService storyChatService,IStoryService storyService)
        {
            _storyService = storyService;
            _storyChatService = storyChatService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<StoryChat>>>> GetAll()
        {
            var storyChats = await _storyChatService.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<StoryChat>>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Story chats retrieved successfully",
                Data = storyChats
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<StoryChat>>> GetById(long id)
        {
            var storyChat = await _storyChatService.GetByIdAsync(id);
            if (storyChat == null)
                return NotFound(new ApiResponse<StoryChat>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Story chat not found",
                    Data = null
                });

            return Ok(new ApiResponse<StoryChat>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Story chat retrieved successfully",
                Data = storyChat
            });
        }

[HttpPost]
    public async Task<ActionResult<ApiResponse<StoryChat>>> Add(StoryChatRequestDTO request)
    {
        try
        {
            var story = await _storyService.GetByIdAsync(request.Story.Id);
            if (story == null)
            {
                return BadRequest(new ApiResponse<StoryChat>
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Story not found",
                    Data = null
                });
            }

            var storyChat = new StoryChat
            {
                Story = story,
                Text = request.Text,
                MediaUrl = request.MediaUrl,
                MessageType = request.MessageType,
                ReactionType = request.ReactionType,
                ReactionEnabled = request.ReactionEnabled,
                Sender = request.Sender,
                ChatTimestamp = request.ChatTimestamp
            };

            var createdStoryChat = await _storyChatService.AddAsync(storyChat);
            return CreatedAtAction(nameof(GetById), new { id = createdStoryChat.Id }, new ApiResponse<StoryChat>
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "Story chat created successfully",
                Data = createdStoryChat
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<StoryChat>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = ex.Message,
                Data = null
            });
        }
    }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<StoryChat>>> Update(long id, StoryChat storyChat)
        {
            var existingStoryChat = await _storyChatService.GetByIdAsync(id);
            if (existingStoryChat == null)
            {
                return NotFound(new ApiResponse<StoryChat>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "StoryChat not found",
                    Data = null
                });
            }

            PartialUpdateHelper.ApplyPatch(existingStoryChat, storyChat);
            var updatedStoryChat = await _storyChatService.UpdateAsync(existingStoryChat);
            return Ok(new ApiResponse<StoryChat>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "StoryChat updated successfully",
                Data = updatedStoryChat
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(long id)
        {
            var result = await _storyChatService.DeleteAsync(id);
            if (!result)
                return NotFound(new ApiResponse<bool>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Story chat not found",
                    Data = false
                });

            return Ok(new ApiResponse<bool>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Story chat deleted successfully",
                Data = true
            });
        }

        [HttpGet("by-story/{storyId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<StoryChat>>>> GetByStoryId(long storyId)
        {
            var storyChats = await _storyChatService.GetByStoryIdAsync(storyId);
            return Ok(new ApiResponse<IEnumerable<StoryChat>>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Story chats retrieved successfully",
                Data = storyChats
            });
        }

        [HttpGet("story/{storyId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<StoryChat>>>> GetByStoryIdOrderBySerialNumber(long storyId)
        {
            var storyChats = await _storyChatService.GetByStoryIdOrderBySerialNumberAsync(storyId);
            return Ok(new ApiResponse<IEnumerable<StoryChat>>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Story chats retrieved successfully",
                Data = storyChats
            });
        }

        [HttpGet("max-serial-number/{storyId}")]
        public async Task<ActionResult<ApiResponse<long?>>> GetMaxSerialNumberByStoryId(long storyId)
        {
            var maxSerialNumber = await _storyChatService.GetMaxSerialNumberByStoryIdAsync(storyId);
            return Ok(new ApiResponse<long?>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Max serial number retrieved successfully",
                Data = maxSerialNumber
            });
        }
    }
}
