using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Deploy.DAL.Migrations
{
    public partial class filenames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeployFile",
                table: "DeployChoices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParamsFile",
                table: "DeployChoices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeployFile",
                table: "DeployChoices");

            migrationBuilder.DropColumn(
                name: "ParamsFile",
                table: "DeployChoices");
        }
    }
}
