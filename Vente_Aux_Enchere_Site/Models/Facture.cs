using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vente_Aux_Enchere_Site.Models
{
    public class Facture
    {
        public int FactureId { get; set; }
        public DateTime DateFacture { get; set; }

        public float MontantTotal { get; set; }
        public int? UtilisateurId { get; set; }
        public virtual Utilisateur Utilisateur { get; set; }

        public virtual ICollection<Commande> Commandes { get; set; }
    }
}