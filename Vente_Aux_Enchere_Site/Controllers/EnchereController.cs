using Vente_Aux_Enchere_Site.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vente_Aux_Enchere_Site.Filter;

namespace Vente_Aux_Enchere_Site.Controllers
{
    
    [Authorize]
   
    public class EnchereController : Controller
    {
      
        MyDbContext db = new MyDbContext();

       
        // GET: Enchere
        public ActionResult Index()
        {
            ViewBag.liste_enchere = db.Encheres.Include(c => c.Bien)
                .Include(c => c.Utilisateur)
                 .Where(c => c.Bien.DateVente < DateTime.Now)
                .OrderByDescending(i => i.DateEnchere)
               // .GroupBy(i => i.Utilisateur.Pseudo)
                .ToList();

           

            return View(ViewBag.liste_enchere);
        }




        public ActionResult MesEncheres(int? id)
        {
            Enchere enchere = new Enchere();
           
          
           

            //ViewBag.maxprice = db.Encheres.Where(x => x.BienId == id).Select(c => new
            //                      {
            //                          PrixActuel = c.PrixActuel
                                      
            //                      });

            enchere.UtilisateurId = Convert.ToInt32(Session["UtilisateurId"]);

            ViewBag.nombre_enchere = db.Encheres

                             .Include(c => c.Bien)
                             .Include(v => v.Utilisateur)
                             .OrderByDescending(x => x.DateEnchere)
                             .Where(c => c.UtilisateurId == enchere.UtilisateurId).Count();

            return View(db.Encheres

                              .Include(c => c.Bien)
                              .Include(v => v.Utilisateur)
                              .OrderByDescending(x => x.DateEnchere)
                              .Where(c => c.UtilisateurId == enchere.UtilisateurId).ToList());

        }

        
        public ActionResult MesEncheresparproduit(int? id)
        {
            Enchere enchere = new Enchere();


            enchere.UtilisateurId = Convert.ToInt32(Session["UtilisateurId"].ToString());

            ViewBag.nombre_enchere = db.Encheres

                             .Include(c => c.Bien)
                             .Include(v => v.Utilisateur)
                             .OrderByDescending(x => x.DateEnchere)
                             .Where(c => c.UtilisateurId == enchere.UtilisateurId).Count();

            return View(db.Encheres

                              .Include(c => c.Bien)
                              .Include(v => v.Utilisateur)
                              .OrderByDescending(x => x.DateEnchere)
                              .Where(c => c.UtilisateurId == enchere.UtilisateurId).ToList());

        }


        public ActionResult EnchereParProduit(int? id)
        {


            ViewBag.liste_utilisateur = db.Utilisateurs.ToList();
            ViewBag.liste_EnchereParProduit = db.Biens.Include(p => p.Encheres).Include(p => p.Utilisateur).OrderBy(p => p.Designation).ToList();
            var detail = db.Encheres.Where(x => x.EnchereId == id).FirstOrDefault();
           
            return View(detail);
        }

        public ActionResult DetailEnchere(int? id)
        {

            

            ViewBag.nombre_enchere= db.Encheres.Where(x => x.BienId == id).Count();
            ViewBag.liste_encherisseur = db.Encheres.Include(v => v.Utilisateur).OrderByDescending(x => x.DateEnchere).Where(x => x.BienId == id).Take(5).ToList();
            ViewBag.liste_suite = db.Encheres.Include(v => v.Utilisateur).OrderByDescending(x => x.DateEnchere).Where(x => x.BienId == id).Skip(5).ToList();
            Enchere enchere = new Enchere();

            var lastbidder = db.Encheres
                .Include(v => v.Utilisateur)
                .Where(x => x.BienId == id)
                .OrderByDescending(x => x.DateEnchere)
                .FirstOrDefault();
            
           
            enchere.Utilisateur = lastbidder != null ? lastbidder.Utilisateur : null;
            enchere.PrixActuel = db.Encheres.Where(x => x.BienId == id).OrderByDescending(x => x.DateEnchere).Select(v => v.PrixActuel).FirstOrDefault();

            return View(enchere);





         //   Enchere enchere = db.Encheres.Find(id);
          // enchere.Bien.Designation = "Detail du produit" + enchere.Bien.Designation;
           // enchere.Utilisateur = db.Encheres.OrderByDescending(c => c.DateEnchere).First().Utilisateur;
         //   viewmodeldetai=
            //recuperer l'id
            //model.pagetitle="detail du produit"+ model.title
            //model.pagedescription=model.description.substring(0,10)
            //model.lastestbider=db.Encheres.OrderByDescending(c => c.DateEnchere).First().Utilisateur; et dans la vue @Model.lastestbider.username
            // lastestbider get set dans la base.  public user lastestbider get set

           
        }

        public PartialViewResult DetailEnchere1(int? id)
        {


           // ViewBag.maxprice = db.Encheres.Where(x => x.BienId == id).Max(v => v.PrixActuel); pour renvoyer prix max aussi
            ViewBag.nombre_enchere = db.Encheres.Where(x => x.BienId == id).Count();
            ViewBag.liste_encherisseur = db.Encheres.Include(v => v.Utilisateur).OrderByDescending(x => x.DateEnchere).Where(x => x.BienId == id).Take(5).ToList();
            ViewBag.liste_suite = db.Encheres.Include(v => v.Utilisateur).OrderByDescending(x => x.DateEnchere).Where(x => x.BienId == id).Skip(5).ToList();
            Enchere enchere = new Enchere();

            var lastbidder = db.Encheres
                .Include(v => v.Utilisateur)
                .Where(x => x.BienId == id)
                .OrderByDescending(x => x.DateEnchere)
                .FirstOrDefault();


            enchere.Utilisateur = lastbidder != null ? lastbidder.Utilisateur : null;
            enchere.PrixActuel = db.Encheres.Where(x => x.BienId == id).OrderByDescending(x => x.DateEnchere).Select(v => v.PrixActuel).FirstOrDefault();

            return PartialView("_DetailEnchere1", enchere);
        }

        public ActionResult EnchereRemporte(int? id)
        {

            Enchere enchere = new Enchere();

            Bien bien = new Bien();

            //var last_enchere = db.Encheres
            //    .Include(v => v.Utilisateur)
            //    .Where(x => x.BienId == id)
            //    .OrderByDescending(x => x.DateEnchere)
            //    .FirstOrDefault();
           
                enchere.UtilisateurId = Convert.ToInt32(Session["UtilisateurId"].ToString());

                //enchere.Utilisateur = last_enchere != null ? last_enchere.Utilisateur : null;
               
            
            
            return View(db.Encheres
                                 
                                  .Include(c => c.Bien)
                                  .Include(v => v.Utilisateur)
                                  .OrderByDescending(x => x.DateEnchere)
                                //  .Where(c => c.PrixActuel ==enchere.PrixActuel)
                                //  .Where(c => c.BienId == enchere.BienId)
                                
                                  .Where(c => c.Bien
                                  .DateVente < DateTime.Now)
                                  .Where(c => c.UtilisateurId == enchere.UtilisateurId)
                                 //.GroupBy(c => c.Bien.Designation)
                                  .ToList()
                                 // .Max(p => p.PrixActuel)
                                  );
            
        }

    }
}