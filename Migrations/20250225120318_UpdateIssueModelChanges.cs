using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tenant_issue_tracker.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIssueModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResolutionComment",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ResolvedDate",
                table: "Issues");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResolutionComment",
                table: "Issues",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResolvedDate",
                table: "Issues",
                type: "datetime2",
                nullable: true);
        }
    }
}
