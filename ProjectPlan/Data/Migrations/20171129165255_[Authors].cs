using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ProjectPlan.Data.Migrations
{
    public partial class Authors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Group",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Group_ApplicationUserId",
                table: "Group",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_AspNetUsers_ApplicationUserId",
                table: "Group",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_AspNetUsers_ApplicationUserId",
                table: "Group");

            migrationBuilder.DropIndex(
                name: "IX_Group_ApplicationUserId",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Group");
        }
    }
}
