using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacilityHub.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTenancyDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenant_Users_UserId",
                table: "Tenant");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Tenant",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Tenant_Users_UserId",
                table: "Tenant",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenant_Users_UserId",
                table: "Tenant");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Tenant",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenant_Users_UserId",
                table: "Tenant",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
