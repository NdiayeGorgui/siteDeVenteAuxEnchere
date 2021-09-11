namespace Vente_Aux_Enchere_Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c9 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bien", "PrixInitial", c => c.Single(nullable: false));
            AlterColumn("dbo.Bien", "PrixVente", c => c.Single());
            AlterColumn("dbo.Commande", "PrixUnitaire", c => c.Single(nullable: false));
            AlterColumn("dbo.Commande", "Montant", c => c.Single(nullable: false));
            AlterColumn("dbo.Facture", "MontantTotal", c => c.Single(nullable: false));
            AlterColumn("dbo.Enchere", "PrixActuel", c => c.Single());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Enchere", "PrixActuel", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Facture", "MontantTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Commande", "Montant", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Commande", "PrixUnitaire", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Bien", "PrixVente", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Bien", "PrixInitial", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
