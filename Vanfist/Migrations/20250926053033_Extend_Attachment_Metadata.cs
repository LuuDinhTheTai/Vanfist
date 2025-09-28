using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vanfist.Migrations
{
    /// <inheritdoc />
    public partial class Extend_Attachment_Metadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AltText",
                table: "Attachments",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Attachments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Attachments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Attachments",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OriginalName",
                table: "Attachments",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Size",
                table: "Attachments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Attachments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Attachments",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AltText",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "OriginalName",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Attachments");
        }
    }
}
