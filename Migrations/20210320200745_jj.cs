using Microsoft.EntityFrameworkCore.Migrations;

namespace NOUR.Migrations
{
    public partial class jj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Case_Users_UserId1",
                table: "Case");

            migrationBuilder.DropIndex(
                name: "IX_Case_UserId1",
                table: "Case");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Case");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Case",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Case_UserId",
                table: "Case",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Case_Users_UserId",
                table: "Case",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Case_Users_UserId",
                table: "Case");

            migrationBuilder.DropIndex(
                name: "IX_Case_UserId",
                table: "Case");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Case",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Case",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Case_UserId1",
                table: "Case",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Case_Users_UserId1",
                table: "Case",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
