using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vente_Aux_Enchere_Site.Models
{
    public class IndexViewModel
    {
        public AfficherViewModel afficherbien { get; set; }
        public DetailViewModel detaienchere { get; set; }
        public virtual IEnumerable<AfficherViewModel> AfficherViewModels { get; set; }
        public virtual ICollection<DetailViewModel> DetailViewModels { get; set; }
    }
}