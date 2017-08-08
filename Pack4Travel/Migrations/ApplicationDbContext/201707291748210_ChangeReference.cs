namespace Pack4Travel.Migrations.ApplicationDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeReference : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.equipements", "userInfo_idUser", "dbo.userInfo");
            DropForeignKey("dbo.items", "userInfo_idUser", "dbo.userInfo");
            DropIndex("dbo.equipements", new[] { "userInfo_idUser" });
            DropIndex("dbo.items", new[] { "userInfo_idUser" });
            AddColumn("dbo.equipements", "Id_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.items", "Id_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.equipements", "Id_Id");
            CreateIndex("dbo.items", "Id_Id");
            AddForeignKey("dbo.equipements", "Id_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.items", "Id_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.equipements", "idOwner");
            DropColumn("dbo.equipements", "userInfo_idUser");
            DropColumn("dbo.items", "idOwner");
            DropColumn("dbo.items", "userInfo_idUser");
            DropTable("dbo.userInfo");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.userInfo",
                c => new
                    {
                        idUser = c.Int(nullable: false, identity: true),
                        userName = c.String(nullable: false, maxLength: 20),
                        mail = c.String(nullable: false, maxLength: 40),
                        password = c.String(nullable: false, maxLength: 30),
                        activeStatus = c.String(nullable: false, maxLength: 1),
                    })
                .PrimaryKey(t => t.idUser);
            
            AddColumn("dbo.items", "userInfo_idUser", c => c.Int());
            AddColumn("dbo.items", "idOwner", c => c.Int());
            AddColumn("dbo.equipements", "userInfo_idUser", c => c.Int());
            AddColumn("dbo.equipements", "idOwner", c => c.Int());
            DropForeignKey("dbo.items", "Id_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.equipements", "Id_Id", "dbo.AspNetUsers");
            DropIndex("dbo.items", new[] { "Id_Id" });
            DropIndex("dbo.equipements", new[] { "Id_Id" });
            DropColumn("dbo.items", "Id_Id");
            DropColumn("dbo.equipements", "Id_Id");
            CreateIndex("dbo.items", "userInfo_idUser");
            CreateIndex("dbo.equipements", "userInfo_idUser");
            AddForeignKey("dbo.items", "userInfo_idUser", "dbo.userInfo", "idUser");
            AddForeignKey("dbo.equipements", "userInfo_idUser", "dbo.userInfo", "idUser");
        }
    }
}
