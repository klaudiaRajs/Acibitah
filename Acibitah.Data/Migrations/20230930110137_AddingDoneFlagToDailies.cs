using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acibitah.Data.Migrations
{
    public partial class AddingDoneFlagToDailies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Done",
                table: "Dailies",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Done",
                table: "Dailies");

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
    }
}
