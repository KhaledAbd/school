using Microsoft.EntityFrameworkCore.Migrations;

namespace school.Migrations
{
    public partial class firstMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Stage_StageId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_StageId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "StageId",
                table: "Student");

            migrationBuilder.AddColumn<bool>(
                name: "IsReject",
                table: "Student",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NumOfReject",
                table: "Student",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimeAbsentDaily",
                table: "Student",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReject",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "NumOfReject",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "TimeAbsentDaily",
                table: "Student");

            migrationBuilder.AddColumn<int>(
                name: "StageId",
                table: "Student",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_StageId",
                table: "Student",
                column: "StageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Stage_StageId",
                table: "Student",
                column: "StageId",
                principalTable: "Stage",
                principalColumn: "StageId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
