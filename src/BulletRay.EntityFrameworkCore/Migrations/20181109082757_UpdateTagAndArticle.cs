using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BulletRay.Migrations
{
    public partial class UpdateTagAndArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tag",
                table: "Articles",
                newName: "TagStr");

            migrationBuilder.AddColumn<long>(
                name: "TagNum",
                table: "Tags",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TagNum",
                table: "Articles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TagNum",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "TagNum",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "TagStr",
                table: "Articles",
                newName: "Tag");
        }
    }
}
