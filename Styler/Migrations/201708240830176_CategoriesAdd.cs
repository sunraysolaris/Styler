namespace Styler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoriesAdd : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProductCategoryProducts", newName: "ProductProductCategories");
            DropPrimaryKey("dbo.ProductProductCategories");
            AddPrimaryKey("dbo.ProductProductCategories", new[] { "Product_ProductID", "ProductCategory_CategoryID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ProductProductCategories");
            AddPrimaryKey("dbo.ProductProductCategories", new[] { "ProductCategory_CategoryID", "Product_ProductID" });
            RenameTable(name: "dbo.ProductProductCategories", newName: "ProductCategoryProducts");
        }
    }
}
