using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacilityHub.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTheIssueDataPoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Facilities_FacilityId",
                table: "Issues");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Issues",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "Details",
                table: "Issues",
                newName: "Description");

            migrationBuilder.AlterColumn<Guid>(
                name: "FacilityId",
                table: "Issues",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "OccurredAt",
                table: "Issues",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "RemedialAction",
                table: "Issues",
                type: "character varying(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Facilities_FacilityId",
                table: "Issues",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Facilities_FacilityId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "OccurredAt",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "RemedialAction",
                table: "Issues");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Issues",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Issues",
                newName: "Details");

            migrationBuilder.AlterColumn<Guid>(
                name: "FacilityId",
                table: "Issues",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Facilities_FacilityId",
                table: "Issues",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id");
        }
    }
}
