using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;

        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDTO?> CreateCouponAsync(CouponDTO couponDTO)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = couponDTO,
                URL = StaticDetails.CouponAPIBase + "/api/coupon"
            });
        }

        public async Task<ResponseDTO?> DeleteCouponAsync(int couponId)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = StaticDetails.ApiType.DELETE,
                URL = StaticDetails.CouponAPIBase + "/api/coupon/" + couponId
            });
        }

        public async Task<ResponseDTO?> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = StaticDetails.ApiType.GET,
                URL = StaticDetails.CouponAPIBase + "/api/coupon"
            });
            //throw new NotImplementedException();
        }

        public async Task<ResponseDTO?> GetCouponAsync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = StaticDetails.ApiType.GET,
                URL = StaticDetails.CouponAPIBase + "/api/coupon/" + couponCode
            });
            //throw new NotImplementedException();
        }

        public  async Task<ResponseDTO?> GetCouponByIdAsync(int id)
        {

            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = StaticDetails.ApiType.GET,
                URL = StaticDetails.CouponAPIBase + "/api/coupon/" + id
            });
        }

        public async Task<ResponseDTO?> UpdateCouponAsync(CouponDTO couponDTO)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = StaticDetails.ApiType.PUT,
                Data = couponDTO,
                URL = StaticDetails.CouponAPIBase + "/api/coupon"
            });
        }
    }
}
