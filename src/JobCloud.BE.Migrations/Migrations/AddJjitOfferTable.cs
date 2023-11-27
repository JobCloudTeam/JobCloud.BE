using FluentMigrator;

namespace JobCloud.BE.Migrations.Migrations
{
    [Tags("JobCloud")]
    [Migration(202311272145)]
    public class AddJjitOfferTable : Migration
    {
        public override void Down()
        {
            Delete.Table("offers").InSchema("jjit");
        }

        public override void Up()
        {
            if (!Schema.Schema("jjit").Table("offers").Exists())
            {
                Create.Table("offers")
                    .InSchema("jjit")
                    .WithColumn("id").AsInt32().NotNullable().Unique().Identity().PrimaryKey()
                    .WithColumn("name").AsString().NotNullable()
                    .WithColumn("company").AsString().NotNullable()
                    .WithColumn("salary_uop").AsString().Nullable()
                    .WithColumn("salary_b2b").AsString().Nullable()
                    .WithColumn("base_tech").AsString().NotNullable()
                    .WithColumn("techstack").AsString().Nullable()
                    .WithColumn("status").AsBoolean().NotNullable();
            }
        }
    }
}
