using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using System.Data.Entity;

using Vente_Aux_Enchere_Site.Models;

namespace Mon_Site_Vente_Aux_Encheres.Controllers
{
     [Authorize]
    public class CategorieController : Controller
    {

        MyDbContext db = new MyDbContext();
        // GET: Categorie
        public ActionResult Index(int? page_cat, string motcle = "")
        {
            ViewBag.liste_categorie = db.Categories.Where(p => p.NomCategorie.Contains(motcle)).ToList().ToPagedList(page_cat ?? 1, 4);

            ViewBag.motcle = motcle;
            return View(ViewBag.liste_categorie);
        }

        [HttpGet]
        public ActionResult NewCategorie(int id = 0)
        {
            Categorie catmodel = new Categorie();
            return View(catmodel);
        }

        [HttpGet]
        public ActionResult NewCategorie2(int id = 0)
        {
            var catmodel = db.Categories.ToList();
            return View(catmodel);
        }

        public ActionResult SaveCategorie(Categorie categorie)
        {
            try
            {

                db.Categories.Add(categorie);
                db.SaveChanges();
                ModelState.Clear();
                ViewBag.SuccessMessage = "Enregistrement effectué avec succés !";
                return View("NewCategorie", new Categorie());
            }

            catch
            {
                return View();

            }


        }

        [HttpGet]
        public ActionResult NewCategorieProduit(int id = 0)
        {
            ViewBag.liste_bien = db.Biens.Include(p => p.Categorie).Include(p => p.Utilisateur).ToList();
            ViewBag.liste_categorie = db.Categories.ToList();
            Categorie catmodel = new Categorie();


            if (id != 0)

                catmodel = db.Categories.ToList().Where(x => x.CategorieId == id).FirstOrDefault();
            catmodel.Biens = ViewBag.liste_bien;

            return View(catmodel);
        }

        public ActionResult RemoveCategorie(int? id)
        {
            Categorie categorie = new Categorie();
            categorie = db.Categories.Find(id);

            if (categorie != null)
            {

                int lastid = categorie.CategorieId;
                Bien bien = new Bien();
                bien.CategorieId = lastid;
                db.Biens.RemoveRange(db.Biens.Where(c => c.CategorieId == lastid));
                db.SaveChanges();

                db.Categories.Remove(categorie);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Suppression effectuée";
            }
            return RedirectToAction("Index");
        }


    }
}