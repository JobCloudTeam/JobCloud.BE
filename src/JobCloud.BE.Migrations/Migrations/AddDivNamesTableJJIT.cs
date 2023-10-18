using FluentMigrator;
using JobCloud.BE.Shared.Enums.JustJoinIt;

namespace JobCloud.BE.Migrations.Migrations
{
    [Tags("JobCloud")]
    [Migration(202310171750)]
    public class AddDivNamesTableJJIT : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            if (!Schema.Schema("jjit").Table("divNames").Exists())
            {
                Create
                    .Table("divNames").InSchema("jjit")
                    .WithColumn("id").AsInt32().NotNullable().Identity().PrimaryKey()
                    .WithColumn("div").AsString().NotNullable()
                    .WithColumn("name").AsString().Nullable();

                Enum.GetNames<Div>().ToList().ForEach(x =>
                    Insert
                        .IntoTable("divNames")
                        .InSchema("jjit")
                        .Row(new { div = x }));
            }
        }
    }
}
