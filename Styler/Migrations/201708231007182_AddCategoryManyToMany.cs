namespace Styler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryManyToMany : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        CategoryNameEng = c.String(),
                        CategoryNameRus = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.ProductCategoryProducts",
                c => new
                    {
                        ProductCategory_CategoryID = c.Int(nullable: false),
                        Product_ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductCategory_CategoryID, t.Product_ProductID })
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategory_CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_ProductID, cascadeDelete: true)
                .Index(t => t.ProductCategory_CategoryID)
                .Index(t => t.Product_ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductCategoryProducts", "Product_ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductCategoryProducts", "ProductCategory_CategoryID", "dbo.ProductCategories");
            DropIndex("dbo.ProductCategoryProducts", new[] { "Product_ProductID" });
            DropIndex("dbo.ProductCategoryProducts", new[] { "ProductCategory_CategoryID" });
            DropTable("dbo.ProductCategoryProducts");
            DropTable("dbo.ProductCategories");
        }
    }
}
