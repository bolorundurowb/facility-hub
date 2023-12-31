using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacilityHub.Migrations
{
    /// <inheritdoc />
    public partial class AddInUserFacilityManyToMayRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_Users_OwnerId",
                table: "Facilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Facilities_FacilityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_FacilityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Facilities_OwnerId",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "FacilityId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Facilities");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "character varying(2048)",
                maxLength: 2048,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "Users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Facilities",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Facilities",
                type: "character varying(1024)",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "FacilityManagers",
                columns: table => new
                {
                    ManagedId = table.Column<Guid>(type: "uuid", nullable: false),
                    ManagersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityManagers", x => new { x.ManagedId, x.ManagersId });
                    table.ForeignKey(
                        name: "FK_FacilityManagers_Facilities_ManagedId",
                        column: x => x.ManagedId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacilityManagers_Users_ManagersId",
                        column: x => x.ManagersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacilityOwners",
                columns: table => new
                {
                    OwnedId = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityOwners", x => new { x.OwnedId, x.OwnersId });
                    table.ForeignKey(
                        name: "FK_FacilityOwners_Facilities_OwnedId",
                        column: x => x.OwnedId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacilityOwners_Users_OwnersId",
                        column: x => x.OwnersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacilityManagers_ManagersId",
                table: "FacilityManagers",
                column: "ManagersId");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityOwners_OwnersId",
                table: "FacilityOwners",
                column: "OwnersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacilityManagers");

            migrationBuilder.DropTable(
                name: "FacilityOwners");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(2048)",
                oldMaxLength: 2048);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.AddColumn<Guid>(
                name: "FacilityId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Facilities",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Facilities",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1024)",
                oldMaxLength: 1024);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Facilities",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_FacilityId",
                table: "Users",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_OwnerId",
                table: "Facilities",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_Users_OwnerId",
                table: "Facilities",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Facilities_FacilityId",
                table: "Users",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id");
        }
    }
}
