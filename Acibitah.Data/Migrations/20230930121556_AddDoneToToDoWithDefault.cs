using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acibitah.Data.Migrations
{
    public partial class AddDoneToToDoWithDefault : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Done",
                table: "ToDoTasks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Dailies",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 9, 30, 14, 15, 55, 919, DateTimeKind.Local).AddTicks(9933));

            migrationBuilder.UpdateData(
                table: "HabbitStats",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfUpdate",
                value: new DateTime(2023, 9, 30, 14, 15, 55, 919, DateTimeKind.Local).AddTicks(9983));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Done",
                table: "ToDoTasks");

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
    }
}
