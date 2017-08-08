namespace Pack4Travel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AverageProperty3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.equipements", "average", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.equipements", "average");
        }
    }
}
