namespace Mango.Web.Utility
{
    public class StaticDetails
    {
        public  enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
        public static string CouponAPIBase { get; set; }
    }
}
