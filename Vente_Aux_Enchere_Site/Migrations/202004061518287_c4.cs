namespace Vente_Aux_Enchere_Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enchere", "UtilisateurId", c => c.Int());
            CreateIndex("dbo.Enchere", "UtilisateurId");
            AddForeignKey("dbo.Enchere", "UtilisateurId", "dbo.Utilisateur", "UtilisateurId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Enchere", "UtilisateurId", "dbo.Utilisateur");
            DropIndex("dbo.Enchere", new[] { "UtilisateurId" });
            DropColumn("dbo.Enchere", "UtilisateurId");
        }
    }
}
