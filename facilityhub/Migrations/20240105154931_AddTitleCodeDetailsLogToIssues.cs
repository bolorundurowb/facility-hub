using System;
using System.Collections.Generic;
using FacilityHub.Models.Data;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacilityHub.Migrations
{
    /// <inheritdoc />
    public partial class AddTitleCodeDetailsLogToIssues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Issues",
                type: "character varying(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Issues",
                type: "character varying(4096)",
                maxLength: 4096,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<List<IssueLogEntry>>(
                name: "Log",
                table: "Issues",
                type: "jsonb",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Issues",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Issues",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "IssueId",
                table: "Document",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Document_IssueId",
                table: "Document",
                column: "IssueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Issues_IssueId",
                table: "Document",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_Issues_IssueId",
                table: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Document_IssueId",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Log",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "IssueId",
                table: "Document");
        }
    }
}
