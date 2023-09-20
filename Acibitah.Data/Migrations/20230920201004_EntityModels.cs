using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acibitah.Data.Migrations
{
    public partial class EntityModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MoneyImpact",
                table: "ToDoTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Dailies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Streak = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dailies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Habits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NegativeValue = table.Column<int>(type: "int", nullable: true),
                    PositiveValue = table.Column<int>(type: "int", nullable: true),
                    LifeImpact = table.Column<int>(type: "int", nullable: false),
                    Streak = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubtaskToDoTask",
                columns: table => new
                {
                    SubtasksId = table.Column<int>(type: "int", nullable: false),
                    TasksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubtaskToDoTask", x => new { x.SubtasksId, x.TasksId });
                    table.ForeignKey(
                        name: "FK_SubtaskToDoTask_Subtasks_SubtasksId",
                        column: x => x.SubtasksId,
                        principalTable: "Subtasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubtaskToDoTask_ToDoTasks_TasksId",
                        column: x => x.TasksId,
                        principalTable: "ToDoTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HabbitStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HabitId = table.Column<int>(type: "int", nullable: false),
                    DateOfUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NegativeValue = table.Column<int>(type: "int", nullable: false),
                    PositiveValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabbitStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HabbitStats_Habits_HabitId",
                        column: x => x.HabitId,
                        principalTable: "Habits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagToDoTask",
                columns: table => new
                {
                    TagsId = table.Column<int>(type: "int", nullable: false),
                    TasksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagToDoTask", x => new { x.TagsId, x.TasksId });
                    table.ForeignKey(
                        name: "FK_TagToDoTask_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagToDoTask_ToDoTasks_TasksId",
                        column: x => x.TasksId,
                        principalTable: "ToDoTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Dailies",
                columns: new[] { "Id", "Created", "Description", "Name", "Streak" },
                values: new object[] { 1, new DateTime(2023, 9, 20, 22, 10, 4, 81, DateTimeKind.Local).AddTicks(1651), "Breakfast", "Breakfast", 0 });

            migrationBuilder.InsertData(
                table: "Habits",
                columns: new[] { "Id", "Description", "LifeImpact", "Name", "NegativeValue", "PositiveValue", "Streak" },
                values: new object[] { 1, "Drink water", 5, "Water", 3, 4, 0 });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Work" });

            migrationBuilder.UpdateData(
                table: "ToDoTasks",
                keyColumn: "Id",
                keyValue: 1,
                column: "MoneyImpact",
                value: 5);

            migrationBuilder.InsertData(
                table: "HabbitStats",
                columns: new[] { "Id", "DateOfUpdate", "HabitId", "NegativeValue", "PositiveValue" },
                values: new object[] { 1, new DateTime(2023, 9, 20, 22, 10, 4, 81, DateTimeKind.Local).AddTicks(1695), 1, 5, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_HabbitStats_HabitId",
                table: "HabbitStats",
                column: "HabitId");

            migrationBuilder.CreateIndex(
                name: "IX_SubtaskToDoTask_TasksId",
                table: "SubtaskToDoTask",
                column: "TasksId");

            migrationBuilder.CreateIndex(
                name: "IX_TagToDoTask_TasksId",
                table: "TagToDoTask",
                column: "TasksId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dailies");

            migrationBuilder.DropTable(
                name: "HabbitStats");

            migrationBuilder.DropTable(
                name: "SubtaskToDoTask");

            migrationBuilder.DropTable(
                name: "TagToDoTask");

            migrationBuilder.DropTable(
                name: "Habits");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropColumn(
                name: "MoneyImpact",
                table: "ToDoTasks");
        }
    }
}
