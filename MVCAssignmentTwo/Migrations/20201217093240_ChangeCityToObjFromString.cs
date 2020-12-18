using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCAssignmentTwo.Migrations
{
    public partial class ChangeCityToObjFromString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Cities_TheCityId",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_TheCityId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "TheCityId",
                table: "Person");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Person",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_CityId",
                table: "Person",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Cities_CityId",
                table: "Person",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Cities_CityId",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_CityId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Person");

            migrationBuilder.AddColumn<int>(
                name: "TheCityId",
                table: "Person",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_TheCityId",
                table: "Person",
                column: "TheCityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Cities_TheCityId",
                table: "Person",
                column: "TheCityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
