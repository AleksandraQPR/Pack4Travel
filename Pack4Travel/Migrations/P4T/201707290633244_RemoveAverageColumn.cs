namespace Pack4Travel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAverageColumn : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.equipements", "average");
        }
        
        public override void Down()
        {
            AddColumn("dbo.equipements", "average", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
