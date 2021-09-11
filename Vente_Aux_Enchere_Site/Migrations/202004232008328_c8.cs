namespace Vente_Aux_Enchere_Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bien", "TypeVente", c => c.String());
            DropColumn("dbo.Bien", "Tempsrestant");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bien", "Tempsrestant", c => c.String());
            DropColumn("dbo.Bien", "TypeVente");
        }
    }
}
