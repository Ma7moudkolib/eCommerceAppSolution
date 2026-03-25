using eCommerce.Application.DTOs.Category;
using eCommerce.Application.DTOs.Product;
using eCommerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService ) : ControllerBase
    {
        [HttpGet("allCategory")]
        public async Task<IActionResult> GetCategories()
        {
            var data = await categoryService.GetAllCategoryAsync();
            return data.Any() ? Ok(data) : NotFound(data);
        }
        [HttpGet("getCategoryById/{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var data = await categoryService.GetCategoryByIdAsync(id);
            return data != null ? Ok(data) : NotFound(data);
        }
        [HttpPost("addCategory")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCategory(CreateCategory category)
        {
            var result = await categoryService.AddCategoryAsync(category);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPut("updateCategory")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateCategory(UpdateCategory category)
        {
            var result = await categoryService.UpdateCategoryAsync(category);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("deleteCategory/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await categoryService.DeleteCategoryAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }
       
    }
}
