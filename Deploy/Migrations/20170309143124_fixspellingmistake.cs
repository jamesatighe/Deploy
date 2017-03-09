using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Deploy.Migrations
{
    public partial class fixspellingmistake : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AzureSubcriptionID",
                table: "Tennants",
                newName: "AzureSubscriptionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AzureSubscriptionID",
                table: "Tennants",
                newName: "AzureSubcriptionID");
        }
    }
}
