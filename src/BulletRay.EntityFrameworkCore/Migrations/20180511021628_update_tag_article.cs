using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BulletRay.Migrations
{
    public partial class update_tag_article : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "Tags");

            migrationBuilder.AlterColumn<string>(
                name: "Tag",
                table: "Articles",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "Tags",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Tags",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Tags",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Tags",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Tags",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "Tags",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Tag",
                table: "Articles",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
