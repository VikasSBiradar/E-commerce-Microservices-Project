using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;

        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDTO?> CreateProductAsync(ProductDTO productDTO)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = productDTO,
                URL = StaticDetails.ProductAPIBase + "/api/Product"
            });
        }

        public async Task<ResponseDTO?> DeleteProductAsync(int productId)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = StaticDetails.ApiType.DELETE,
                URL = StaticDetails.ProductAPIBase + "/api/Product/" + productId
            });
        }

        public async Task<ResponseDTO?> GetAllProductsAsync()
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = StaticDetails.ApiType.GET,
                URL = StaticDetails.ProductAPIBase + "/api/Product"
            });
        }

        public  async Task<ResponseDTO?> GetProductByIdAsync(int id)
        {

            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = StaticDetails.ApiType.GET,
                URL = StaticDetails.ProductAPIBase + "/api/Product/" + id
            });
        }

        public async Task<ResponseDTO?> UpdateProductAsync(ProductDTO productDTO)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = StaticDetails.ApiType.PUT,
                Data = productDTO,
                URL = StaticDetails.ProductAPIBase + "/api/Product"
            });
        }
    }
}
