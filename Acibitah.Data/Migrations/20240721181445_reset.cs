using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acibitah.Data.Migrations
{
    public partial class reset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dailies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Streak = table.Column<int>(type: "int", nullable: false),
                    Done = table.Column<bool>(type: "bit", nullable: false)
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
                    StreakNegative = table.Column<int>(type: "int", nullable: false),
                    StreakPositive = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Heroes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Health = table.Column<int>(type: "int", nullable: false),
                    Power = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heroes", x => x.Id);
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
                name: "ToDoTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Difficulty = table.Column<int>(type: "int", nullable: true),
                    MoneyImpact = table.Column<int>(type: "int", nullable: false),
                    Done = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoTasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordSalt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "Subtasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Done = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subtasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subtasks_ToDoTasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "ToDoTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagsTasks",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagsTasks", x => new { x.TagId, x.TaskId });
                    table.ForeignKey(
                        name: "FK_TagsTasks_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagsTasks_ToDoTasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "ToDoTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Dailies",
                columns: new[] { "Id", "Created", "Description", "Done", "Name", "Streak" },
                values: new object[] { 1, new DateTime(2024, 7, 21, 20, 14, 45, 89, DateTimeKind.Local).AddTicks(5337), "Breakfast", false, "Breakfast", 0 });

            migrationBuilder.InsertData(
                table: "Habits",
                columns: new[] { "Id", "Description", "LifeImpact", "Name", "NegativeValue", "PositiveValue", "StreakNegative", "StreakPositive" },
                values: new object[] { 1, "Drink water", 5, "Water", 3, 4, 0, 0 });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Work" });

            migrationBuilder.InsertData(
                table: "ToDoTasks",
                columns: new[] { "Id", "Content", "Difficulty", "Done", "MoneyImpact", "Title" },
                values: new object[,]
                {
                    { 1, "Test", null, false, 5, "Action" },
                    { 3, "Test", null, false, 5, "Action" }
                });

            migrationBuilder.InsertData(
                table: "HabbitStats",
                columns: new[] { "Id", "DateOfUpdate", "HabitId", "NegativeValue", "PositiveValue" },
                values: new object[] { 1, new DateTime(2024, 7, 21, 20, 14, 45, 89, DateTimeKind.Local).AddTicks(5383), 1, 5, 1 });

            migrationBuilder.InsertData(
                table: "Subtasks",
                columns: new[] { "Id", "Description", "Done", "Name", "TaskId" },
                values: new object[,]
                {
                    { 1, "I need to do the first thing", false, "First thing to do", 1 },
                    { 2, "I need to do the second thing", true, "Second thing to do", 1 },
                    { 3, "I need to do the second thing", true, "Second thing to do", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyTag_TagsId",
                table: "DailyTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_HabbitStats_HabitId",
                table: "HabbitStats",
                column: "HabitId");

            migrationBuilder.CreateIndex(
                name: "IX_HabitTag_TagsId",
                table: "HabitTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_Subtasks_TaskId",
                table: "Subtasks",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TagsTasks_TaskId",
                table: "TagsTasks",
                column: "TaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyTag");

            migrationBuilder.DropTable(
                name: "HabbitStats");

            migrationBuilder.DropTable(
                name: "HabitTag");

            migrationBuilder.DropTable(
                name: "Heroes");

            migrationBuilder.DropTable(
                name: "Subtasks");

            migrationBuilder.DropTable(
                name: "TagsTasks");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Dailies");

            migrationBuilder.DropTable(
                name: "Habits");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "ToDoTasks");
        }
    }
}
