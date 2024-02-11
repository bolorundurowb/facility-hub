using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacilityHub.Migrations
{
    /// <inheritdoc />
    public partial class FixDocumentsTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_Facilities_FacilityId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_Issues_IssueId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_Tenant_TenantId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_Users_CreatedById",
                table: "Document");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Document",
                table: "Document");

            migrationBuilder.RenameTable(
                name: "Document",
                newName: "Documents");

            migrationBuilder.RenameIndex(
                name: "IX_Document_TenantId",
                table: "Documents",
                newName: "IX_Documents_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Document_IssueId",
                table: "Documents",
                newName: "IX_Documents_IssueId");

            migrationBuilder.RenameIndex(
                name: "IX_Document_FacilityId",
                table: "Documents",
                newName: "IX_Documents_FacilityId");

            migrationBuilder.RenameIndex(
                name: "IX_Document_CreatedById",
                table: "Documents",
                newName: "IX_Documents_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documents",
                table: "Documents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Facilities_FacilityId",
                table: "Documents",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Issues_IssueId",
                table: "Documents",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Tenant_TenantId",
                table: "Documents",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Users_CreatedById",
                table: "Documents",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Facilities_FacilityId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Issues_IssueId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Tenant_TenantId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Users_CreatedById",
                table: "Documents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Documents",
                table: "Documents");

            migrationBuilder.RenameTable(
                name: "Documents",
                newName: "Document");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_TenantId",
                table: "Document",
                newName: "IX_Document_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_IssueId",
                table: "Document",
                newName: "IX_Document_IssueId");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_FacilityId",
                table: "Document",
                newName: "IX_Document_FacilityId");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_CreatedById",
                table: "Document",
                newName: "IX_Document_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Document",
                table: "Document",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Facilities_FacilityId",
                table: "Document",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Issues_IssueId",
                table: "Document",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Tenant_TenantId",
                table: "Document",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Users_CreatedById",
                table: "Document",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
