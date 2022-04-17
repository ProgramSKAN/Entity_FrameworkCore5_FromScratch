using Microsoft.EntityFrameworkCore.Migrations;

namespace WizLib_DataAccess.Migrations
{
    public partial class AddRawCategoryToTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //this is manually writted after doing "add-migration AddRawCategoryToTable" which generated empty migration
            migrationBuilder.Sql("INSERT INTO tbl_category VALUES ('Catgry 1')");
            migrationBuilder.Sql("INSERT INTO tbl_category VALUES ('Catgry 2')");
            migrationBuilder.Sql("INSERT INTO tbl_category VALUES ('Catgry 3')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
