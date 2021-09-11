using Vente_Aux_Enchere_Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vente_Aux_Enchere_Site.Controllers
{
     [Authorize]
    public class EvaluationController : Controller
    {
        MyDbContext db = new MyDbContext();
        // GET: Evaluation
        public ActionResult Index()
        {
            ViewBag.liste_utilisateur = db.Utilisateurs.ToList();
            ViewBag.liste_evaluation = db.Evaluations.OrderByDescending(i => i.DateEvaluation).ToList();

            return View(ViewBag.liste_evaluation);
        }

       

        public ActionResult Groupbynote()
        {

            //ViewBag.evalpos = db.Evaluations.Where(c => c.Note >= 5).ToList();
            ViewBag.liste_utilisateur = db.Utilisateurs.ToList();
            ViewBag.liste_evaluation = db.Evaluations.OrderByDescending(i => i.DateEvaluation).GroupBy(c => c.Note).ToList();

            return View(db.Evaluations.OrderByDescending(i => i.DateEvaluation).ToList());
        }


        public ActionResult Index2(Evaluation evaluation)
        {
            ViewBag.UtilisateurId = new SelectList(db.Utilisateurs, "UtilisateurId", "Pseudo");


            try
            {
                if (ModelState.IsValid)
                {
                    evaluation.DateEvaluation = DateTime.Now;
                    //evaluation.UtilisateurId = Convert.ToInt32(Session["UtilisateurId"].ToString());
                    db.Evaluations.Add(evaluation);
                    db.SaveChanges();
                    ModelState.Clear();


                }

                ViewBag.UtilisateurId = new SelectList(db.Utilisateurs, "UtilisateurId", "Pseudo", evaluation.UtilisateurId);
                return View("Index2", new Evaluation());
            }

            catch
            {
                return View();

            }
        }


        [HttpGet]
        public ActionResult NewEvaluation(int id = 0)
        {
            ViewBag.UtilisateurId = new SelectList(db.Utilisateurs, "UtilisateurId", "Pseudo");
            Evaluation evaluation = new Evaluation();

            return View(evaluation);
        }

        public ActionResult SaveEvaluation(Evaluation evaluation)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    evaluation.DateEvaluation = DateTime.Now;
                    db.Evaluations.Add(evaluation);
                    db.SaveChanges();
                    ModelState.Clear();
                    TempData["SuccessMessage"] = "Enregistrement effectué";

                }

                ViewBag.UtilisateurId = new SelectList(db.Utilisateurs, "UtilisateurId", "Pseudo", evaluation.UtilisateurId);
                return View("NewEvaluation", new Evaluation());
            }

            catch
            {
                return View();

            }
        }

    }
}