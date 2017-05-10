using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Deploy.DAL.Migrations
{
    public partial class files : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeployFile",
                table: "DeployTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParamsFile",
                table: "DeployTypes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeployFile",
                table: "DeployTypes");

            migrationBuilder.DropColumn(
                name: "ParamsFile",
                table: "DeployTypes");
        }
    }
}
