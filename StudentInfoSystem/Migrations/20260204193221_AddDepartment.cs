using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentInfoSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Courses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "CS", "Computer Science" },
                    { 2, "MATH", "Mathematics" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user_admin",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1cd70795-b951-4c2d-970d-bdfac9ba6d4c", "AQAAAAIAAYagAAAAEOqJ3ltjUKy3kEmRkPg6qvS/hurVDeZD16PUalV+BVwJgQpuFRpRsW0U/E9ZkzWXNg==", "04ebdcc8-75c6-4eab-a7f5-f123ee4e3878" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user_lecturer",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "707e74e5-f769-4e2b-8f8c-15c500a20cb1", "AQAAAAIAAYagAAAAEIh9zy7h+wisjqmlDa1OX70ha+M3mcDoxQeWNYSeWKqMiT5OX6P6tnqeErjnoJ5heA==", "166b179b-2f43-4f13-99ed-2eec8ca33d88" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user_student",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2e75840a-549a-4108-9c6b-aa4c19a5aa6b", "AQAAAAIAAYagAAAAEDUDgxdDTV17Co42cFZW4anXfa0yXkp3HOsbxZfKQtaZeCwrFwufV+Dj8/ei/jUsMw==", "2e092c5e-d4c5-4e12-b467-8d2752fea252" });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                column: "DepartmentId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                column: "DepartmentId",
                value: 1);


            migrationBuilder.CreateIndex(
                name: "IX_Courses_DepartmentId",
                table: "Courses",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Departments_DepartmentId",
                table: "Courses",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Departments_DepartmentId",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Courses_DepartmentId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Courses");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user_admin",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "294217bf-007e-42fc-b56d-a547b393a64c", "AQAAAAIAAYagAAAAEKPYJIx5uaWwK5O1lSxglY4RnX+1yxpY+K1Gmko987QkZhhaXeCxuP7GQsykH6rgYg==", "483c0022-5fb5-4eac-a7ef-9206530477b2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user_lecturer",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9f46bb65-5568-4659-a58c-e06e13be0315", "AQAAAAIAAYagAAAAEFSd3M3+JX+dgqEm8hSvrzkRELq6u47cVaJn5A3srIWqGK4uMDc1Crzs/X9NQEvu8w==", "3ef3abcf-53bc-4eed-bfd2-c7665fee9fac" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user_student",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "07f0e385-698f-41f0-b5da-1d95e54a33ee", "AQAAAAIAAYagAAAAEPNvJM5OlNeaEwI2IjDCWs4aKJg93SjeyLqhpo7IPYqV1/qSSpv4OQiASEBaDP4r/A==", "985a69c7-208e-4e55-b18c-eca7f6afcaee" });
        }
    }
}
