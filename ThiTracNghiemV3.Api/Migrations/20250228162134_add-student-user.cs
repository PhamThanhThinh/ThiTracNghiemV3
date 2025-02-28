using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThiTracNghiemV3.Api.Migrations
{
    /// <inheritdoc />
    public partial class addstudentuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENtuMSYayHOxo2JhujHJuPX6Y9IGCefv68beB9QjHwEmLQBTkFPDh0+4LLhCMHp73Q==");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsApproved", "Name", "PasswordHash", "Phone", "Role" },
                values: new object[] { 2, "student@test.xyz", false, "Thinh Pham (Student)", "AQAAAAIAAYagAAAAELT8gU9otvTF56cQuk/tAyOPPz7gSSqtQa/p7gUDt54zRKw8RJhLSHIS2H9S4Lhvnw==", "0866439504", "Student" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEEljJjDkuRO/4qVTExDzQuqcNo6QAITvrkMsCFR8sRzOFDt/tWhC/C4T2uBPC3xmeA==");
        }
    }
}
