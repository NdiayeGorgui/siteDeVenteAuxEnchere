using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vente_Aux_Enchere_Site.Models
{
    public class AfficherViewModel
    {
        public string BienId { get; set; }
        public string Designation { get; set; }
        public string EtatBien { get; set; }
        public double PrixInitial { get; set; }
        public double? PrixVente { get; set; }
        public string Description { get; set; }
        public DateTime? DatePublication { get; set; }
        public DateTime? DateVente { get; set; }
        public string NomCategorie { get; set; }
        public string Pseudo { get; set; }
        public string Telephone { get; set; }
        public string URL_Photo { get; set; }
        public string Photo { get; set; }
        public int? UtilisateurId { get; set; }

        public Utilisateur Utilisateur { get; set; }



        public int? CategorieId { get; set; }



        public Categorie Categorie { get; set; }

        public virtual ICollection<Enchere> Encheres { get; set; }

    }
}