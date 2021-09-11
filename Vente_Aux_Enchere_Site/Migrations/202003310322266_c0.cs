namespace Vente_Aux_Enchere_Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c0 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bien",
                c => new
                    {
                        BienId = c.Int(nullable: false, identity: true),
                        Designation = c.String(),
                        EtatBien = c.String(),
                        URL_Photo = c.String(),
                        Photo = c.String(),
                        PrixInitial = c.Double(nullable: false),
                        PrixVente = c.Double(),
                        Description = c.String(),
                        EtatVente = c.String(),
                        DatePublication = c.DateTime(),
                        DateAchat = c.DateTime(),
                        DateVente = c.DateTime(),
                        UtilisateurId = c.Int(),
                        CategorieId = c.Int(),
                    })
                .PrimaryKey(t => t.BienId)
                .ForeignKey("dbo.Categorie", t => t.CategorieId)
                .ForeignKey("dbo.Utilisateur", t => t.UtilisateurId)
                .Index(t => t.UtilisateurId)
                .Index(t => t.CategorieId);
            
            CreateTable(
                "dbo.Categorie",
                c => new
                    {
                        CategorieId = c.Int(nullable: false, identity: true),
                        NomCategorie = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategorieId);
            
            CreateTable(
                "dbo.Enchere",
                c => new
                    {
                        EnchereId = c.Int(nullable: false, identity: true),
                        DateEnchere = c.DateTime(),
                        PrixActuel = c.Double(nullable: false),
                        BienId = c.Int(),
                    })
                .PrimaryKey(t => t.EnchereId)
                .ForeignKey("dbo.Bien", t => t.BienId)
                .Index(t => t.BienId);
            
            CreateTable(
                "dbo.Utilisateur",
                c => new
                    {
                        UtilisateurId = c.Int(nullable: false, identity: true),
                        Prenom = c.String(nullable: false, maxLength: 70),
                        Nom = c.String(nullable: false, maxLength: 70),
                        Mail = c.String(nullable: false, maxLength: 50),
                        Pseudo = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 20),
                        ConfirmPassword = c.String(nullable: false),
                        Adresse = c.String(),
                        Telephone = c.String(),
                        DateAdhesion = c.DateTime(nullable: false),
                        NombreObjetVendue = c.Int(nullable: false),
                        NombreObjetAchete = c.Int(nullable: false),
                        LoginErrorMessage = c.String(),
                        Remember = c.Boolean(),
                        RoleId = c.Int(),
                    })
                .PrimaryKey(t => t.UtilisateurId)
                .ForeignKey("dbo.Role", t => t.RoleId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Evaluation",
                c => new
                    {
                        EvaluationId = c.Int(nullable: false, identity: true),
                        Note = c.Int(nullable: false),
                        Comentaire = c.String(),
                        DateEvaluation = c.DateTime(nullable: false),
                        UtilisateurId = c.Int(),
                    })
                .PrimaryKey(t => t.EvaluationId)
                .ForeignKey("dbo.Utilisateur", t => t.UtilisateurId)
                .Index(t => t.UtilisateurId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        NomRole = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Utilisateur", "RoleId", "dbo.Role");
            DropForeignKey("dbo.Evaluation", "UtilisateurId", "dbo.Utilisateur");
            DropForeignKey("dbo.Bien", "UtilisateurId", "dbo.Utilisateur");
            DropForeignKey("dbo.Enchere", "BienId", "dbo.Bien");
            DropForeignKey("dbo.Bien", "CategorieId", "dbo.Categorie");
            DropIndex("dbo.Evaluation", new[] { "UtilisateurId" });
            DropIndex("dbo.Utilisateur", new[] { "RoleId" });
            DropIndex("dbo.Enchere", new[] { "BienId" });
            DropIndex("dbo.Bien", new[] { "CategorieId" });
            DropIndex("dbo.Bien", new[] { "UtilisateurId" });
            DropTable("dbo.Role");
            DropTable("dbo.Evaluation");
            DropTable("dbo.Utilisateur");
            DropTable("dbo.Enchere");
            DropTable("dbo.Categorie");
            DropTable("dbo.Bien");
        }
    }
}
