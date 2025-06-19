using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CenterDragon.Migrations
{
    /// <inheritdoc />
    public partial class Laster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_participants_Instructors_InstractorId",
                table: "participants");

            migrationBuilder.AlterColumn<int>(
                name: "InstractorId",
                table: "participants",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "participantViewModelId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "participantViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Ediation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParCourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_participantViewModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_participantViewModelId",
                table: "Courses",
                column: "participantViewModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_participantViewModel_participantViewModelId",
                table: "Courses",
                column: "participantViewModelId",
                principalTable: "participantViewModel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_participants_Instructors_InstractorId",
                table: "participants",
                column: "InstractorId",
                principalTable: "Instructors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_participantViewModel_participantViewModelId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_participants_Instructors_InstractorId",
                table: "participants");

            migrationBuilder.DropTable(
                name: "participantViewModel");

            migrationBuilder.DropIndex(
                name: "IX_Courses_participantViewModelId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "participantViewModelId",
                table: "Courses");

            migrationBuilder.AlterColumn<int>(
                name: "InstractorId",
                table: "participants",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_participants_Instructors_InstractorId",
                table: "participants",
                column: "InstractorId",
                principalTable: "Instructors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
