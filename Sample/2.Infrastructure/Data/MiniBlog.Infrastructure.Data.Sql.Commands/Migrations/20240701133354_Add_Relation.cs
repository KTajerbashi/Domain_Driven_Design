using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniBlog.Infrastructure.Data.Sql.Commands.Migrations
{
    /// <inheritdoc />
    public partial class Add_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Courses_CourseId",
                schema: "Blog",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Advertisements_AdvertisementId",
                schema: "Blog",
                table: "Courses");

            migrationBuilder.AlterColumn<long>(
                name: "AdvertisementId",
                schema: "Blog",
                table: "Courses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CourseId",
                schema: "Blog",
                table: "Admins",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Courses_CourseId",
                schema: "Blog",
                table: "Admins",
                column: "CourseId",
                principalSchema: "Blog",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Advertisements_AdvertisementId",
                schema: "Blog",
                table: "Courses",
                column: "AdvertisementId",
                principalSchema: "Blog",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Courses_CourseId",
                schema: "Blog",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Advertisements_AdvertisementId",
                schema: "Blog",
                table: "Courses");

            migrationBuilder.AlterColumn<long>(
                name: "AdvertisementId",
                schema: "Blog",
                table: "Courses",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CourseId",
                schema: "Blog",
                table: "Admins",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Courses_CourseId",
                schema: "Blog",
                table: "Admins",
                column: "CourseId",
                principalSchema: "Blog",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Advertisements_AdvertisementId",
                schema: "Blog",
                table: "Courses",
                column: "AdvertisementId",
                principalSchema: "Blog",
                principalTable: "Advertisements",
                principalColumn: "Id");
        }
    }
}
