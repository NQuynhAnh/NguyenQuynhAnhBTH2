using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NguyenQuynhAnhBTH2.Migrations
{
    /// <inheritdoc />
    public partial class CreateForeignkeyStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FacultyID",
                table: "Student",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Student_FacultyID",
                table: "Student",
                column: "FacultyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Faculty_FacultyID",
                table: "Student",
                column: "FacultyID",
                principalTable: "Faculty",
                principalColumn: "FacultyID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Faculty_FacultyID",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_FacultyID",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "FacultyID",
                table: "Student");
        }
    }
}
