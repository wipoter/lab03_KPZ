namespace CD_First.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "Educ", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "Educ");
        }
    }
}
