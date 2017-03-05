using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Deploy.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tennants",
                columns: table => new
                {
                    TennantID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmailAddress = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    TennantName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tennants", x => x.TennantID);
                });

            migrationBuilder.CreateTable(
                name: "DeployTypes",
                columns: table => new
                {
                    DeployTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeployName = table.Column<string>(nullable: true),
                    TennantID = table.Column<int>(nullable: false),
                    TennantParamID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeployTypes", x => x.DeployTypeID);
                    table.ForeignKey(
                        name: "FK_DeployTypes_Tennants_TennantID",
                        column: x => x.TennantID,
                        principalTable: "Tennants",
                        principalColumn: "TennantID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TennantParams",
                columns: table => new
                {
                    TennantParamID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeployParamID = table.Column<int>(nullable: false),
                    DeployTypeID = table.Column<int>(nullable: false),
                    ParamName = table.Column<string>(nullable: true),
                    ParamType = table.Column<string>(nullable: true),
                    ParamValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TennantParams", x => x.TennantParamID);
                    table.ForeignKey(
                        name: "FK_TennantParams_DeployTypes_DeployTypeID",
                        column: x => x.DeployTypeID,
                        principalTable: "DeployTypes",
                        principalColumn: "DeployTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeployParms",
                columns: table => new
                {
                    DeployParamID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParameterDeployType = table.Column<string>(nullable: true),
                    ParameterName = table.Column<string>(nullable: true),
                    ParameterType = table.Column<string>(nullable: true),
                    TennantParamID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeployParms", x => x.DeployParamID);
                    table.ForeignKey(
                        name: "FK_DeployParms_TennantParams_TennantParamID",
                        column: x => x.TennantParamID,
                        principalTable: "TennantParams",
                        principalColumn: "TennantParamID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeployParms_TennantParamID",
                table: "DeployParms",
                column: "TennantParamID");

            migrationBuilder.CreateIndex(
                name: "IX_DeployTypes_TennantID",
                table: "DeployTypes",
                column: "TennantID");

            migrationBuilder.CreateIndex(
                name: "IX_TennantParams_DeployTypeID",
                table: "TennantParams",
                column: "DeployTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeployParms");

            migrationBuilder.DropTable(
                name: "TennantParams");

            migrationBuilder.DropTable(
                name: "DeployTypes");

            migrationBuilder.DropTable(
                name: "Tennants");
        }
    }
}
