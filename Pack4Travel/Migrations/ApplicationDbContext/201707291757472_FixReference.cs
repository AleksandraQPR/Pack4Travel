namespace Pack4Travel.Migrations.ApplicationDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixReference : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.equipements", name: "Id_Id", newName: "Id");
            RenameColumn(table: "dbo.items", name: "Id_Id", newName: "Id");
            RenameIndex(table: "dbo.equipements", name: "IX_Id_Id", newName: "IX_Id");
            RenameIndex(table: "dbo.items", name: "IX_Id_Id", newName: "IX_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.items", name: "IX_Id", newName: "IX_Id_Id");
            RenameIndex(table: "dbo.equipements", name: "IX_Id", newName: "IX_Id_Id");
            RenameColumn(table: "dbo.items", name: "Id", newName: "Id_Id");
            RenameColumn(table: "dbo.equipements", name: "Id", newName: "Id_Id");
        }
    }
}
