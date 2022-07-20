using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyLeasing.Web.Migrations
{
    public partial class ImageId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Lessee");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Owners",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PhotoId",
                table: "Lessee",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Lessee");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Owners",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Lessee",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
