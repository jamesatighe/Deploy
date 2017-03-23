using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Deploy.Migrations
{
    public partial class ResourceLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Tennants",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "ResourceGroupLocation",
                table: "Tennants",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResourceGroupLocation",
                table: "Tennants");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Tennants",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
