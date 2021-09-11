namespace Vente_Aux_Enchere_Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Bien", "Quantite");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bien", "Quantite", c => c.Int());
        }
    }
}
