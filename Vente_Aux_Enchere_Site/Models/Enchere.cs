using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vente_Aux_Enchere_Site.Models
{
    public class Enchere
    {

        public int EnchereId { get; set; }

        public DateTime? DateEnchere { get; set; }

        public float? PrixActuel { get; set; }

        public int? BienId { get; set; }
        public Bien Bien { get; set; }
        public int? UtilisateurId { get; set; }
        public Utilisateur Utilisateur { get; set; }

       
    }
}