using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CenterDragon.Migrations
{
    /// <inheritdoc />
    public partial class Testing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseStudent");

            migrationBuilder.DropTable(
                name: "InstractorStudent");

            migrationBuilder.CreateTable(
                name: "CourseParticipants",
                columns: table => new
                {
                    CoursesId = table.Column<int>(type: "int", nullable: false),
                    ParticipantsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseParticipants", x => new { x.CoursesId, x.ParticipantsId });
                    table.ForeignKey(
                        name: "FK_CourseParticipants_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseParticipants_participants_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstractorParticipants",
                columns: table => new
                {
                    InstractorsId = table.Column<int>(type: "int", nullable: false),
                    ParticipantsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstractorParticipants", x => new { x.InstractorsId, x.ParticipantsId });
                    table.ForeignKey(
                        name: "FK_InstractorParticipants_Instructors_InstractorsId",
                        column: x => x.InstractorsId,
                        principalTable: "Instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstractorParticipants_participants_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseParticipants_ParticipantsId",
                table: "CourseParticipants",
                column: "ParticipantsId");

            migrationBuilder.CreateIndex(
                name: "IX_InstractorParticipants_ParticipantsId",
                table: "InstractorParticipants",
                column: "ParticipantsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseParticipants");

            migrationBuilder.DropTable(
                name: "InstractorParticipants");

            migrationBuilder.CreateTable(
                name: "CourseStudent",
                columns: table => new
                {
                    CoursesId = table.Column<int>(type: "int", nullable: false),
                    StudentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStudent", x => new { x.CoursesId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_CourseStudent_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseStudent_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstractorStudent",
                columns: table => new
                {
                    InstractorsId = table.Column<int>(type: "int", nullable: false),
                    StudentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstractorStudent", x => new { x.InstractorsId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_InstractorStudent_Instructors_InstractorsId",
                        column: x => x.InstractorsId,
                        principalTable: "Instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstractorStudent_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseStudent_StudentsId",
                table: "CourseStudent",
                column: "StudentsId");

            migrationBuilder.CreateIndex(
                name: "IX_InstractorStudent_StudentsId",
                table: "InstractorStudent",
                column: "StudentsId");
        }
    }
}
