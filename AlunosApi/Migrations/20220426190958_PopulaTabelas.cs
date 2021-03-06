using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlunosApi.Migrations
{
    public partial class PopulaTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "Email", "Idade", "Name" },
                values: new object[] { 1, "mariapenha@yahoo.com", 23, "Maria da Penha" });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "Email", "Idade", "Name" },
                values: new object[] { 2, "manualbueno@yahoo.com", 22, "Manuel Bueno" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Alunos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Alunos",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
