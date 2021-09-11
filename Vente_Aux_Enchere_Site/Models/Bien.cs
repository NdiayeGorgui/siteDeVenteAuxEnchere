
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Vente_Aux_Enchere_Site.Models
{
    public class Bien
    {

        public int BienId { get; set; }
        [DisplayName("Produit")]
        public string Designation { get; set; }
        [DisplayName("Etat")]
        public string EtatBien { get; set; }
        [DisplayName("TéléCharger l'image")]
        public string URL_Photo { get; set; }
        public string Photo { get; set; }
        public string TypeVente { get; set; }
      
     
        [DisplayName("Prix Initial")]
        [Required]
        public float PrixInitial { get; set; }
        
        [DisplayName("Prix Actuel")]
        public float? PrixVente { get; set; }
        public string Description { get; set; }
        [DisplayName("Etat")]
        public string EtatVente { get; set; }
        [DisplayName("Date de Publication")]
        public DateTime? DatePublication { get; set; }
        public DateTime? DateAchat { get; set; }
        [DisplayName("Date fin Enchère")]
        [DataType(DataType.Date)]
        public DateTime? DateVente { get; set; }



        [DisplayName("Pseudo")]


        public int? UtilisateurId { get; set; }

        public Utilisateur Utilisateur { get; set; }



        public int? CategorieId { get; set; }



        public Categorie Categorie { get; set; }

        public virtual ICollection<Commande> Commandes { get; set; }

        public virtual ICollection<Enchere> Encheres { get; set; }

        //public int NombreEnchere()
        //{
        //    return Encheres.Count;
        //}











    }
}