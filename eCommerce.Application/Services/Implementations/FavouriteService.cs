using AutoMapper;
using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Favourite;
using eCommerce.Application.Services.Interfaces;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces;
using eCommerce.Domain.Interfaces.UnitOfWork;
namespace eCommerce.Application.Services.Implementations
{
    public class FavouriteService(IUnitOfWork unitOfWork, IMapper mapper ) : IFavouriteService
    {
        public async Task<ServiceResponse> AddAsync(CreateFavourite favourites)
        {
            var favouriteModel = mapper.Map<Favourite>(favourites);
           // var result = await favouriteRepository.AddAsync(favouriteModel);
           await unitOfWork.Favorites.AddAsync(favouriteModel);
            var result = await unitOfWork.CompleteAsync();
            if (result > 0)
            {
                new ServiceResponse(true, "Product Added!");
            }
            unitOfWork.Dispose();
            return new ServiceResponse(false, "Error occuered while adding product to Favourite");
        }

        public async Task<ServiceResponse> DeleteAsync(Guid Id)
        {
           //var result = await favouriteRepository.DeleteAsync(Id);
            await unitOfWork.Favorites.DeleteAsync(Id);
            var result = await unitOfWork.CompleteAsync();
            if (result > 0)
            {
                return new ServiceResponse(true, "Product Favorite deleted");
            }
            unitOfWork.Dispose();
            return new ServiceResponse(false, "Error to delete favorite product");
        }

        public async Task<IEnumerable<GetFavorite>> GetAllAsync()
        {
           // var favourites = await favouriteRepository.GetAllAsync();
           var favourites = await unitOfWork.Favorites.GetAllAsync();
            if (!favourites.Any())
            {
                return [];
            }
            var result = mapper.Map<IEnumerable<GetFavorite>>(favourites);
            return result;
        }

        public async Task<GetFavorite> GetByIdAsync(Guid id)
        {
           // var favorite = await favouriteRepository.GetByIdAsync(id);
           var favorite = await unitOfWork.Favorites.GetByIdAsync(id);
            if (favorite == null)
            {
                return new GetFavorite();
            }
            var result = mapper.Map<GetFavorite>(favorite);
            return result;
        }
    }
}
