using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vente_Aux_Enchere_Site.Models;

namespace Vente_Aux_Enchere_Site.Controllers
{
    public class Item
    {
     private   Bien pr = new Bien();
       

     public Bien Pr {
         get { return pr; }
          set { pr=value ; }
         }

     private int quantite;

     public int Quantite {
         get { return quantite; }
         set { quantite = value; }
           }

        public Item()
     { }

        public Item(Bien produit, int quantite)
        {
            this.pr = produit;
            this.quantite = quantite;
        }

    }
}