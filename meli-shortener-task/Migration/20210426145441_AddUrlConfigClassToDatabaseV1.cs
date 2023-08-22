using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace meli.Migrations
{
    public partial class AddUrlConfigClassToDatabaseV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UrlConfig",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrlLarga = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlCorta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaExpira = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Habilitado = table.Column<bool>(type: "bit", nullable: false),
                    NumVisitas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlConfig", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrlConfig");
        }
    }
}
