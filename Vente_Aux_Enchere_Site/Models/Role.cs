using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Vente_Aux_Enchere_Site.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        [DisplayName("Role")]
        public string NomRole { get; set; }


        public virtual ICollection<Utilisateur> Utilisateurs { get; set; }
    }
}