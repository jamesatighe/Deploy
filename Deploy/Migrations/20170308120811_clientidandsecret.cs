using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Deploy.Migrations
{
    public partial class clientidandsecret : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ResourceGroupName",
                table: "Tennants",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "AzureTennantID",
                table: "Tennants",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "AzureClientID",
                table: "Tennants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AzureClientSecret",
                table: "Tennants",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AzureClientID",
                table: "Tennants");

            migrationBuilder.DropColumn(
                name: "AzureClientSecret",
                table: "Tennants");

            migrationBuilder.AlterColumn<string>(
                name: "ResourceGroupName",
                table: "Tennants",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AzureTennantID",
                table: "Tennants",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
