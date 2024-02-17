using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacilityHub.Migrations
{
    /// <inheritdoc />
    public partial class FixTenantsTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Tenant_TenantId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_Tenant_TenantId",
                table: "Facilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Tenant_FiledById",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Tenant_Users_CreatedById",
                table: "Tenant");

            migrationBuilder.DropForeignKey(
                name: "FK_Tenant_Users_UserId",
                table: "Tenant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tenant",
                table: "Tenant");

            migrationBuilder.RenameTable(
                name: "Tenant",
                newName: "Tenants");

            migrationBuilder.RenameIndex(
                name: "IX_Tenant_UserId",
                table: "Tenants",
                newName: "IX_Tenants_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tenant_CreatedById",
                table: "Tenants",
                newName: "IX_Tenants_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tenants",
                table: "Tenants",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Tenants_TenantId",
                table: "Documents",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_Tenants_TenantId",
                table: "Facilities",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Tenants_FiledById",
                table: "Issues",
                column: "FiledById",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_Users_CreatedById",
                table: "Tenants",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_Users_UserId",
                table: "Tenants",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Tenants_TenantId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_Tenants_TenantId",
                table: "Facilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Tenants_FiledById",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_Users_CreatedById",
                table: "Tenants");

            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_Users_UserId",
                table: "Tenants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tenants",
                table: "Tenants");

            migrationBuilder.RenameTable(
                name: "Tenants",
                newName: "Tenant");

            migrationBuilder.RenameIndex(
                name: "IX_Tenants_UserId",
                table: "Tenant",
                newName: "IX_Tenant_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tenants_CreatedById",
                table: "Tenant",
                newName: "IX_Tenant_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tenant",
                table: "Tenant",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Tenant_TenantId",
                table: "Documents",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Tenant_Users_CreatedById",
                table: "Tenant",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenant_Users_UserId",
                table: "Tenant",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
