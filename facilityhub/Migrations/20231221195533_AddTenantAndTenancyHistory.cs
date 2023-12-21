using System;
using System.Collections.Generic;
using FacilityHub.Models.Data;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacilityHub.Migrations
{
    /// <inheritdoc />
    public partial class AddTenantAndTenancyHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Users_FiledById",
                table: "Issues");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Facilities",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Facilities",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    History = table.Column<List<TenancyHistory>>(type: "jsonb", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tenant_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tenant_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_TenantId",
                table: "Facilities",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_CreatedById",
                table: "Tenant",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_UserId",
                table: "Tenant",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_Tenant_TenantId",
                table: "Facilities",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Tenant_FiledById",
                table: "Issues",
                column: "FiledById",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_Tenant_TenantId",
                table: "Facilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Tenant_FiledById",
                table: "Issues");

            migrationBuilder.DropTable(
                name: "Tenant");

            migrationBuilder.DropIndex(
                name: "IX_Facilities_TenantId",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Facilities");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Facilities",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Users_FiledById",
                table: "Issues",
                column: "FiledById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
