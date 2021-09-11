using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vente_Aux_Enchere_Site.Filter;
using Vente_Aux_Enchere_Site.Models;

namespace Vente_Aux_Enchere_Site.Controllers
{
    public class FilterController : Controller
    {
        MyDbContext db = new MyDbContext();
        // GET: Filter
        [ExFilter]
        //[HandleError]
        public ActionResult Index()
        {

            int[] a = new int[5];
            a[70] = 45;
            return View(a[70]);
        }

        // handlererror agit sur le controller il ne faut pas oublier d'aller à webconfig et ajouter dans systeme.web  <customErrors mode="On" />
        public ActionResult hand()
        {

            int[] a = new int[5];
            a[70] = 45;
            return View(a[70]);
        }
        
        [OutputCache(Duration=10)]
        public string Cache()
        {


            return DateTime.Now.ToString();
        }

        
    }
}