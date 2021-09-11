namespace Vente_Aux_Enchere_Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bien", "Tempsrestant", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bien", "Tempsrestant");
        }
    }
}
