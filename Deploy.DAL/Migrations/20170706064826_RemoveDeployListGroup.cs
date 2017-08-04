using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Deploy.DAL.Migrations
{
    public partial class RemoveDeployListGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeployListGroup");

            migrationBuilder.AddColumn<string>(
                name: "DeployType",
                table: "DeployList",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeployType",
                table: "DeployList");

            migrationBuilder.CreateTable(
                name: "DeployListGroup",
                columns: table => new
                {
                    DeployListGroupID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeployListGroupName = table.Column<string>(nullable: true),
                    DeployListID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeployListGroup", x => x.DeployListGroupID);
                    table.ForeignKey(
                        name: "FK_DeployListGroup_DeployList_DeployListID",
                        column: x => x.DeployListID,
                        principalTable: "DeployList",
                        principalColumn: "DeployListID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeployListGroup_DeployListID",
                table: "DeployListGroup",
                column: "DeployListID");
        }
    }
}
