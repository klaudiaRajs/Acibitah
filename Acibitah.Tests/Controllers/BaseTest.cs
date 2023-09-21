using Acibitah.Data.Repositories.Interfaces;
using Acibitah.Models;
using Acibitah.Models.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acibitah.Tests.Controllers
{
    public class BaseTest
    {
        protected List<ToDoTask> _activeTasks;
        protected List<Subtask> _activeSubTasks;
        protected readonly Mock<ITaskRepository> _taskRepositoryMock;
        protected readonly Mock<IHabitRepository> _habitRepositoryMock;
        protected readonly Mock<IDailyRepository> _dailyRepositoryMock;
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
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _habitRepositoryMock = new Mock<IHabitRepository>();
            _dailyRepositoryMock = new Mock<IDailyRepository>();
        }

    }
}
