using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Deploy.DAL.Migrations
{
    public partial class solution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OMS",
                table: "DeployChoices",
                newName: "Solution");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Solution",
                table: "DeployChoices",
                newName: "OMS");
        }
    }
}
