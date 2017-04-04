using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Deploy.Migrations
{
    public partial class paramtooltip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParamToolTip",
                table: "TennantParams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParamToolTip",
                table: "DeployParms",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParamToolTip",
                table: "TennantParams");

            migrationBuilder.DropColumn(
                name: "ParamToolTip",
                table: "DeployParms");
        }
    }
}
