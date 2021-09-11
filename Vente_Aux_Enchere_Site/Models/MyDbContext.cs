using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Vente_Aux_Enchere_Site.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
            : base("MyDbContext")
        {
        }

        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Bien> Biens { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Enchere> Encheres { get; set; }
        public DbSet<Facture> Factures { get; set; }
        public DbSet<Commande> Commandes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }


    }
}