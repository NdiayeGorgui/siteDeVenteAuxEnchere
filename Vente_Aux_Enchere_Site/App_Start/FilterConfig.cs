using System.Web;
using System.Web.Mvc;

namespace Vente_Aux_Enchere_Site
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
