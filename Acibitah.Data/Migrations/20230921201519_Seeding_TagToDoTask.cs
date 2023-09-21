using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acibitah.Data.Migrations
{
    public partial class Seeding_TagToDoTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Dailies",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 9, 21, 22, 15, 19, 63, DateTimeKind.Local).AddTicks(8892));

            migrationBuilder.UpdateData(
                table: "HabbitStats",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfUpdate",
                value: new DateTime(2023, 9, 21, 22, 15, 19, 63, DateTimeKind.Local).AddTicks(8942));

            migrationBuilder.InsertData(
                table: "TagToDoTask",
                columns: new[] { "TagsId", "TasksId" },
                values: new object[] { 1, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TagToDoTask",
                keyColumns: new[] { "TagsId", "TasksId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.UpdateData(
                table: "Dailies",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 9, 21, 22, 4, 10, 953, DateTimeKind.Local).AddTicks(9598));

            migrationBuilder.UpdateData(
                table: "HabbitStats",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfUpdate",
                value: new DateTime(2023, 9, 21, 22, 4, 10, 953, DateTimeKind.Local).AddTicks(9646));
        }
    }
}
