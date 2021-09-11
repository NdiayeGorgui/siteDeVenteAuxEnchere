namespace Vente_Aux_Enchere_Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bien", "Quantite", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bien", "Quantite");
        }
    }
}
