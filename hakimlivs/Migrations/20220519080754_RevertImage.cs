using Microsoft.EntityFrameworkCore.Migrations;

namespace hakimlivs.Migrations
{
    public partial class RevertImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Images_ImageID",
                schema: "Identity",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Images",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "Images",
                schema: "Identity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ImageID",
                schema: "Identity",
                table: "Product",
                column: "ImageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Images_ImageID",
                schema: "Identity",
                table: "Product",
                column: "ImageID",
                principalSchema: "Identity",
                principalTable: "Images",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
