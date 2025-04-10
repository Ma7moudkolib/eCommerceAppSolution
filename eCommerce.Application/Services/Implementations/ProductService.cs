using AutoMapper;
using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Product;
using eCommerce.Application.Services.Interfaces;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces;
using eCommerce.Domain.Interfaces.UnitOfWork;
namespace eCommerce.Application.Services.Implementations
{
    public class ProductService(IUnitOfWork unitOfWork , IMapper mapper ) : IProductService
    {
        
        public async Task<ServiceResponse> AddAsync(CreateProduct product)
        {
            var MapingProduct = mapper.Map<Product>( product );
           // var result = await genericRepository.AddAsync( MapingProduct );
            await unitOfWork.Products.AddAsync( MapingProduct );
            var result= await unitOfWork.CompleteAsync();
            if (result > 0)
                return new ServiceResponse(true, "Product is Added");
            
            unitOfWork.Dispose();
            return new ServiceResponse(true, "Fail to Added Product!");
        }

        public async Task<ServiceResponse> DeleteAsync(Guid Id)
        {
          // var result = await genericRepository.DeleteAsync( Id );
             await unitOfWork.Products.DeleteAsync( Id );
            var result = await unitOfWork.CompleteAsync();
            if(result > 0 )
            {
                return new ServiceResponse(true, "Product deleted");
            }
            unitOfWork.Dispose();
            return new ServiceResponse(false, "Faild to delete Product");
        }

        public async Task<IEnumerable<GetProduct>> GetAllAsync()
        {
          // var products = await genericRepository.GetAllAsync();
          var products = await unitOfWork.Products.GetAllAsync();
            if (!products.Any() )
            {
                return [];
            }
            var result =  mapper.Map<IEnumerable< GetProduct>>( products );
            return result;

        }

        public async Task<GetProduct> GetByIdAsync(Guid id)
        {
           // var products = await genericRepository.GetByIdAsync(id);
           var products = await unitOfWork.Products.GetByIdAsync( id );
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
           // var result = await genericRepository.UpdateAsync(MapingProduct);
            unitOfWork.Products.UpdateAsync( MapingProduct );
            var result = await unitOfWork.CompleteAsync();
            if (result > 0)  
                return new ServiceResponse(true, "Updated Product!");
            
            unitOfWork.Dispose();
            return new ServiceResponse(true, "Fail to Update Product");
        }
    }
    
}
