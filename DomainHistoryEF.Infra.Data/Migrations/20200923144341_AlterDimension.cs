using Microsoft.EntityFrameworkCore.Migrations;

namespace DomainHistoryEF.Infra.Data.Migrations
{
    public partial class AlterDimension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Dimension_Width",
                table: "Product",
                newName: "Width");

            migrationBuilder.RenameColumn(
                name: "Dimension_Height",
                table: "Product",
                newName: "Height");

            migrationBuilder.RenameColumn(
                name: "Dimension_Depth",
                table: "Product",
                newName: "Depth");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Width",
                table: "Product",
                newName: "Dimension_Width");

            migrationBuilder.RenameColumn(
                name: "Height",
                table: "Product",
                newName: "Dimension_Height");

            migrationBuilder.RenameColumn(
                name: "Depth",
                table: "Product",
                newName: "Dimension_Depth");
        }
    }
}
