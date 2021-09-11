using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vente_Aux_Enchere_Site.Models;

namespace Vente_Aux_Enchere_Site.Controllers
{
    [Authorize]
    public class ProduitController : Controller
    {
        MyDbContext db = new MyDbContext();
        // GET: Produit
        public ActionResult Index()
        {
            ViewBag.listeproduit = db.Biens.ToList();
            return View();
        }
    }
}