namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class about_entity_update2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Abouts", "AboutImage1", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Abouts", "AboutImage2", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Abouts", "AboutImage2", c => c.String());
            AlterColumn("dbo.Abouts", "AboutImage1", c => c.String());
        }
    }
}
