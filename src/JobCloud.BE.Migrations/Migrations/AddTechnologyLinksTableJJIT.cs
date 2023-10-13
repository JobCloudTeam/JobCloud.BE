using FluentMigrator;
using JobCloud.BE.Shared.Enums;

namespace JobCloud.BE.Migrations.Migrations
{
    [Tags("JobCloud")]
    [Migration(202310131755)]
    public class AddTechnologyLinksTableJJIT : Migration
    {
        public override void Down()
        {
            Delete.Table("tchlink").InSchema("jjit");
        }

        public override void Up()
        {
            if (!Schema.Schema("jjit").Table("tchlink").Exists())
            {
                Create
                    .Table("tchlink")
                    .InSchema("jjit")
                    .WithColumn("id").AsInt32().NotNullable().Identity().PrimaryKey()
                    .WithColumn("technology").AsString().NotNullable()
                    .WithColumn("link").AsString().Nullable();


                Enum.GetNames<Technology>().ToList().ForEach(x =>
                Insert
                    .IntoTable("tchlink")
                    .InSchema("jjit")
                    .Row(new { technology = x })
                );
            }
        }
    }
}
