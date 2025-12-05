using eCommerce.Application.DTOs.Product;
using eCommerce.Application.Services.Implementations;
using eCommerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService ) : ControllerBase
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetProducts()
        {
            var data = await productService.GetAllProductAsync();
            return data.Any() ? Ok(data) : NotFound(data);
        }
        [HttpGet("productById/{id}")]
        public async Task<IActionResult> GetProduct(int id )
        {
            var data = await productService.GetProductByIdAsync(id);
            return data == null ? Ok(data) : NotFound(data);
        }
        [HttpPost("addProduct")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct(CreateProduct Product)
        {
            var result = await productService.AddProductAsync(Product);
            return result.Success? Ok(result) : BadRequest(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("updateProduct")]
        public async Task<IActionResult> UpdateProduct(UpdateProduct Product)
        {
            var result = await productService.UpdateProductAsync(Product);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("deleteProduct/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await productService.DeleteProductAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpGet("products-by-category/{categoryId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            var products = await productService.GetProductsByCategoryIdAsync(categoryId);
            return products.Any() ? Ok(products) : NotFound();
        }


    }
}
