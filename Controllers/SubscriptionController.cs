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
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<Subscription>>>> GetAll()
        {
            var subscriptions = await _subscriptionService.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<Subscription>>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Subscriptions retrieved successfully",
                Data = subscriptions
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Subscription>>> GetById(long id)
        {
            var subscription = await _subscriptionService.GetByIdAsync(id);
            if (subscription == null)
                return NotFound(new ApiResponse<Subscription>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Subscription not found",
                    Data = null
                });

            return Ok(new ApiResponse<Subscription>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Subscription retrieved successfully",
                Data = subscription
            });
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<ApiResponse<Subscription>>> GetByName(string name)
        {
            var subscriptions = await _subscriptionService.GetByNameAsync(name);
            if (subscriptions == null)
                return NotFound(new ApiResponse<Subscription>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Subscription not found",
                    Data = null
                });

            return Ok(new ApiResponse<IEnumerable<Subscription>>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Subscriptions retrieved successfully",
                Data = subscriptions
            });
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<Subscription>>> Add(Subscription subscription)
        {
            try
            {
                var createdSubscription = await _subscriptionService.AddAsync(subscription);
                return CreatedAtAction(nameof(GetById), new { id = createdSubscription.Id }, new ApiResponse<Subscription>
                {
                    StatusCode = (int)HttpStatusCode.Created,
                    Message = "Subscription created successfully",
                    Data = createdSubscription
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<Subscription>
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Subscription>>> Update(long id, Subscription subscription)
        {
            var existingSubscription = await _subscriptionService.GetByIdAsync(id);
            if (existingSubscription == null)
            {
                return NotFound(new ApiResponse<Subscription>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Subscription not found",
                    Data = null
                });
            }

            PartialUpdateHelper.ApplyPatch(existingSubscription, subscription);
            var updatedSubscription = await _subscriptionService.UpdateAsync(existingSubscription);
            return Ok(new ApiResponse<Subscription>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Subscription updated successfully",
                Data = updatedSubscription
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(long id)
        {
            var result = await _subscriptionService.DeleteAsync(id);
            if (!result)
                return NotFound(new ApiResponse<bool>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Subscription not found",
                    Data = false
                });

            return Ok(new ApiResponse<bool>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Subscription deleted successfully",
                Data = true
            });
        }
    }
}
