using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Vente_Aux_Enchere_Site.Models;

namespace Vente_Aux_Enchere_Site.Controllers
{
    public class AccountController : Controller
    {

        MyDbContext db = new MyDbContext();
        // GET: Account
        public ActionResult Login()
        {


            return View();
        }
        [HttpPost]
        public ActionResult Login(Utilisateur user)
        {

            bool isValid = db.Utilisateurs.Any(x => x.Pseudo == user.Pseudo && x.Password == user.Password);
            if (isValid)
            {
                FormsAuthentication.SetAuthCookie(user.Pseudo, false);
                return RedirectToAction("Catalogue", "Bien");
            }

            ModelState.AddModelError("", "Login ou mot de Passe incorrecte");
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }
    }
}