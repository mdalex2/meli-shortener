using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace meli.Migrations
{
    public partial class DeleteListURLModelToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListUrlViewClass");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ListUrlViewClass",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaExpira = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Habilitado = table.Column<bool>(type: "bit", nullable: false),
                    NumVisitas = table.Column<int>(type: "int", nullable: false),
                    UrlCorta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlLarga = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListUrlViewClass", x => x.ID);
                });
        }
    }
}
