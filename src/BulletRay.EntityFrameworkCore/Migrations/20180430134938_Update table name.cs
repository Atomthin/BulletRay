using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BulletRay.Migrations
{
    public partial class Updatetablename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PostCategorys",
                table: "PostCategorys");

            migrationBuilder.RenameTable(
                name: "PostCategorys",
                newName: "ArticleCategorys");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleCategorys",
                table: "ArticleCategorys",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleCategorys",
                table: "ArticleCategorys");

            migrationBuilder.RenameTable(
                name: "ArticleCategorys",
                newName: "PostCategorys");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostCategorys",
                table: "PostCategorys",
                column: "Id");
        }
    }
}
