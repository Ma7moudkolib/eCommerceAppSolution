using AutoMapper;
using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Category;
using eCommerce.Application.DTOs.Product;
using eCommerce.Application.Services.Interfaces;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces;
using eCommerce.Domain.Interfaces.CategorySpecific;
using eCommerce.Domain.Interfaces.UnitOfWork;

namespace eCommerce.Application.Services.Implementations
{
    public class CategoryService( IMapper mapper , IUnitOfWork unitOfWork ) : ICategoryService
    {
        public async Task<ServiceResponse> AddAsync(CreateCategory category)
        {
            var MapingCategory = mapper.Map<Category>(category);
           // var result = await genericRepository.AddAsync(MapingCategory);
             await unitOfWork.Categories.AddAsync(MapingCategory);
            var result = await unitOfWork.CompleteAsync();
            if (result > 0)
            {
                return new ServiceResponse(true, "Category is Added");
            }
            unitOfWork.Dispose();
            return new ServiceResponse(true, "Fail to Added Category!");
        }

        public async Task<ServiceResponse> DeleteAsync(Guid Id)
        {
           // var result = await genericRepository.DeleteAsync(Id);
            await unitOfWork.Categories.DeleteAsync(Id);
            var result = await unitOfWork.CompleteAsync();
            if (result > 0)
            {
                return new ServiceResponse(true, "Category deleted");
            }
            unitOfWork.Dispose();
            return new ServiceResponse(false, "Faild to delete Category");
        }

        public async Task<IEnumerable<GetCategory>> GetAllAsync()
        {
         // var category = await genericRepository.GetAllAsync();
         var category = await unitOfWork.Categories.GetAllAsync();

            if (!category.Any())
            {
                return [];
            }
            var result = mapper.Map<IEnumerable<GetCategory>>(category);
            return result;

        }

        public async Task<GetCategory> GetByIdAsync(Guid id)
        {
            //var category = await genericRepository.GetByIdAsync(id);
            var category = await unitOfWork.Categories.GetByIdAsync(id);
            if (category == null)
            {
                return new GetCategory();
            }
            var result = mapper.Map<GetCategory>(category);
            return result;

        }

        public async Task<IEnumerable<GetProduct>> GetProductsByCategory(Guid categoryId)
        {
            //var products = await categorySpecific.GetProductsByCategory(categoryId);
            var products = await unitOfWork.Categories.GetProductsByCategory(categoryId);
            if(!products.Any())
            {
                return [];
            }
            return mapper.Map<IEnumerable<GetProduct>>(products);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateCategory category)
        {
            var MapingCategory = mapper.Map<Category>(category);
            //var result = await genericRepository.UpdateAsync(MapingCategory);
            unitOfWork.Categories.UpdateAsync(MapingCategory);
            var result = await unitOfWork.CompleteAsync();
            if (result > 0)
                return new ServiceResponse(true, "Updated Category!");
            unitOfWork.Dispose();
            return new ServiceResponse(true, "Fail to Update Category");
        }
    }
}
