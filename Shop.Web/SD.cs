using Microsoft.AspNetCore.Identity;

namespace Shop.Web
{
    public static  class SD
    {
        public static string ProductAPIBase { get; set; }
        public static string CartAPIBase { get; set; }
        public enum ApiType
        {
            GET,
            POST,
            PUT, 
            DELETE
        }
    }
}
