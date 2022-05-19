using Microsoft.EntityFrameworkCore.Migrations;

namespace hakimlivs.Migrations
{
    public partial class CorrectImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Image_ImageID",
                schema: "Identity",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Image",
                schema: "Identity",
                table: "Image");

            migrationBuilder.RenameTable(
                name: "Image",
                schema: "Identity",
                newName: "Images",
                newSchema: "Identity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                schema: "Identity",
                table: "Images",
                column: "ID");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Images_ImageID",
                schema: "Identity",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                schema: "Identity",
                table: "Images");

            migrationBuilder.RenameTable(
                name: "Images",
                schema: "Identity",
                newName: "Image",
                newSchema: "Identity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Image",
                schema: "Identity",
                table: "Image",
                column: "ID");

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
    }
}
