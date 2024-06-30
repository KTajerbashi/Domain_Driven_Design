using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniBlog.Infrastructure.Data.Sql.Commands.Migrations
{
    /// <inheritdoc />
    public partial class Change_Prop_Length : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lenght",
                schema: "Blog",
                table: "Courses",
                newName: "Length");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Length",
                schema: "Blog",
                table: "Courses",
                newName: "Lenght");
        }
    }
}
