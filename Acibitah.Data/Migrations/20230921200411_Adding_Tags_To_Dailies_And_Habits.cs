using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acibitah.Data.Migrations
{
    public partial class Adding_Tags_To_Dailies_And_Habits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyTag",
                columns: table => new
                {
                    DailiesId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyTag", x => new { x.DailiesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_DailyTag_Dailies_DailiesId",
                        column: x => x.DailiesId,
                        principalTable: "Dailies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DailyTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HabitTag",
                columns: table => new
                {
                    HabitsId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitTag", x => new { x.HabitsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_HabitTag_Habits_HabitsId",
                        column: x => x.HabitsId,
                        principalTable: "Habits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HabitTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_DailyTag_TagsId",
                table: "DailyTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_HabitTag_TagsId",
                table: "HabitTag",
                column: "TagsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyTag");

            migrationBuilder.DropTable(
                name: "HabitTag");

            migrationBuilder.UpdateData(
                table: "Dailies",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 9, 20, 22, 10, 4, 81, DateTimeKind.Local).AddTicks(1651));

            migrationBuilder.UpdateData(
                table: "HabbitStats",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfUpdate",
                value: new DateTime(2023, 9, 20, 22, 10, 4, 81, DateTimeKind.Local).AddTicks(1695));
        }
    }
}
