﻿using Acibitah.Data.Repositories.Interfaces;
using Acibitah.Models;
using Acibitah.Models.Enums;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;

namespace Acibitah.Tests.Controllers
{
    public class BaseTest
    {
        public List<ToDoTask> _activeTasks;
        public List<Subtask> _activeSubTasks;
        public List<Habit> _habits;
        public List<Daily> _dailies;
        protected readonly Mock<ITaskRepository> _taskRepositoryMock;
        protected readonly Mock<IHabitRepository> _habitRepositoryMock;
        protected readonly Mock<IDailyRepository> _dailyRepositoryMock;
        protected readonly Mock<ITagsRepository> _tagsRepositoryMock; 
        public BaseTest()
        {
            _activeTasks = new List<ToDoTask>
            {
                new ToDoTask{ Id = 1, Title = "Task", Content = "Stuff to do", Difficulty = (int)Difficulty.Easy }
            };
            _activeSubTasks = new List<Subtask>
            {
                new Subtask{ Id = 1, Name = "SubTask", Description = "Stuff to do", Done = false,  TaskId = 1}
            };
            _habits = new List<Habit>
            {
                new Habit() { Id = 1, Description = "Drink water", Name = "Water", LifeImpact = 5, NegativeValue = 3, PositiveValue = 4 }
            };
            _dailies = new List<Daily>()
            {
                new Daily() { Id = 1, Description = "Breakfast", Name = "Breakfast", Created = DateTime.Now }
            };


            _taskRepositoryMock = new Mock<ITaskRepository>();
            _habitRepositoryMock = new Mock<IHabitRepository>();
            _dailyRepositoryMock = new Mock<IDailyRepository>();
            _tagsRepositoryMock = new Mock<ITagsRepository>();
        }

        protected string GetTempDataMessage(string message, ITempDataDictionary temp)
        {
            string errorKey = null;
            foreach (var key in temp.Keys)
            {
                if (key == message)
                {
                    errorKey = key;
                    break;
                }
            }
            return errorKey;
        }

    }
}
