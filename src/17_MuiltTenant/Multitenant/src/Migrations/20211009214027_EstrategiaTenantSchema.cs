using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EFCore.Multitenant.Migrations
{
    public partial class EstrategiaTenantSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "People",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "People",
                columns: new[] { "Id", "Name", "TenantId" },
                values: new object[,]
                {
                    { 1, "Person 1", "Tenant-1" },
                    { 2, "Person 2", "Tenant-2" },
                    { 3, "Person 3", "Tenant-3" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Products",
                columns: new[] { "Id", "Description", "TenantId" },
                values: new object[,]
                {
                    { 1, "Description 1", "Tenant-1" },
                    { 2, "Description 2", "Tenant-2" },
                    { 3, "Description 3", "Tenant-3" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "dbo");
        }
    }
}
