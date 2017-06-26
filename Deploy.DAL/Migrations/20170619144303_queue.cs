using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Deploy.DAL.Migrations
{
    public partial class queue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ParamValue",
                table: "TennantParams",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Queue",
                columns: table => new
                {
                    QueueID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeployName = table.Column<string>(nullable: true),
                    DeployTypeID = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    TennantID = table.Column<int>(nullable: false),
                    TennantName = table.Column<string>(nullable: true),
                    accesstoken = table.Column<string>(nullable: true),
                    azuredeploy = table.Column<string>(nullable: true),
                    jsondeploy = table.Column<string>(nullable: true),
                    resource = table.Column<bool>(nullable: false),
                    resourcegroup = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    subscriptionID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Queue", x => x.QueueID);
                    table.ForeignKey(
                        name: "FK_Queue_DeployTypes_DeployTypeID",
                        column: x => x.DeployTypeID,
                        principalTable: "DeployTypes",
                        principalColumn: "DeployTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Queue_DeployTypeID",
                table: "Queue",
                column: "DeployTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Queue");

            migrationBuilder.AlterColumn<string>(
                name: "ParamValue",
                table: "TennantParams",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
