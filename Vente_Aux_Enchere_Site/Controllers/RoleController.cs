using Vente_Aux_Enchere_Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vente_Aux_Enchere_Site.Controllers
{
    public class RoleController : Controller
    {
        MyDbContext db = new MyDbContext();
        // GET: Role

        public ActionResult Index()
        {
            ViewBag.liste_role = db.Roles.ToList();
            return View(db.Roles.ToList());
        }

        public ActionResult Index2()
        {
            ViewBag.liste_utilisateur = db.Utilisateurs.ToList();

            return View(ViewBag.liste_utilisateur);
        }

        [HttpGet]
        public ActionResult CreateRole(int id = 0)
        {
            Role rolemodel = new Role();
            return View(rolemodel);
        }

        [HttpPost]
        public ActionResult SaveRole(Role role)
        {

            try
            {

                db.Roles.Add(role);
                db.SaveChanges();
                ModelState.Clear();
                ViewBag.SuccessMessage = "Enregistrement effectué avec succés !";
                return View("CreateRole", new Role());
            }

            catch
            {
                return View();

            }

        }


    }
}