using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Deploy.Migrations
{
    public partial class deployresult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeployResult",
                table: "DeployTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeployState",
                table: "DeployTypes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeployResult",
                table: "DeployTypes");

            migrationBuilder.DropColumn(
                name: "DeployState",
                table: "DeployTypes");
        }
    }
}
