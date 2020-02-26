namespace NearDuplicatesAnalysis.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Baseline : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Listing",
                c => new
                    {
                        id = c.Long(nullable: false),
                        seller_id = c.Int(nullable: false),
                        seller_name = c.String(nullable: false, maxLength: 15),
                        category_id = c.Int(nullable: false),
                        mcat_path = c.String(nullable: false, maxLength: 256),
                        title = c.String(nullable: false, maxLength: 80),
                        buy_now_price = c.Decimal(precision: 18, scale: 2),
                        description = c.String(nullable: false, maxLength: 2050),
                        photo_id = c.Int(),
                        likely_duplicate_id_by_title = c.Long(),
                        likely_duplicate_id_by_description = c.Long(),
                        similarity_title = c.Double(nullable: false),
                        similarity_description = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.id, clustered: false)
                .Index(t => t.seller_id)
                .Index(t => t.mcat_path, clustered: true, name: "C_mcat_path");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Listing", "C_mcat_path");
            DropIndex("dbo.Listing", new[] { "seller_id" });
            DropTable("dbo.Listing");
        }
    }
}
