using FluentMigrator;

namespace JobCloud.BE.Migrations.Migrations
{
    [Tags("JobCloud")]
    [Migration(202310130030)]
    public class AddBasechemas : Migration
    {
        public override void Down()
        {
            Delete.Schema("jjit");
            Delete.Schema("nfj");
            Delete.Schema("jbc");
        }

        public override void Up()
        {
            if (!Schema.Schema("jjit").Exists())
                Create.Schema("jjit");
            if (!Schema.Schema("nfj").Exists())
                Create.Schema("nfj");
            if (!Schema.Schema("jbc").Exists())
                Create.Schema("jbc");
        }
    }
}
