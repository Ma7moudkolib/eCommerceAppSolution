using AutoMapper;
using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Product;
using eCommerce.Application.Services.Interfaces;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces;
namespace eCommerce.Application.Services.Implementations
{
    public class ProductService(IGenericRepository<Product> genericRepository , IMapper mapper ) : IProductService
    {
        
        public async Task<ServiceResponse> AddAsync(CreateProduct product)
        {
            var MapingProduct = mapper.Map<Product>( product );
            var result = await genericRepository.AddAsync( MapingProduct );
            if(result >  0)
                return new ServiceResponse(true, "Product is Added");

            return new ServiceResponse(true, "Fail to Added Product!");
        }

        public async Task<ServiceResponse> DeleteAsync(Guid Id)
        {
           var result = await genericRepository.DeleteAsync( Id );
            if(result > 0 )
            {
                return new ServiceResponse(true, "Product deleted");
            }
            return new ServiceResponse(false, "Faild to delete Product");
        }

        public async Task<IEnumerable<GetProduct>> GetAllAsync()
        {
           var products = await genericRepository.GetAllAsync();
            if (!products.Any() )
            {
                return [];
            }
            var result =  mapper.Map<IEnumerable< GetProduct>>( products );
            return result;

        }

        public async Task<GetProduct> GetByIdAsync(Guid id)
        {
            var products = await genericRepository.GetByIdAsync(id);
            if(products == null)
            {
               return new GetProduct();
            }
            var result = mapper.Map<GetProduct>(products);
            return result;

        }

        public async Task<ServiceResponse> UpdateAsync(UpdateProduct product)
        {
            var MapingProduct = mapper.Map<Product>(product);
            var result = await genericRepository.UpdateAsync(MapingProduct);
            if (result > 0)
                return new ServiceResponse(true, "Updated Product!");

            return new ServiceResponse(true, "Fail to Update Product");
        }
    }
    
}
