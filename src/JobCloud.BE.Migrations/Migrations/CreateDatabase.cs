using FluentMigrator;

namespace JobCloud.BE.Migrations.Migrations
{
    [Tags("Server")]
    [Migration(202310130000, TransactionBehavior.None)]
    public class CreateDatabase : Migration
    {
        public override void Down()
        {
            Execute.Sql(SQL.Queries.DropDatabase);
        }

        public override void Up()
        {
            Execute.Sql(SQL.Queries.CreateDatabase);
        }
    }
}
