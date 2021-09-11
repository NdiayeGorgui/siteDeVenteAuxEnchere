namespace Vente_Aux_Enchere_Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Commande",
                c => new
                    {
                        CommandeId = c.Int(nullable: false, identity: true),
                        DateCommande = c.DateTime(nullable: false),
                        Quantite = c.Int(nullable: false),
                        PrixUnitaire = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Montant = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FactureId = c.Int(),
                        BienId = c.Int(),
                    })
                .PrimaryKey(t => t.CommandeId)
                .ForeignKey("dbo.Bien", t => t.BienId)
                .ForeignKey("dbo.Facture", t => t.FactureId)
                .Index(t => t.FactureId)
                .Index(t => t.BienId);
            
            CreateTable(
                "dbo.Facture",
                c => new
                    {
                        FactureId = c.Int(nullable: false, identity: true),
                        DateFacture = c.DateTime(nullable: false),
                        MontantTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UtilisateurId = c.Int(),
                    })
                .PrimaryKey(t => t.FactureId)
                .ForeignKey("dbo.Utilisateur", t => t.UtilisateurId)
                .Index(t => t.UtilisateurId);
            
            AlterColumn("dbo.Bien", "PrixInitial", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Bien", "PrixVente", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Enchere", "PrixActuel", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Facture", "UtilisateurId", "dbo.Utilisateur");
            DropForeignKey("dbo.Commande", "FactureId", "dbo.Facture");
            DropForeignKey("dbo.Commande", "BienId", "dbo.Bien");
            DropIndex("dbo.Facture", new[] { "UtilisateurId" });
            DropIndex("dbo.Commande", new[] { "BienId" });
            DropIndex("dbo.Commande", new[] { "FactureId" });
            AlterColumn("dbo.Enchere", "PrixActuel", c => c.Double());
            AlterColumn("dbo.Bien", "PrixVente", c => c.Double());
            AlterColumn("dbo.Bien", "PrixInitial", c => c.Double(nullable: false));
            DropTable("dbo.Facture");
            DropTable("dbo.Commande");
        }
    }
}
