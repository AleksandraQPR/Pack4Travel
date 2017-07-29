namespace Pack4Travel.Migrations.ApplicationDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoveToADC : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.equipements",
                c => new
                    {
                        idEquipement = c.Int(nullable: false, identity: true),
                        equipementName = c.String(nullable: false, maxLength: 30),
                        idGroup = c.Int(nullable: false),
                        idOwner = c.Int(),
                        privateStatus = c.String(nullable: false, maxLength: 1),
                        five_stars = c.Int(nullable: false),
                        four_stars = c.Int(nullable: false),
                        three_stars = c.Int(nullable: false),
                        two_stars = c.Int(nullable: false),
                        one_star = c.Int(nullable: false),
                        userInfo_idUser = c.Int(),
                    })
                .PrimaryKey(t => t.idEquipement)
                .ForeignKey("dbo.userInfo", t => t.userInfo_idUser)
                .Index(t => t.userInfo_idUser);
            
            CreateTable(
                "dbo.equipementTags",
                c => new
                    {
                        idEquipement = c.Int(nullable: false),
                        idEquipementGroup = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.idEquipement, t.idEquipementGroup })
                .ForeignKey("dbo.equipements", t => t.idEquipement, cascadeDelete: true)
                .Index(t => t.idEquipement);
            
            CreateTable(
                "dbo.items",
                c => new
                    {
                        idItem = c.Int(nullable: false, identity: true),
                        itemName = c.String(nullable: false, maxLength: 30),
                        idGroup = c.Int(nullable: false),
                        idOwner = c.Int(),
                        privateStatus = c.String(nullable: false, maxLength: 1),
                        itemGroup_idItemGroup = c.Int(),
                        userInfo_idUser = c.Int(),
                    })
                .PrimaryKey(t => t.idItem)
                .ForeignKey("dbo.itemGroup", t => t.itemGroup_idItemGroup)
                .ForeignKey("dbo.userInfo", t => t.userInfo_idUser)
                .Index(t => t.itemGroup_idItemGroup)
                .Index(t => t.userInfo_idUser);
            
            CreateTable(
                "dbo.itemGroup",
                c => new
                    {
                        idItemGroup = c.Int(nullable: false, identity: true),
                        groupName = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.idItemGroup);
            
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
            
            CreateTable(
                "dbo.itemsequipements",
                c => new
                    {
                        items_idItem = c.Int(nullable: false),
                        equipements_idEquipement = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.items_idItem, t.equipements_idEquipement })
                .ForeignKey("dbo.items", t => t.items_idItem, cascadeDelete: true)
                .ForeignKey("dbo.equipements", t => t.equipements_idEquipement, cascadeDelete: true)
                .Index(t => t.items_idItem)
                .Index(t => t.equipements_idEquipement);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.items", "userInfo_idUser", "dbo.userInfo");
            DropForeignKey("dbo.equipements", "userInfo_idUser", "dbo.userInfo");
            DropForeignKey("dbo.items", "itemGroup_idItemGroup", "dbo.itemGroup");
            DropForeignKey("dbo.itemsequipements", "equipements_idEquipement", "dbo.equipements");
            DropForeignKey("dbo.itemsequipements", "items_idItem", "dbo.items");
            DropForeignKey("dbo.equipementTags", "idEquipement", "dbo.equipements");
            DropIndex("dbo.itemsequipements", new[] { "equipements_idEquipement" });
            DropIndex("dbo.itemsequipements", new[] { "items_idItem" });
            DropIndex("dbo.items", new[] { "userInfo_idUser" });
            DropIndex("dbo.items", new[] { "itemGroup_idItemGroup" });
            DropIndex("dbo.equipementTags", new[] { "idEquipement" });
            DropIndex("dbo.equipements", new[] { "userInfo_idUser" });
            DropTable("dbo.itemsequipements");
            DropTable("dbo.userInfo");
            DropTable("dbo.itemGroup");
            DropTable("dbo.items");
            DropTable("dbo.equipementTags");
            DropTable("dbo.equipements");
        }
    }
}
