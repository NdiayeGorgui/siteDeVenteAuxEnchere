using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vente_Aux_Enchere_Site.Models
{
    public class Evaluation
    {
        public int EvaluationId { get; set; }
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        [Range(0, 10)]
        public int Note { get; set; }
        public string Comentaire { get; set; }
        public DateTime DateEvaluation { get; set; }



        public int? UtilisateurId { get; set; }
        public Utilisateur Utilisateur { get; set; }





    }
}