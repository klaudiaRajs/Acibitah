using Acibitah.Models;
using Acibitah.Models.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acibitah.Tests
{
    public class TestDataGenerator : IEnumerable<object[]>
    {
        public static IEnumerable<object[]> GetPersonFromDataGenerator()
        {
            List<ToDoTask> _activeTasks = new List<ToDoTask>
            {
                new ToDoTask{ Id = 1, Title = "Task", Content = "Stuff to do", Difficulty = (int)Difficulty.Easy }
            };
            List<Subtask> _activeSubTasks = new List<Subtask>
            {
                new Subtask{ Id = 1, Name = "SubTask", Description = "Stuff to do", Done = false,  TaskId = 1}
            };
            List<Habit> _habits = new List<Habit>
            {
                new Habit() { Id = 1, Description = "Drink water", Name = "Water", LifeImpact = 5, NegativeValue = 3, PositiveValue = 4}
            };
            List<Daily> _dailies = new List<Daily>()
            {
                new Daily() { Id = 1, Description = "Breakfast", Name = "Breakfast", Created = DateTime.Now }
            };


            yield return new object[]
            {
                _habits,
                (List<Daily>) null,
                _activeTasks
            };

            yield return new object[]
            {
                (List<Habit>) null,
                _dailies,
                _activeTasks
            };


            yield return new object[]
            {
                _habits,
                _dailies,
                (List<ToDoTask>)null
            };
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
