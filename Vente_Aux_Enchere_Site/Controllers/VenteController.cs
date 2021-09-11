using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vente_Aux_Enchere_Site.Models;
using System.Data.Entity;
using PagedList.Mvc;
using PagedList;

namespace Vente_Aux_Enchere_Site.Controllers
{
    [Authorize]
    public class VenteController : Controller
    {
        MyDbContext db = new MyDbContext();
        // GET: Vente
        public ActionResult Index()
        {
            //Session["UtilisateurId"]= Convert.ToInt32(Session["UtilisateurId"].ToString());
            //Session["UtilisateuId"] = 3;
            if (TempData["Cart"] != null)
            {
                float x = 0;
                List<Cart> li2 = TempData["Cart"] as List<Cart>;
                foreach (var item in li2)
                {
                    x += item.Montant;

                }
                TempData["Total"] = x;
            }
            TempData.Keep();
            return View(db.Biens.OrderByDescending(x => x.BienId).Where(c => c.TypeVente == "Imediat").ToList());
        }


        [HttpGet]
        public ActionResult AddToCart(int? id)
        {
            Bien p = db.Biens.Where(x => x.BienId == id).SingleOrDefault();
            return View(p);
        }

        List<Cart> li = new List<Cart>();

        [HttpPost]
        public ActionResult AddToCart(Bien pi, string qty, int? Id)
        {
            Bien p = db.Biens.Where(x => x.BienId == Id).SingleOrDefault();

            Cart c = new Cart();
            c.ProductId = p.BienId;
            c.ProductName = p.Designation;
            c.Price = p.PrixInitial;
            c.Qty = Convert.ToInt32(qty);
            c.Montant = c.Price * c.Qty;

            if (TempData["Cart"] == null)
            {
                li.Add(c);
                TempData["Cart"] = li;
            }

            else
            {
                //List<Cart> li2 = TempData["Cart"] as List<Cart>;
                //li2.Add(c);
                //TempData["Cart"] = li2;

                List<Cart> li2 = TempData["Cart"] as List<Cart>;
                int flag = 0;
                foreach (var item in li2)
                {
                    if (item.ProductId == c.ProductId)
                    {
                        item.Qty += c.Qty;
                        item.Montant = c.Montant;
                        flag = 1;
                    }
                }
                if (flag == 0)
                {
                    li2.Add(c);
                }
                TempData["Cart"] = li2;
            }

            TempData.Keep();
            return RedirectToAction("Index");
        }

        public ActionResult Remove(int? id)
        {
            List<Cart> li2 = TempData["Cart"] as List<Cart>;
            Cart c = li2.Where(x => x.ProductId == id).SingleOrDefault();
            li2.Remove(c);

            float h = 0;
            foreach (var item in li2)
            {
                h += item.Montant;
            }

            TempData["Total"] = h;

            return RedirectToAction("Checkout");

        }

        public ActionResult Checkout()
        {
            TempData.Keep();
            return View();

        }
        [HttpPost]
        public ActionResult Checkout(Commande com)
        {

            List<Cart> li = TempData["Cart"] as List<Cart>;

            Facture fact = new Facture();
            fact.UtilisateurId = Convert.ToInt32(Session["UtilisateurId"].ToString());
            fact.DateFacture = System.DateTime.Now;
            fact.MontantTotal = (float)TempData["Total"];
            db.Factures.Add(fact);
            db.SaveChanges();

            foreach (var item in li)
            {
                Commande co = new Commande();
                co.BienId = item.ProductId;
                co.FactureId = fact.FactureId;
                co.DateCommande = System.DateTime.Now;
                co.Quantite = item.Qty;

                co.PrixUnitaire = (int)item.Price;
                co.Montant = item.Montant;

                db.Commandes.Add(co);
                db.SaveChanges();
            }

            TempData.Remove("Total");
            TempData.Remove("Cart");
            TempData["msg"] = "Transaction effectuée avec succés";
            TempData.Keep();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult NewVente(int? id)
        {

            ViewBag.CategorieId = new SelectList(db.Categories, "CategorieId", "NomCategorie");
            ViewBag.UtilisateurId = new SelectList(db.Utilisateurs, "UtilisateurId", "Pseudo");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveVente(Bien bien)
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
                    bien.TypeVente = "Imediat";
                    db.Biens.Add(bien);
                    db.SaveChanges();

                   

                    ModelState.Clear();
                    //  ViewBag.SuccessMessage = "Enregistrement effectué avec succés !";
                    TempData["SuccessMessage"] = "Enregistrement effectué";
                }

                ViewBag.CategorieId = new SelectList(db.Categories, "CategorieId", "NomCategorie", bien.CategorieId);
                ViewBag.UtilisateurId = new SelectList(db.Utilisateurs, "UtilisateurId", "Pseudo", bien.UtilisateurId);
                return RedirectToAction("Index", "Vente");
            }

            catch
            {
                return View();
            }
        }


        public ActionResult ListeImediat(int? page_bien, int? id, string motcle = "")
        {
            ViewBag.motcle = motcle;
            ViewBag.liste_utilisateur = db.Utilisateurs.ToList();
            return View(db.Biens
             .Include(v => v.Utilisateur)
             .Include(c => c.Categorie).Where(d => d.Designation.Contains(motcle))
                .OrderByDescending(i => i.DatePublication).Where(c => c.TypeVente == "Imediat")
               .ToPagedList(page_bien ?? 1, 3));
        }
    }
}