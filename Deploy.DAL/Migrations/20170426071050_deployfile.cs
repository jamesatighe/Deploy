using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Deploy.DAL.Migrations
{
    public partial class deployfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeplpoyFile",
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
                name: "DeplpoyFile",
                table: "DeployTypes");

            migrationBuilder.DropColumn(
                name: "ParamsFile",
                table: "DeployTypes");
        }
    }
}
