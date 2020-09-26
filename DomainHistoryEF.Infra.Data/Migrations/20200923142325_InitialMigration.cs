using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DomainHistoryEF.Infra.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DOMAIN_HISTORY",
                columns: table => new
                {
                    DOMAIN_HISTORY_ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TABLE_NM = table.Column<string>(maxLength: 500, nullable: false),
                    COLUMN_NM = table.Column<string>(maxLength: 500, nullable: false),
                    COLUMN_SOURCE_ID = table.Column<string>(maxLength: 500, nullable: false),
                    COLUMN_PREVIOUS_VL = table.Column<string>(maxLength: 500, nullable: false),
                    CREATED_BY_DT = table.Column<DateTime>(nullable: false),
                    CREATED_BY_DS = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOMAIN_HISTORY", x => x.DOMAIN_HISTORY_ID);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAtDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatededAtDate = table.Column<DateTime>(nullable: true),
                    UpdatedByUser = table.Column<string>(nullable: true),
                    Dimension_Height = table.Column<double>(nullable: true),
                    Dimension_Width = table.Column<double>(nullable: true),
                    Dimension_Depth = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DOMAIN_HISTORY");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
