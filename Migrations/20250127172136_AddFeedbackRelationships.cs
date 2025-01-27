using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tenant_issue_tracker.Migrations
{
    /// <inheritdoc />
    public partial class AddFeedbackRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApartmentNumber",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Feedback",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ApartmentNumber",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "TenantName",
                table: "Feedbacks");

            migrationBuilder.RenameColumn(
                name: "TenantName",
                table: "Issues",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Feedbacks",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Feedbacks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                table: "Feedbacks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_TenantId",
                table: "Feedbacks",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_AspNetUsers_TenantId",
                table: "Feedbacks",
                column: "TenantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_AspNetUsers_TenantId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_TenantId",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Feedbacks");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Issues",
                newName: "TenantName");

            migrationBuilder.AddColumn<string>(
                name: "ApartmentNumber",
                table: "Issues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Feedback",
                table: "Issues",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApartmentNumber",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Feedbacks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TenantName",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
