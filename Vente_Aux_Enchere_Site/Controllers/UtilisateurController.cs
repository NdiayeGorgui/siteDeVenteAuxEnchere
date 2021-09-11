using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Vente_Aux_Enchere_Site.Models;
using PagedList.Mvc;
using PagedList;
using System.Web.Security;


namespace Vente_Aux_Enchere_Site.Controllers
{
     [Authorize]
    public class UtilisateurController : Controller
    {
       
        MyDbContext db = new MyDbContext();
        // GET: Utilisateur
        public ActionResult Index(int? page_user, string motcle = "")
        {
            ViewBag.liste_utilisateur = db.Utilisateurs.Where(p => p.Pseudo.Contains(motcle)).ToList().ToPagedList(page_user ?? 1, 4);
            ViewBag.motcle = motcle;
            return View(ViewBag.liste_utilisateur);
        }
         [AllowAnonymous]
        [HttpGet]
        public ActionResult NewUser(int id = 0)
        {
            Utilisateur usermodel = new Utilisateur();
            return View(usermodel);
        }
         [AllowAnonymous]
        public ActionResult SaveUser(Utilisateur user)
        {
            try
            {
                if (db.Utilisateurs.Any(x => x.Pseudo == user.Pseudo))
                {
                    ViewBag.DuplicateMessage = "Le Pseudo choisi existe dejà !";
                    return View("NewUser", user);
                }
                user.DateAdhesion = DateTime.Now;
                user.RoleId = 2;
                db.Utilisateurs.Add(user);
                db.SaveChanges();
                ModelState.Clear();
                // ViewBag.SuccessMessage = "Enregistrement effectué avec succés !";
                TempData["SuccessMessage"] = user.Prenom + " " + user.Nom + " créé avec succé !";
                //  return View("NewUser", new Utilisateur());
                return RedirectToAction("Login");
            }

            catch
            {
                return View();

            }


        }

        public ActionResult AfficherUser(int? id)
        {
            ViewBag.liste_utilisateur = db.Utilisateurs.ToList();

            var detailuser = db.Utilisateurs.Where(x => x.UtilisateurId == id).FirstOrDefault();

            return View(detailuser);
        }

        public PartialViewResult AfficherUser1(int? id)
        {
            ViewBag.liste_utilisateur = db.Utilisateurs.ToList();

            var detailuser = db.Utilisateurs.Where(x => x.UtilisateurId == id).FirstOrDefault();

            return PartialView("_AfficherUser1", detailuser);
        }

        public ActionResult RemoveUser(int? id)
        {
            Utilisateur user = new Utilisateur();
            user = db.Utilisateurs.Find(id);

            if (user != null)
            {
                
               

                db.Utilisateurs.Remove(user);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Suppression effectuée";
            }
            return RedirectToAction("Index");
        }

        // GET: Account
         [AllowAnonymous]
        public ActionResult Login()
        {


            return View();
        }
         [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Utilisateur user)
        {

            var userdetail = db.Utilisateurs.Where(c => c.Pseudo == user.Pseudo && c.Password == user.Password).FirstOrDefault();
            if (userdetail!=null)
            {
                Session["UtilisateurId"] = userdetail.UtilisateurId.ToString();
                Session["Pseudo"] = userdetail.Pseudo.ToString();

                //HttpCookie c2 = new HttpCookie("UtilisateurId", userdetail.UtilisateurId.ToString());
                //Response.Cookies.Add(c2);
                //c2.Expires = DateTime.Now.AddHours(2);
                //HttpCookie c1 = new HttpCookie("Pseudo", userdetail.Pseudo.ToString());
                //Response.Cookies.Add(c1);
                //c1.Expires = DateTime.Now.AddHours(2);

                FormsAuthentication.SetAuthCookie(user.Pseudo, false);
                return RedirectToAction("Catalogue", "Bien");
            }

            ModelState.AddModelError("", "Login ou mot de Passe incorrecte");
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

    }
}