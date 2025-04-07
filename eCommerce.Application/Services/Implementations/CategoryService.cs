using AutoMapper;
using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Category;
using eCommerce.Application.DTOs.Product;
using eCommerce.Application.Services.Interfaces;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces;
using eCommerce.Domain.Interfaces.CategorySpecific;

namespace eCommerce.Application.Services.Implementations
{
    public class CategoryService(IGenericRepository<Category> genericRepository
        , IMapper mapper , ICategory categorySpecific ) : ICategoryService
    {
        public async Task<ServiceResponse> AddAsync(CreateCategory category)
        {
            var MapingCategory = mapper.Map<Category>(category);
            var result = await genericRepository.AddAsync(MapingCategory);
            if (result > 0)
                return new ServiceResponse(true, "Category is Added");

            return new ServiceResponse(true, "Fail to Added Category!");
        }

        public async Task<ServiceResponse> DeleteAsync(Guid Id)
        {
            var result = await genericRepository.DeleteAsync(Id);
            if (result > 0)
            {
                return new ServiceResponse(true, "Category deleted");
            }
            return new ServiceResponse(false, "Faild to delete Category");
        }

        public async Task<IEnumerable<GetCategory>> GetAllAsync()
        {
            var category = await genericRepository.GetAllAsync();
            if (!category.Any())
            {
                return [];
            }
            var result = mapper.Map<IEnumerable<GetCategory>>(category);
            return result;

        }

        public async Task<GetCategory> GetByIdAsync(Guid id)
        {
            var category = await genericRepository.GetByIdAsync(id);
            if (category == null)
            {
                return new GetCategory();
            }
            var result = mapper.Map<GetCategory>(category);
            return result;

        }

        public async Task<IEnumerable<GetProduct>> GetProductsByCategory(Guid categoryId)
        {
            var products = await categorySpecific.GetProductsByCategory(categoryId);
            if(!products.Any())
            {
                return [];
            }
            return mapper.Map<IEnumerable<GetProduct>>(products);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateCategory category)
        {
            var MapingCategory = mapper.Map<Category>(category);
            var result = await genericRepository.UpdateAsync(MapingCategory);
            if (result > 0)
                return new ServiceResponse(true, "Updated Category!");

            return new ServiceResponse(true, "Fail to Update Category");
        }
    }
}
