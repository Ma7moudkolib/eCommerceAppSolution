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
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public CategoryService(IRepositoryManager repositoryManager , IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }
        public async Task<ServiceResponse> AddCategoryAsync(CreateCategory category , CancellationToken cancellationToken=default)
        {
            var categoryEntity = _mapper.Map<Category>(category);
            
            _repositoryManager.Categorie.CreateCategory(categoryEntity);
            var result = await _repositoryManager.CompleteAsync(cancellationToken);
           
            return result > 0 ? new ServiceResponse(true, "Category is Added"):
                new ServiceResponse(true, "Fail to Added Category!");
        }

        public async Task<ServiceResponse> DeleteCategoryAsync(int Id , CancellationToken cancellationToken=default)
        {
            var category = await _repositoryManager.Categorie.GetCategoryById(Id,false);
            if (category is null)
                return new ServiceResponse(false, "this category not found");

            _repositoryManager.Categorie.DeleteCategory(category);
            var result = await _repositoryManager.CompleteAsync(cancellationToken);
           
                return result > 0 ?  new ServiceResponse(true, "Category deleted"):
                 new ServiceResponse(false, "Faild to delete Category");
        }

        public async Task<IEnumerable<GetCategory>> GetAllCategoryAsync()
        {
            var categories = await _repositoryManager.Categorie.GetAllCategories(false);

            if (!categories.Any())
                return [];
            
            var result = _mapper.Map<IEnumerable<GetCategory>>(categories);
            return result;
        }

        public async Task<GetCategory> GetCategoryByIdAsync(int id)
        {
           
            var category = await _repositoryManager.Categorie.GetCategoryById(id, false);
            if (category == null)
                return new GetCategory();
            
            var result = _mapper.Map<GetCategory>(category);
            return result;

        }

        public async Task<ServiceResponse> UpdateAsync(UpdateCategory category , CancellationToken cancellationToken=default)
        {
          var categoryEntity = await _repositoryManager.Categorie.GetCategoryById(category.Id, true);
            if (categoryEntity is null)
                return new ServiceResponse(false, "this category not found");
            categoryEntity.Name = category.Name;
            _repositoryManager.Categorie.UpdateCategory(categoryEntity);
            var result = await _repositoryManager.CompleteAsync(cancellationToken);

            return result > 0 ? new ServiceResponse(true, "Category is Updated") :
             new ServiceResponse(false, "Faild to update Category");
        }
    }
}
