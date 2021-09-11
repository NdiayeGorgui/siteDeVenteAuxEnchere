using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vente_Aux_Enchere_Site.Models;

namespace Vente_Aux_Enchere_Site.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        MyDbContext db = new MyDbContext();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }

        private int isExisting(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Pr.BienId == id)
                    return i;
            return -1;
        }
        public ActionResult Commander(int id)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item(db.Biens.Find(id),1));
                Session["cart"] = cart;

            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = isExisting(id);
                if (index == -1)
                    cart.Add(new Item(db.Biens.Find(id), 1));
                else
                    cart[index].Quantite++;
                Session["cart"] = cart;
            }

            return View("Cart");
        }
        public ActionResult Delete(int id)
        {
            int index = isExisting(id);
            List<Item> cart = (List<Item>)Session["cart"];
            cart.RemoveAt(index);
            Session["cart"] = cart;

            return View("Cart");
        }
    }
}