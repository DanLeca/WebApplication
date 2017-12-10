using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ProjectPlan.Data.Migrations
{
    public partial class CommentSystsem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Group_GroupId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_GroupId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Comment");

            migrationBuilder.AddColumn<int>(
                name: "MyGroupId",
                table: "Comment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_MyGroupId",
                table: "Comment",
                column: "MyGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Group_MyGroupId",
                table: "Comment",
                column: "MyGroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Group_MyGroupId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_MyGroupId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "MyGroupId",
                table: "Comment");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Comment",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_GroupId",
                table: "Comment",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Group_GroupId",
                table: "Comment",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
