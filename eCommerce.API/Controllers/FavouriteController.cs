
using eCommerce.Application.DTOs.Favourite;
using eCommerce.Application.Services.Implementations;
using eCommerce.Application.Services.Implementations.Cart;
using eCommerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteController(IFavouriteService favouriteService ) : ControllerBase
    {
        [HttpPost("add-product-favourite")]
        public async Task<IActionResult> AddFavourite(CreateFavourite favourites)
        {
           
            var result = await favouriteService.AddAsync(favourites);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllFavorite()
        {
            var data = await favouriteService.GetAllAsync();
            return data.Any()? Ok(data) : BadRequest(data);
        }
        [HttpGet("getBy/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await favouriteService.GetByIdAsync(id);
            return data != null ? Ok(data) : NotFound(data);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await favouriteService.DeleteAsync(id);
            return result.Success? Ok(result) : BadRequest(result);
        }
    }
}
