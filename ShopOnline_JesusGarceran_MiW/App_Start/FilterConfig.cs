using System.Web;
using System.Web.Mvc;

namespace ShopOnline_JesusGarceran_MiW
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
