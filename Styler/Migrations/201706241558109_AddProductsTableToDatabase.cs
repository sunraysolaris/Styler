namespace Styler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductsTableToDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        ProductNameEng = c.String(),
                        ProductNameRus = c.String(),
                        Description = c.String(),
                        DescriptionEng = c.String(),
                        DescriptionRus = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PhotoUrl = c.String(),
                    })
                .PrimaryKey(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
        }
    }
}
