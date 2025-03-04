﻿// <auto-generated />
using System;
using Acibitah.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Acibitah.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240721181445_reset")]
    partial class reset
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Acibitah.Models.Daily", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Done")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Streak")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Dailies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2024, 7, 21, 20, 14, 45, 89, DateTimeKind.Local).AddTicks(5337),
                            Description = "Breakfast",
                            Done = false,
                            Name = "Breakfast",
                            Streak = 0
                        });
                });

            modelBuilder.Entity("Acibitah.Models.HabbitStats", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateOfUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("HabitId")
                        .HasColumnType("int");

                    b.Property<int>("NegativeValue")
                        .HasColumnType("int");

                    b.Property<int>("PositiveValue")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HabitId");

                    b.ToTable("HabbitStats");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateOfUpdate = new DateTime(2024, 7, 21, 20, 14, 45, 89, DateTimeKind.Local).AddTicks(5383),
                            HabitId = 1,
                            NegativeValue = 5,
                            PositiveValue = 1
                        });
                });

            modelBuilder.Entity("Acibitah.Models.Habit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LifeImpact")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NegativeValue")
                        .HasColumnType("int");

                    b.Property<int?>("PositiveValue")
                        .HasColumnType("int");

                    b.Property<int>("StreakNegative")
                        .HasColumnType("int");

                    b.Property<int>("StreakPositive")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Habits");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Drink water",
                            LifeImpact = 5,
                            Name = "Water",
                            NegativeValue = 3,
                            PositiveValue = 4,
                            StreakNegative = 0,
                            StreakPositive = 0
                        });
                });

            modelBuilder.Entity("Acibitah.Models.Hero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Health")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Power")
                        .HasColumnType("int");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Heroes");
                });

            modelBuilder.Entity("Acibitah.Models.Subtask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Done")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("Subtasks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "I need to do the first thing",
                            Done = false,
                            Name = "First thing to do",
                            TaskId = 1
                        },
                        new
                        {
                            Id = 2,
                            Description = "I need to do the second thing",
                            Done = true,
                            Name = "Second thing to do",
                            TaskId = 1
                        },
                        new
                        {
                            Id = 3,
                            Description = "I need to do the second thing",
                            Done = true,
                            Name = "Second thing to do",
                            TaskId = 1
                        });
                });

            modelBuilder.Entity("Acibitah.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Work"
                        });
                });

            modelBuilder.Entity("Acibitah.Models.TagsTasks", b =>
                {
                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.HasKey("TagId", "TaskId");

                    b.HasIndex("TaskId");

                    b.ToTable("TagsTasks");
                });

            modelBuilder.Entity("Acibitah.Models.ToDoTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Difficulty")
                        .HasColumnType("int");

                    b.Property<bool>("Done")
                        .HasColumnType("bit");

                    b.Property<int>("MoneyImpact")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ToDoTasks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "Test",
                            Done = false,
                            MoneyImpact = 5,
                            Title = "Action"
                        },
                        new
                        {
                            Id = 3,
                            Content = "Test",
                            Done = false,
                            MoneyImpact = 5,
                            Title = "Action"
                        });
                });

            modelBuilder.Entity("Acibitah.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DailyTag", b =>
                {
                    b.Property<int>("DailiesId")
                        .HasColumnType("int");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("DailiesId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("DailyTag");
                });

            modelBuilder.Entity("HabitTag", b =>
                {
                    b.Property<int>("HabitsId")
                        .HasColumnType("int");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("HabitsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("HabitTag");
                });

            modelBuilder.Entity("Acibitah.Models.HabbitStats", b =>
                {
                    b.HasOne("Acibitah.Models.Habit", "Habit")
                        .WithMany()
                        .HasForeignKey("HabitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Habit");
                });

            modelBuilder.Entity("Acibitah.Models.Subtask", b =>
                {
                    b.HasOne("Acibitah.Models.ToDoTask", "Task")
                        .WithMany("Subtasks")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");
                });

            modelBuilder.Entity("Acibitah.Models.TagsTasks", b =>
                {
                    b.HasOne("Acibitah.Models.Tag", "Tag")
                        .WithMany("TagsTasks")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Acibitah.Models.ToDoTask", "Task")
                        .WithMany("TagsTasks")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tag");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("DailyTag", b =>
                {
                    b.HasOne("Acibitah.Models.Daily", null)
                        .WithMany()
                        .HasForeignKey("DailiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Acibitah.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HabitTag", b =>
                {
                    b.HasOne("Acibitah.Models.Habit", null)
                        .WithMany()
                        .HasForeignKey("HabitsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Acibitah.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Acibitah.Models.Tag", b =>
                {
                    b.Navigation("TagsTasks");
                });

            modelBuilder.Entity("Acibitah.Models.ToDoTask", b =>
                {
                    b.Navigation("Subtasks");

                    b.Navigation("TagsTasks");
                });
#pragma warning restore 612, 618
        }
    }
}
