using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacilityHub.Migrations
{
    /// <inheritdoc />
    public partial class AddIssuesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "FacilityId",
                table: "Users",
                type: "char(40)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "char(40)",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Users",
                type: "char(40)",
                maxLength: 40,
                nullable: false,
                defaultValueSql: "(UUID())",
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "char(40)",
                oldMaxLength: 40,
                oldDefaultValueSql: "(UUID())")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "Facilities",
                type: "char(40)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "char(40)",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Facilities",
                type: "char(40)",
                maxLength: 40,
                nullable: false,
                defaultValueSql: "(UUID())",
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "char(40)",
                oldMaxLength: 40,
                oldDefaultValueSql: "(UUID())")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(40)", maxLength: 40, nullable: false, defaultValueSql: "(UUID())", collation: "ascii_general_ci"),
                    FiledById = table.Column<Guid>(type: "char(40)", nullable: false, collation: "ascii_general_ci"),
                    FiledAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    FacilityId = table.Column<Guid>(type: "char(40)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issues_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Issues_Users_FiledById",
                        column: x => x.FiledById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_FacilityId",
                table: "Issues",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_FiledById",
                table: "Issues",
                column: "FiledById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.AlterColumn<string>(
                name: "FacilityId",
                table: "Users",
                type: "char(40)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(40)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Users",
                type: "char(40)",
                maxLength: 40,
                nullable: false,
                defaultValueSql: "(UUID())",
                oldClrType: typeof(Guid),
                oldType: "char(40)",
                oldMaxLength: 40,
                oldDefaultValueSql: "(UUID())")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Facilities",
                type: "char(40)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(40)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Facilities",
                type: "char(40)",
                maxLength: 40,
                nullable: false,
                defaultValueSql: "(UUID())",
                oldClrType: typeof(Guid),
                oldType: "char(40)",
                oldMaxLength: 40,
                oldDefaultValueSql: "(UUID())")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");
        }
    }
}
