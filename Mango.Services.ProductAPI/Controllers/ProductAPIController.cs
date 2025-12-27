using AutoMapper;
using Mango.Services.ProductAPI.Data;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers
{
    [Route("api/Product")]
    [ApiController]
    [Authorize]
    public class ProductAPIController : ControllerBase
    {
        private readonly AppDBContext _dbContext;
        private ResponseDTO _response;
        private IMapper _mapper;

        public ProductAPIController(AppDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _response = new ResponseDTO();
            _mapper = mapper;
        }

        [HttpGet]
        public ResponseDTO Get()
        {
            try
            {
                IEnumerable<Product> Products = _dbContext.Products.ToList();
                _response.Result = _mapper.Map<IEnumerable<ProductDTO>>(Products);
              
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDTO Get(int id)
        {
            try
            {
                Product Product = _dbContext.Products.First(u => u.ProductId == id);
                _response.Result = _mapper.Map<ProductDTO>(Product);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDTO SaveProduct([FromBody] ProductDTO ProductDTO)
        {
            try
            {
                Product Product = _mapper.Map<Product>(ProductDTO);
                _dbContext.Add(Product);
                _dbContext.SaveChanges();
                _response.Result = _mapper.Map<ProductDTO>(Product);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDTO UpdateProduct([FromBody] ProductDTO ProductDTO)
        {
            try
            {
                Product Product = _mapper.Map<Product>(ProductDTO);
                _dbContext.Update(Product);
                _dbContext.SaveChanges();
                _response.Result = _mapper.Map<ProductDTO>(Product);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        //[Authorize(Roles = "ADMIN")]
        [Route("{ProductId:int}")]
        public ResponseDTO DeleteProduct(int ProductId)
        {
            try
            {
                Product Product = _dbContext.Products.First(x => x.ProductId == ProductId);
                _dbContext.Remove(Product);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
