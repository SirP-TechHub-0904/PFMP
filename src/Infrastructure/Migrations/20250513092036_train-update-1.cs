using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class trainupdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResourcePerson",
                table: "TimeTables",
                newName: "ResponsiblePerson");

            migrationBuilder.AddColumn<string>(
                name: "Day",
                table: "TimeTables",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "TimeTables",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "TimeTables");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "TimeTables");

            migrationBuilder.RenameColumn(
                name: "ResponsiblePerson",
                table: "TimeTables",
                newName: "ResourcePerson");
        }
    }
}
