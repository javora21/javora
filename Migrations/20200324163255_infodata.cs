using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace javora.Migrations
{
    public partial class infodata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InfoDatas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChangeData = table.Column<DateTime>(nullable: false),
                    HtmlData = table.Column<string>(nullable: true),
                    InfoType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoDatas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InfoDatas");
        }
    }
}
