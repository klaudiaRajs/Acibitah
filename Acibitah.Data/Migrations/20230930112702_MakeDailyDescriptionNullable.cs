using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acibitah.Data.Migrations
{
    public partial class MakeDailyDescriptionNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Dailies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Dailies",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 9, 30, 13, 27, 1, 921, DateTimeKind.Local).AddTicks(9032));

            migrationBuilder.UpdateData(
                table: "HabbitStats",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfUpdate",
                value: new DateTime(2023, 9, 30, 13, 27, 1, 921, DateTimeKind.Local).AddTicks(9083));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Dailies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Dailies",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 9, 30, 13, 1, 37, 263, DateTimeKind.Local).AddTicks(9409));

            migrationBuilder.UpdateData(
                table: "HabbitStats",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfUpdate",
                value: new DateTime(2023, 9, 30, 13, 1, 37, 263, DateTimeKind.Local).AddTicks(9454));
        }
    }
}
