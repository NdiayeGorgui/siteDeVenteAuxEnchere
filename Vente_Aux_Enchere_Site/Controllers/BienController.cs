using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vente_Aux_Enchere_Site.Models;
using PagedList.Mvc;
using PagedList;
using System.IO;
using System.Data.Entity;


namespace Vente_Aux_Enchere_Site.Controllers
{
    [Authorize]
    public class BienController : Controller
    {
        MyDbContext db = new MyDbContext();
        // GET: Bien
        public ActionResult Index(int? page_bien, int? id, string motcle = "")
        {
            Bien bien = new Bien();
            ViewBag.motcle = motcle;
              ViewBag.liste_utilisateur = db.Utilisateurs.ToList();
            // ViewBag.liste_categorie = db.Categories.ToList();

            List<Bien> liste_bien = new List<Bien>();
            liste_bien = db.Biens
               .Include(v => v.Utilisateur)
               .Include(c => c.Categorie).ToList();




            var tempsrestant = db.Biens.Where(b => b.BienId == 64).Select(v => v.DateVente).FirstOrDefault().ToString();


            DateTime dt2 = DateTime.Parse(tempsrestant);

            DateTime dt1 = DateTime.Now;

            TimeSpan ts = dt2.Subtract(dt1);

            ViewBag.tempsrestant = ts.Days;


            return View(db.Biens
             .Include(v => v.Utilisateur)
             .Include(c => c.Categorie).Where(d => d.Designation.Contains(motcle))
                .OrderByDescending(i => i.DatePublication).Where(c => c.DateVente >= DateTime.Now).Where(c => c.TypeVente == "Enchere")
                .ToPagedList(page_bien ?? 1, 3));
        }


        public ActionResult TestSearch(string searchby, string search)
        {
            if(searchby=="Designation")
            {
                var bien = db.Biens.Include(v => v.Utilisateur).Include(c => c.Categorie);
                var model = db.Biens.Where(c => c.Designation == search || search == null).ToList();
                return View(model);
            }
            else
            {
                var bien = db.Biens.Include(v => v.Utilisateur).Include(c => c.Categorie);
                var model = db.Biens.Where(c => c.Description == search || search == null).ToList();
                return View(model);
            }
            

           // return View(bien.ToList());
        }


        [HttpGet]
        public ActionResult NewBien(int? id)
        {

            ViewBag.CategorieId = new SelectList(db.Categories, "CategorieId", "NomCategorie");
            ViewBag.UtilisateurId = new SelectList(db.Utilisateurs, "UtilisateurId", "Pseudo");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveBien(Bien bien)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Request.Files.Count > 0) //si on a au moins un fichier
                    {
                        var file = Request.Files[0]; //le nom de notre fichier
                        if (file != null && file.ContentLength > 0)  //si le fichier est different de null et sa taille est sup à 0 octet
                        {
                            var fileName = Path.GetFileName(file.FileName); // recuperer le nom du fichier
                            var path = Path.Combine(Server.MapPath("~/Fichier"), fileName); // recuperer le chemin d'acces ou sera mis notre fichier
                            file.SaveAs(path); // Enregistrer le tout sur le serveur
                            bien.Photo = fileName;
                            bien.URL_Photo = "/Fichier";
                        }
                    }

                    bien.UtilisateurId = Convert.ToInt32(Session["UtilisateurId"].ToString());
                    bien.DatePublication = DateTime.Now;
                    bien.PrixVente = bien.PrixInitial;
                    bien.TypeVente = "Enchere";
                    db.Biens.Add(bien);
                    db.SaveChanges();

                    //int lastid = bien.BienId;

                    //Enchere enchere = new Enchere();
                    //enchere.DateEnchere = DateTime.Now;
                    //enchere.BienId = lastid;
                    //db.Encheres.Add(enchere);
                    //db.SaveChanges();

                    ModelState.Clear();
                    //  ViewBag.SuccessMessage = "Enregistrement effectué avec succés !";
                    TempData["SuccessMessage"] = "Enregistrement effectué";
                }

                ViewBag.CategorieId = new SelectList(db.Categories, "CategorieId", "NomCategorie", bien.CategorieId);
                ViewBag.UtilisateurId = new SelectList(db.Utilisateurs, "UtilisateurId", "Pseudo", bien.UtilisateurId);
                return RedirectToAction("Index", "Bien");
            }

            catch
            {
                return View();
            }
        }

        public ActionResult AfficherBien(int? id)
        {


            ViewBag.liste_bien = db.Biens
                 .Include(v => v.Utilisateur)
                 .Include(c => c.Categorie)

                 .ToList();

            var detailbien = db.Biens.Include(v => v.Utilisateur).Include(c => c.Categorie).Where(x => x.BienId == id).FirstOrDefault();
            ModelState.Clear();
            return View(detailbien);
        }

        public ActionResult Index1(int? id)
        {
            ViewBag.liste_bien = db.Biens
                          .Include(v => v.Utilisateur)
                          .Include(c => c.Categorie)
                          .ToList();

            Bien bien = db.Biens.Find(id);

            return View(bien);
        }

        public PartialViewResult AfficherBien1(int? id)
        {


            ViewBag.liste_bien = db.Biens
                 .Include(v => v.Utilisateur)
                 .Include(c => c.Categorie)

                 .ToList();

            var detailbien = db.Biens.Include(v => v.Utilisateur).Include(c => c.Categorie).Where(x => x.BienId == id).FirstOrDefault();
      
            return PartialView("_AfficherBien1", detailbien);
        }

        public ActionResult Catalogue(int? id)
        {

            //.Where(c => c.Biens.Any(i => i.TypeVente == "Enchere"))
            
            //var detailcat = db.Biens.Include(v => v.Utilisateur).Where(x => x.BienId == id).FirstOrDefault();
            //return View(detailcat);
            ViewBag.liste_categorie = db.Categories.Include(p => p.Biens).OrderBy(p => p.NomCategorie).ToList();
            //Categorie categorie=db.Categories.FirstOrDefault();
            //ViewBag.liste_bien = db.Entry(categorie).Collection(a => a.Biens).Query().Where(x => x.TypeVente == "Enchere").Load();
            return View();
        }

        [HttpGet]
        public ActionResult ModifierBien(int? id)
        {


            Bien bien = db.Biens.Find(id);

            ViewBag.CategorieId = new SelectList(db.Categories, "CategorieId", "NomCategorie", bien.CategorieId);
            ViewBag.UtilisateurId = new SelectList(db.Utilisateurs, "UtilisateurId", "Pseudo", bien.UtilisateurId);
            return View(bien);
        }

        [HttpPost]
        public ActionResult UpdateBien(int? id, Bien bien)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0) //si on a au moins un fichier
                {
                    var file = Request.Files[0]; //le nom de notre fichier
                    if (file != null && file.ContentLength > 0)  //si le fichier est different de null et sa taille est sup à 0 octet
                    {
                        var fileName = Path.GetFileName(file.FileName); // recuperer le nom du fichier
                        var path = Path.Combine(Server.MapPath("~/Fichier"), fileName); // recuperer le chemin d'acces ou sera mis notre fichier
                        file.SaveAs(path); // Enregistrer le tout sur le serveur
                        bien.Photo = fileName;
                        bien.URL_Photo = "/Fichier";
                    }
                }

                db.Entry(bien).State = EntityState.Modified;
                db.SaveChanges();

                TempData["SuccessMessage"] = "Modification effectuée";
                return RedirectToAction("Index");
            }
            ViewBag.CategorieId = new SelectList(db.Categories, "CategorieId", "NomCategorie", bien.CategorieId);
            ViewBag.UtilisateurId = new SelectList(db.Utilisateurs, "UtilisateurId", "Pseudo", bien.UtilisateurId);
            return View(bien);
        }

        public ActionResult IndexViewModel(int? id)
        {
            ViewBag.liste_bien = db.Biens
               .Include(v => v.Utilisateur)
               .Include(c => c.Categorie)
               .ToList();
            List<AfficherViewModel> listeOfAffiches = new List<AfficherViewModel>();
            List<DetailViewModel> listeODetails = new List<DetailViewModel>();
            List<IndexViewModel> listeOfIndexs = new List<IndexViewModel>();

            //Bien bien = db.Biens.Find(id);
            var vm = new IndexViewModel();


            return View(vm);

        }

        [HttpGet]
        public ActionResult ModifierEnchere(int? id)
        {

            ViewBag.liste_bien = db.Biens
               .Include(v => v.Utilisateur)
               .Include(c => c.Categorie)
               .ToList();

            Bien bien = db.Biens.Find(id);

            return View(bien);

        }

        public PartialViewResult ModifierEnchere1(int? id)
        {

            ViewBag.liste_bien = db.Biens
               .Include(v => v.Utilisateur)
               .Include(c => c.Categorie)
               .ToList();

            Bien bien = db.Biens.Find(id);

            return PartialView("_ModifierEnchere1", bien);

        }




        public ActionResult UpdateEnchere(int? id, Bien bien)
        {
            ViewBag.liste_utilisateur = db.Utilisateurs.ToList();
          

            var dt = DateTime.Now;

            if (ModelState.IsValid)
            {
                var req = db.Biens.SingleOrDefault(x => x.PrixVente >= bien.PrixVente && x.BienId == bien.BienId);
                var req2 = db.Biens.SingleOrDefault(x => x.PrixInitial >= bien.PrixVente && x.BienId == bien.BienId);
                var req3 = db.Biens.SingleOrDefault(x => DateTime.Now > bien.DateVente && x.BienId == bien.BienId);

                if (req != null)
                {
                    ModelState.AddModelError("", "Le Prix que vous avez proposé est inferieur ou égal au prix actuel !");
                    // ViewBag.PrixMessage = "Le Prix que vous avez proposé est inferieur ou égal au prix actuel !";


                    return View("ModifierEnchere", bien);

                }

                if (req2 != null)
                {
                    ModelState.AddModelError("", "Le Prix que vous avez proposé est inferieur ou égal au prix actuel !");

                    return View("ModifierEnchere", bien);
                }
                if (req3 != null)
                {
                    ModelState.AddModelError("", "Attention La date de fin des enchères est dépassé pour ce produit !");
                    //ViewBag.PrixMessage = "Attention La date de fin des enchères est dépassé pour ce produit  !";


                    return View("ModifierEnchere", bien);
                }
                if ((bien.PrixVente) == null)
                {
                    ModelState.AddModelError("", "Saisir une valeur s'il vous plait !");
                    //   ViewBag.PrixMessage = "Saisir une valeur";
                    return View("ModifierEnchere", bien);
                }

                //enchere.Bien.UtilisateurId = Convert.ToInt32(Session["UtilisateurId"].ToString());



                db.Entry(bien).State = EntityState.Modified;
                db.SaveChanges();

                TempData["SuccessMessage"] = "Enchère Sauvegardé";

                int lastid = bien.BienId;
                float? pv = bien.PrixVente;

                Enchere enchere = new Enchere();
                //  enchere.Utilisateur.Pseudo = User.Identity.Name;
                enchere.UtilisateurId = Convert.ToInt32(Session["UtilisateurId"].ToString());
                enchere.DateEnchere = DateTime.Now;

                enchere.BienId = lastid;
                enchere.PrixActuel = pv;
                db.Encheres.Add(enchere);
                db.SaveChanges();

                // return View("Index1", new Bien());
                return RedirectToAction("Catalogue");
            }
            // ViewBag.CategorieId = new SelectList(db.Categories, "CategorieId", "NomCategorie", bien.CategorieId);
            // ViewBag.UtilisateurId = new SelectList(db.Utilisateurs, "UtilisateurId", "Pseudo", bien.UtilisateurId);
            return View(bien);
        }
        //  [Authorize(Roles="Administrateur")]
        public ActionResult SupprimerBien(int id)
        {
            Bien bien = db.Biens.Find(id);
            return View(bien);
        }

        // [HttpPost, ActionName("SupprimerBien")]
        //  [Authorize(Roles = "Administrateur")]
        [HttpGet]
        public ActionResult RemoveBien(int? id)
        {
            Bien bien = new Bien();
            bien = db.Biens.Find(id);

            if (bien != null)
            {
                int lastid = bien.BienId;
                Enchere enchere = new Enchere();
                enchere.BienId = lastid;
                db.Encheres.RemoveRange(db.Encheres.Where(c => c.BienId == lastid));
                db.SaveChanges();

                db.Biens.Remove(bien);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Suppression effectuée";
            }
            return RedirectToAction("Index");
        }

        public ActionResult DetailEnchere()
        {

            return View();
        }

        public ActionResult IndexViewModel()
        {
            IndexViewModel vm = new IndexViewModel();
            // vm.afficherbien=

            return View();
        }

        [HttpGet]
        public ActionResult RemoveEnchereDepasse(int? id)
        {
            Bien bien = new Bien();
            bien = db.Biens.Find(id);

            if (bien != null)
            {
                int lastid = bien.BienId;
                Enchere enchere = new Enchere();
                enchere.BienId = lastid;
                db.Encheres.RemoveRange(db.Encheres.Where(c => c.BienId == lastid));
                db.SaveChanges();

                db.Biens.Remove(bien);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Suppression effectuée";
            }
            return RedirectToAction("EnchereDepasse");
        }


        public ActionResult EnchereDepasse()
        {
            ViewBag.liste_enchere = db.Biens
                .Include(c => c.Utilisateur)
                
                 .Where(c => c.DateVente < DateTime.Now)
               
                .ToList();



            return View(ViewBag.liste_enchere);
        }

    }
}