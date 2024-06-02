using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.Migrations
{
    public partial class UpdatedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Categories_CategoryId",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseImage_Course_CourseId",
                table: "CourseImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseImage",
                table: "CourseImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Course",
                table: "Course");

            migrationBuilder.RenameTable(
                name: "CourseImage",
                newName: "CourseImages");

            migrationBuilder.RenameTable(
                name: "Course",
                newName: "Courses");

            migrationBuilder.RenameIndex(
                name: "IX_CourseImage_CourseId",
                table: "CourseImages",
                newName: "IX_CourseImages_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Course_CategoryId",
                table: "Courses",
                newName: "IX_Courses_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseImages",
                table: "CourseImages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseImages_Courses_CourseId",
                table: "CourseImages",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Categories_CategoryId",
                table: "Courses",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseImages_Courses_CourseId",
                table: "CourseImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Categories_CategoryId",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseImages",
                table: "CourseImages");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "Course");

            migrationBuilder.RenameTable(
                name: "CourseImages",
                newName: "CourseImage");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_CategoryId",
                table: "Course",
                newName: "IX_Course_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseImages_CourseId",
                table: "CourseImage",
                newName: "IX_CourseImage_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Course",
                table: "Course",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseImage",
                table: "CourseImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Categories_CategoryId",
                table: "Course",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseImage_Course_CourseId",
                table: "CourseImage",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id");
        }
    }
}
