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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<Category>>>> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<Category>>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Categories retrieved successfully",
                Data = categories
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Category>>> GetById(long id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound(new ApiResponse<Category>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Category not found",
                    Data = null
                });

            return Ok(new ApiResponse<Category>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Category retrieved successfully",
                Data = category
            });
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Category>>>> GetByName(string name)
        {
            var categories = await _categoryService.GetByNameAsync(name);
            return Ok(new ApiResponse<IEnumerable<Category>>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Categories found",
                Data = categories
            });
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<Category>>> Add(Category category)
        {
            try
            {
                var createdCategory = await _categoryService.AddAsync(category);
                return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id }, new ApiResponse<Category>
                {
                    StatusCode = (int)HttpStatusCode.Created,
                    Message = "Category created successfully",
                    Data = createdCategory
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<Category>
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Category>>> Update(long id, Category category)
        {
            var existingCategory = await _categoryService.GetByIdAsync(id);
            if (existingCategory == null)
            {
                return NotFound(new ApiResponse<Category>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Category not found",
                    Data = null
                });
            }

            PartialUpdateHelper.ApplyPatch(existingCategory, category);
            var updatedCategory = await _categoryService.UpdateAsync(existingCategory);
            return Ok(new ApiResponse<Category>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Category updated successfully",
                Data = updatedCategory
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(long id)
        {
            var result = await _categoryService.DeleteAsync(id);
            if (!result)
                return NotFound(new ApiResponse<bool>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Category not found",
                    Data = false
                });

            return Ok(new ApiResponse<bool>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Category deleted successfully",
                Data = true
            });
        }
    }
}
