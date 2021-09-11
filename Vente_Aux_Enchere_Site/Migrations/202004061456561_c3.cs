namespace Vente_Aux_Enchere_Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Enchere", "PrixActuel", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Enchere", "PrixActuel", c => c.Double(nullable: false));
        }
    }
}
