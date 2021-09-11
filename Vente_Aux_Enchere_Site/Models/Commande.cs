using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vente_Aux_Enchere_Site.Models
{
    public class Commande
    {
        public int CommandeId { get; set; }
        public DateTime DateCommande { get; set; }
        public int Quantite { get; set; }
        public float PrixUnitaire { get; set; }
        public float Montant { get; set; }
        public int? FactureId { get; set; }
        
        public int? BienId { get; set; }
        public virtual Facture Facture { get; set; }
        public virtual Bien Bien { get; set; }
    }
}