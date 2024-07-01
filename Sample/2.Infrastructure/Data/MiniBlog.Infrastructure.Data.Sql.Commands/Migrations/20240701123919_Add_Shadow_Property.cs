using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniBlog.Infrastructure.Data.Sql.Commands.Migrations
{
    /// <inheritdoc />
    public partial class Add_Shadow_Property : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Security",
                table: "People",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Security",
                table: "People",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Blog",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Blog",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Blog",
                table: "Advertisements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Blog",
                table: "Advertisements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Blog",
                table: "Admins",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Blog",
                table: "Admins",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Security",
                table: "People");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Security",
                table: "People");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Blog",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Blog",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Blog",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Blog",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Blog",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Blog",
                table: "Admins");
        }
    }
}
