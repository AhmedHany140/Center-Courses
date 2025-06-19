using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CenterDragon.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMessageModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Secretaries_RecieverId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Secretaries_SenderId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Students_RecieverId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Students_SenderId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_RecieverId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_SenderId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "RecieverId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "SecertaryReciverId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SecertarySenderId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentReciverId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentSenderId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SecertaryReciverId",
                table: "Messages",
                column: "SecertaryReciverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SecertarySenderId",
                table: "Messages",
                column: "SecertarySenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_StudentReciverId",
                table: "Messages",
                column: "StudentReciverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_StudentSenderId",
                table: "Messages",
                column: "StudentSenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Secretaries_SecertaryReciverId",
                table: "Messages",
                column: "SecertaryReciverId",
                principalTable: "Secretaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Secretaries_SecertarySenderId",
                table: "Messages",
                column: "SecertarySenderId",
                principalTable: "Secretaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Students_StudentReciverId",
                table: "Messages",
                column: "StudentReciverId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Students_StudentSenderId",
                table: "Messages",
                column: "StudentSenderId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Secretaries_SecertaryReciverId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Secretaries_SecertarySenderId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Students_StudentReciverId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Students_StudentSenderId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_SecertaryReciverId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_SecertarySenderId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_StudentReciverId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_StudentSenderId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SecertaryReciverId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SecertarySenderId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "StudentReciverId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "StudentSenderId",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "RecieverId",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SenderId",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RecieverId",
                table: "Messages",
                column: "RecieverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Secretaries_RecieverId",
                table: "Messages",
                column: "RecieverId",
                principalTable: "Secretaries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Secretaries_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "Secretaries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Students_RecieverId",
                table: "Messages",
                column: "RecieverId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Students_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
