using AutoMapper;
using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Product;
using eCommerce.Application.Services.Interfaces;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces;
using eCommerce.Domain.Interfaces.UnitOfWork;
namespace eCommerce.Application.Services.Implementations
{
    public class ProductService: IProductService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ProductService(IRepositoryManager repositoryManager , IMapper mapper )
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }
        public async Task<ServiceResponse> AddProductAsync(CreateProduct product , CancellationToken cancellationToken)
        {
            var productEntity = _mapper.Map<Product>( product );
          
           
            _repositoryManager.Product.AddProduct(productEntity);
            var result = await _repositoryManager.CompleteAsync(cancellationToken);
          
             return result > 0 ? new ServiceResponse(true, "Product is Added"):
                           new ServiceResponse(true, "Fail to Added Product!");
        }

        public async Task<ServiceResponse> DeleteProductAsync(int Id , CancellationToken cancellationToken=default)
        {

             var product = await _repositoryManager.Product.GetProductByIdAsync(Id, trackChanges: false);
            if(product is null)
                return new ServiceResponse(false, "Product not found");
            _repositoryManager.Product.DeleteProduct(product);
            var result = await _repositoryManager.CompleteAsync(cancellationToken);
            return result > 0 ? new ServiceResponse(true, "Product deleted") :
                           new ServiceResponse(false, "Faild to delete Product");
        
        }

        public async Task<IEnumerable<GetProduct>> GetAllProductAsync()
        {
           
          var products = await _repositoryManager.Product.GetAllProductsAsync(false);
            if (!products.Any() )
                return [];
            
            var result =  _mapper.Map<IEnumerable<GetProduct>>( products );
            return result;

        }

        public async Task<IEnumerable<GetProduct>> GetProductByCategoryIdAsync(int categoryId)
        {
           var products = await _repositoryManager.Product.GetProductsByCategory(categoryId, false);
            if (!products.Any())
                return [];
            var result = _mapper.Map<IEnumerable<GetProduct>>(products);
            return result;
        }

        public async Task<GetProduct> GetProductByIdAsync(int id)
        {
         
           var products = await _repositoryManager.Product.GetProductByIdAsync(id, false);
           if (products is null)
                return new GetProduct();
            var result = _mapper.Map<GetProduct>(products);
            return result;

        }

        public async Task<ServiceResponse> UpdateProductAsync(UpdateProduct product , CancellationToken cancellationToken=default)
        {
           var productEntity = await _repositoryManager.Product.GetProductByIdAsync(product.Id, trackChanges: true);
            if (productEntity is null)
                return new ServiceResponse(false, "Product not found");
            _mapper.Map(product, productEntity);
            _repositoryManager.Product.UpdateProduct(productEntity);
            var result = await _repositoryManager.CompleteAsync(cancellationToken);
            return result > 0 ? new ServiceResponse(true, "Product is Updated") :
                           new ServiceResponse(false, "Fail to Update Product!");
        }
    }
    
}
