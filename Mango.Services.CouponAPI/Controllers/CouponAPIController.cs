using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDBContext _dbContext;
        private ResponseDTO _response;
        private IMapper _mapper;

        public CouponAPIController(AppDBContext dbContext, IMapper mapper)
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
                IEnumerable<Coupon> coupons = _dbContext.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDTO>>(coupons);
              
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
                Coupon coupon = _dbContext.Coupons.First(u => u.CouponId == id);
                _response.Result = _mapper.Map<CouponDTO>(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("Code/{code}")]
        public ResponseDTO GetCouponByCode(string code)
        {
            try
            {
                Coupon coupon = _dbContext.Coupons.First(u => u.CouponCode.ToLower() == code.ToLower());
                _response.Result = _mapper.Map<CouponDTO>(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public ResponseDTO SaveCoupon([FromBody] CouponDTO couponDTO)
        {
            try
            {
                Coupon coupon = _mapper.Map<Coupon>(couponDTO);
                _dbContext.Add(coupon);
                _dbContext.SaveChanges();
                _response.Result = _mapper.Map<CouponDTO>(coupon);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        public ResponseDTO UpdateCoupon([FromBody] CouponDTO couponDTO)
        {
            try
            {
                Coupon coupon = _mapper.Map<Coupon>(couponDTO);
                _dbContext.Update(coupon);
                _dbContext.SaveChanges();
                _response.Result = _mapper.Map<CouponDTO>(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Route("{couponId:int}")]
        public ResponseDTO DeleteCoupon(int couponId)
        {
            try
            {
                Coupon coupon = _dbContext.Coupons.First(x => x.CouponId == couponId);
                _dbContext.Remove(coupon);
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
