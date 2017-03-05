using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Deploy.Migrations
{
    public partial class removetennantparamIDfromdeploytype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TennantParamID",
                table: "DeployTypes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TennantParamID",
                table: "DeployTypes",
                nullable: false,
                defaultValue: 0);
        }
    }
}
