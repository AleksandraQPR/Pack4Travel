namespace Pack4Travel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.equipements",
                c => new
                    {
                        idEquipement = c.Int(nullable: false, identity: true),
                        equipementName = c.String(nullable: false, maxLength: 30, unicode: false),
                        idGroup = c.Int(nullable: false),
                        idOwner = c.Int(),
                        privateStatus = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        five_stars = c.Int(nullable: false),
                        four_stars = c.Int(nullable: false),
                        three_stars = c.Int(nullable: false),
                        two_stars = c.Int(nullable: false),
                        one_star = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idEquipement)
                .ForeignKey("dbo.userInfo", t => t.idOwner)
                .Index(t => t.idOwner);
            
            CreateTable(
                "dbo.equipementTags",
                c => new
                    {
                        idEquipement = c.Int(nullable: false),
                        idEquipementGroup = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.idEquipement, t.idEquipementGroup })
                .ForeignKey("dbo.equipements", t => t.idEquipement)
                .Index(t => t.idEquipement);
            
            CreateTable(
                "dbo.items",
                c => new
                    {
                        idItem = c.Int(nullable: false, identity: true),
                        itemName = c.String(nullable: false, maxLength: 30, unicode: false),
                        idGroup = c.Int(nullable: false),
                        idOwner = c.Int(),
                        privateStatus = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.idItem)
                .ForeignKey("dbo.itemGroup", t => t.idGroup)
                .ForeignKey("dbo.userInfo", t => t.idOwner)
                .Index(t => t.idGroup)
                .Index(t => t.idOwner);
            
            CreateTable(
                "dbo.itemGroup",
                c => new
                    {
                        idItemGroup = c.Int(nullable: false, identity: true),
                        groupName = c.String(nullable: false, maxLength: 30, unicode: false),
                    })
                .PrimaryKey(t => t.idItemGroup);
            
            CreateTable(
                "dbo.userInfo",
                c => new
                    {
                        idUser = c.Int(nullable: false, identity: true),
                        userName = c.String(nullable: false, maxLength: 20, unicode: false),
                        mail = c.String(nullable: false, maxLength: 40, unicode: false),
                        password = c.String(nullable: false, maxLength: 30, unicode: false),
                        activeStatus = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.idUser);
            
            CreateTable(
                "dbo.equipementIems",
                c => new
                    {
                        idEquipement = c.Int(nullable: false),
                        idItem = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.idEquipement, t.idItem })
                .ForeignKey("dbo.equipements", t => t.idEquipement, cascadeDelete: true)
                .ForeignKey("dbo.items", t => t.idItem, cascadeDelete: true)
                .Index(t => t.idEquipement)
                .Index(t => t.idItem);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.equipementIems", "idItem", "dbo.items");
            DropForeignKey("dbo.equipementIems", "idEquipement", "dbo.equipements");
            DropForeignKey("dbo.items", "idOwner", "dbo.userInfo");
            DropForeignKey("dbo.equipements", "idOwner", "dbo.userInfo");
            DropForeignKey("dbo.items", "idGroup", "dbo.itemGroup");
            DropForeignKey("dbo.equipementTags", "idEquipement", "dbo.equipements");
            DropIndex("dbo.equipementIems", new[] { "idItem" });
            DropIndex("dbo.equipementIems", new[] { "idEquipement" });
            DropIndex("dbo.items", new[] { "idOwner" });
            DropIndex("dbo.items", new[] { "idGroup" });
            DropIndex("dbo.equipementTags", new[] { "idEquipement" });
            DropIndex("dbo.equipements", new[] { "idOwner" });
            DropTable("dbo.equipementIems");
            DropTable("dbo.userInfo");
            DropTable("dbo.itemGroup");
            DropTable("dbo.items");
            DropTable("dbo.equipementTags");
            DropTable("dbo.equipements");
        }
    }
}
