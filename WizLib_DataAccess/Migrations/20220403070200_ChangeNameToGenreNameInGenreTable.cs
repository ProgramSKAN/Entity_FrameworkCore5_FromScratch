using Microsoft.EntityFrameworkCore.Migrations;

namespace WizLib_DataAccess.Migrations
{
    public partial class ChangeNameToGenreNameInGenreTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Genres",
                newName: "GenreName");

            /* rename is new
             In olden way it used to dropcolumn and add new column
              in that case run SQL query before drop

                migrationBuilder.AddColumn(...GenreName)
                migrationBuilder.Sql("UPDATE dbo.genres SET GenreName=Name")

                migrationBuilder.DropColumn(...Name)
             
             */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GenreName",
                table: "Genres",
                newName: "Name");

            /* rename is new
             In olden way it used to dropcolumn and add new column
              in that case run SQL query before drop
            here in down() run reversly

                migrationBuilder.AddColumn(...Name)
                migrationBuilder.Sql("UPDATE dbo.genres SET Name=GenreName")

                migrationBuilder.DropColumn(...GenreName)
             
             */
        }
    }
}
