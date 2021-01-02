using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCAssignmentTwo.Migrations
{
    public partial class AddedLanguages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "Person",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Person_LanguageId",
                table: "Person",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Languages_LanguageId",
                table: "Person",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Languages_LanguageId",
                table: "Person");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Person_LanguageId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Person");
        }
    }
}
