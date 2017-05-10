using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Deploy.DAL.Migrations
{
    public partial class DeployChoices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeployChoices",
                columns: table => new
                {
                    DeployChoicesID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BaseOption = table.Column<string>(nullable: true),
                    Datadisk = table.Column<bool>(nullable: false),
                    Domain = table.Column<bool>(nullable: false),
                    OMS = table.Column<bool>(nullable: false),
                    SolutionSize = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeployChoices", x => x.DeployChoicesID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeployChoices");
        }
    }
}
