using AutoMapper;
using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Favourite;
using eCommerce.Application.Services.Interfaces;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces;
namespace eCommerce.Application.Services.Implementations
{
    public class FavouriteService(IGenericRepository<Favourite> favouriteRepository, IMapper mapper ) : IFavouriteService
    {
        public async Task<ServiceResponse> AddAsync(CreateFavourite favourites)
        {
            var favouriteModel = mapper.Map<Favourite>(favourites);
            var result = await favouriteRepository.AddAsync(favouriteModel);
            return result > 0 ? new ServiceResponse(true, "Product Added!") :
                new ServiceResponse(false, "Error occuered while adding product to Favourite");
        }

        public async Task<ServiceResponse> DeleteAsync(Guid Id)
        {
           var result = await favouriteRepository.DeleteAsync(Id);
            if(result > 0)
            {
                return new ServiceResponse(true, "Product Favorite deleted");
            }
            return new ServiceResponse(false, "Error to delete favorite product");
        }

        public async Task<IEnumerable<GetFavorite>> GetAllAsync()
        {
            var favourites = await favouriteRepository.GetAllAsync();
            if(!favourites.Any())
            {
                return [];
            }
            var result = mapper.Map<IEnumerable<GetFavorite>>(favourites);
            return result;
        }

        public async Task<GetFavorite> GetByIdAsync(Guid id)
        {
            var favorite = await favouriteRepository.GetByIdAsync(id);
            if(favorite == null)
            {
                return new GetFavorite();
            }
            var result = mapper.Map<GetFavorite>(favorite);
            return result;
        }
    }
}
