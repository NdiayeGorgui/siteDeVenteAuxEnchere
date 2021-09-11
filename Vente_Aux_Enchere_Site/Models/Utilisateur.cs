using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vente_Aux_Enchere_Site.Models
{
    public class Utilisateur
    {
        public int UtilisateurId { get; set; }

        [MaxLength(70)]
        [Required(ErrorMessage = "Ce champ est Requis")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Ce champ est Requis")]
        [MaxLength(70)]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Ce champ est Requis")]
        [MaxLength(50)]
       // [RegularExpression( " ^ [a-z0-9 _ \\ + -] + (\\ [a-z0-9 _ \\ + -.] +) * @ [A-z0-9 -] + (\\ . [a-z0-9] +) * \\. ([az] {2,4}) $ " , ErrorMessage = " Format de courrier électronique non valide. " )]
        public string Mail { get; set; }


        [Required(ErrorMessage = "Ce champ est Requis")]
        [MaxLength(20)]
        public string Pseudo { get; set; }

        [Required(ErrorMessage = "Ce champ est Requis")]
        [MinLength(4), MaxLength(20)]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Ce champ est Requis")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [DisplayName("Confirmer")]
        public string ConfirmPassword { get; set; }

        public string Adresse { get; set; }
        public string Telephone { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [DisplayName("Date d'adhésion")]
        [DataType(DataType.Date)]
        public DateTime DateAdhesion { get; set; }
        public int NombreObjetVendue { get; set; }
        public int NombreObjetAchete { get; set; }
        public string LoginErrorMessage { get; set; }

        public bool? Remember { get; set; }

        public int? RoleId { get; set; }

        public Role Role { get; set; }

        public ICollection<Bien> Biens { get; set; }

        public ICollection<Evaluation> Evaluations { get; set; }

        public ICollection<Enchere> Encheres { get; set; }
        public virtual ICollection<Facture> Factures { get; set; }

    }
}