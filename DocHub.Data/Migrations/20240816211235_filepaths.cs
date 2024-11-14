using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class filepaths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Documents",
                newName: "FilePaths");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilePaths",
                table: "Documents",
                newName: "FilePath");
        }
    }
}
