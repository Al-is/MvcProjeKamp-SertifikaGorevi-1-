﻿namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_admin_class_update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admins", "AdminUserName", c => c.String(maxLength: 200));
            AlterColumn("dbo.Admins", "AdminPassword", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Admins", "AdminPassword", c => c.String(maxLength: 50));
            AlterColumn("dbo.Admins", "AdminUserName", c => c.String(maxLength: 50));
        }
    }
}
