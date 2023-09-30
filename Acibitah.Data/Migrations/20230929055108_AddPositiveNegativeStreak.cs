using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acibitah.Data.Migrations
{
    public partial class AddPositiveNegativeStreak : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Streak",
                table: "Habits",
                newName: "StreakPositive");

            migrationBuilder.AddColumn<int>(
                name: "StreakNegative",
                table: "Habits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Dailies",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 9, 29, 7, 51, 7, 917, DateTimeKind.Local).AddTicks(6759));

            migrationBuilder.UpdateData(
                table: "HabbitStats",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfUpdate",
                value: new DateTime(2023, 9, 29, 7, 51, 7, 917, DateTimeKind.Local).AddTicks(6809));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StreakNegative",
                table: "Habits");

            migrationBuilder.RenameColumn(
                name: "StreakPositive",
                table: "Habits",
                newName: "Streak");

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
        }
    }
}
