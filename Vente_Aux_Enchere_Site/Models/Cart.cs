using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vente_Aux_Enchere_Site.Models
{
    public class Cart
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public float Price { get; set; }
        public int Qty { get; set; }
        public float Montant { get; set; }
    }
}