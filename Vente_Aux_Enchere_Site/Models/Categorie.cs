using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vente_Aux_Enchere_Site.Models
{
    public partial class Categorie
    {
        public int CategorieId { get; set; }

        [Required(ErrorMessage = "Ce champ est Requis")]
        [DisplayName("Categorie")]
        public string NomCategorie { get; set; }


        public virtual ICollection<Bien> Biens { get; set; }

    }
}