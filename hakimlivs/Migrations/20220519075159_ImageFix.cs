using Microsoft.EntityFrameworkCore.Migrations;

namespace hakimlivs.Migrations
{
    public partial class ImageFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                schema: "Identity",
                table: "Product");

            migrationBuilder.AddColumn<int>(
                name: "ImageID",
                schema: "Identity",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Image",
                schema: "Identity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ImageID",
                schema: "Identity",
                table: "Product",
                column: "ImageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Image_ImageID",
                schema: "Identity",
                table: "Product",
                column: "ImageID",
                principalSchema: "Identity",
                principalTable: "Image",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Image_ImageID",
                schema: "Identity",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Image",
                schema: "Identity");

            migrationBuilder.DropIndex(
                name: "IX_Product_ImageID",
                schema: "Identity",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ImageID",
                schema: "Identity",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                schema: "Identity",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
