using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BulletRay.Migrations
{
    public partial class addmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "PostName",
                table: "Posts",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "PostContent",
                table: "Posts",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "PostCategorys",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CategoryDesc",
                table: "PostCategorys",
                newName: "Desc");

            migrationBuilder.AddColumn<long>(
                name: "ArticleId",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoverImgUrl",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReadCount",
                table: "Posts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnLike",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOpenShown",
                table: "PostCategorys",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CoverImgUrl",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ReadCount",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "UnLike",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsOpenShown",
                table: "PostCategorys");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Posts",
                newName: "PostName");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Posts",
                newName: "PostContent");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "PostCategorys",
                newName: "CategoryName");

            migrationBuilder.RenameColumn(
                name: "Desc",
                table: "PostCategorys",
                newName: "CategoryDesc");

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PostId",
                table: "Comments",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
