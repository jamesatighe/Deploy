using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Deploy.DAL.Migrations
{
    public partial class resourcegroupname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeployFile",
                table: "DeployTypes");

            migrationBuilder.RenameColumn(
                name: "ParamsFile",
                table: "DeployTypes",
                newName: "ResourceGroupName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResourceGroupName",
                table: "DeployTypes",
                newName: "ParamsFile");

            migrationBuilder.AddColumn<string>(
                name: "DeployFile",
                table: "DeployTypes",
                nullable: true);
        }
    }
}
