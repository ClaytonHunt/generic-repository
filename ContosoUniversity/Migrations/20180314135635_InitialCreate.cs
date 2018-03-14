using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ContosoUniversity.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Course",
                table => new
                {
                    CourseId = table.Column<int>(),
                    Credits = table.Column<int>(),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                "Student",
                table => new
                {
                    ID = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EnrollmentDate = table.Column<DateTime>(),
                    FirstMidName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.ID);
                });

            migrationBuilder.CreateTable(
                "Enrollment",
                table => new
                {
                    EnrollmentID = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseID = table.Column<int>(),
                    Grade = table.Column<int>(nullable: true),
                    StudentID = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollment", x => x.EnrollmentID);
                    table.ForeignKey(
                        "FK_Enrollment_Course_CourseID",
                        x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Enrollment_Student_StudentID",
                        x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Enrollment_CourseID",
                "Enrollment",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                "IX_Enrollment_StudentID",
                "Enrollment",
                column: "StudentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Enrollment");

            migrationBuilder.DropTable(
                "Course");

            migrationBuilder.DropTable(
                "Student");
        }
    }
}
