using System.Web.Http;

namespace ApiDevMyGame.App_Start
{
    /// <summary>
    /// 
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public static void Configure(HttpConfiguration config)
        {
            config.Filters.Add(new AuthorizeAttribute());
        }
    }
}