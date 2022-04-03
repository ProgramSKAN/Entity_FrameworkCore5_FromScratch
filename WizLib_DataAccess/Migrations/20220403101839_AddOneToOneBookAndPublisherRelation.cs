using Microsoft.EntityFrameworkCore.Migrations;

namespace WizLib_DataAccess.Migrations
{
    public partial class AddOneToOneBookAndPublisherRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Publisher_Id",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Publisher_Id1",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_Publisher_Id1",
                table: "Books",
                column: "Publisher_Id1");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publishers_Publisher_Id1",
                table: "Books",
                column: "Publisher_Id1",
                principalTable: "Publishers",
                principalColumn: "Publisher_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publishers_Publisher_Id1",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_Publisher_Id1",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Publisher_Id",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Publisher_Id1",
                table: "Books");
        }
    }
}
