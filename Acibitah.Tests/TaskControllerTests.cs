﻿using Acibitah.Controllers;
using Acibitah.Data.Repositories.Interfaces;
using Acibitah.Models;
using Acibitah.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acibitah.Tests
{

    public class TaskControllerTests
    {
        private List<ToDoTask> _activeTasks;
        private List<Subtask> _activeSubTasks;
        private TaskController _taskController;
        private readonly Mock<ITaskRepository> _taskRepositoryMock;

        public TaskControllerTests()
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
            _taskRepositoryMock.Setup(x => x.GetAll()).Returns(_activeTasks);

            _taskController = new TaskController(_taskRepositoryMock.Object);

            ITempDataProvider tempDataProvider = Mock.Of<ITempDataProvider>();
            TempDataDictionaryFactory tempDataDictionaryFactory = new TempDataDictionaryFactory(tempDataProvider);
            ITempDataDictionary tempData = tempDataDictionaryFactory.GetTempData(new DefaultHttpContext());
            _taskController.TempData = tempData;
        }

        [Fact]
        public void ShouldMainViewReceiveOneItem()
        {
            ViewResult result = (ViewResult)_taskController.Index();

            Assert.Equal(typeof(TaskViewModel), result.Model.GetType());
            Assert.Single(((TaskViewModel)result.Model).Tasks);
        }

        [Fact]
        public void ShouldMainViewWorkWithNoneToDoTasks()
        {
            _activeTasks.Clear();
            ViewResult result = (ViewResult) _taskController.Index();
            Assert.Equal(typeof(TaskViewModel), result.Model.GetType());
            Assert.Empty(((TaskViewModel)result.Model).Tasks);
        }

        [Fact]
        public void ShouldSubtasksBeGetForDetails()
        {
            _taskRepositoryMock.Setup(x => x.GetById(_activeTasks.First().Id)).Returns(_activeTasks.First());
            _taskRepositoryMock.Setup(x => x.GetSubtasksByTask(_activeTasks.First())).Returns(_activeSubTasks);

            ViewResult result = (ViewResult)_taskController.Details(_activeTasks.First().Id);

            Assert.Equal(typeof(TaskViewModel), result.Model.GetType());
            Assert.Single(((TaskViewModel)result.Model).Tasks);
            Assert.Single(((TaskViewModel)result.Model).Subtasks);
        }

        [Fact]
        public void ShouldDetailsViewReturnErrorMessageInTempDataWhenNoTaskFound()
        {
            _taskRepositoryMock.Setup(x => x.GetById(5)).Returns((ToDoTask)null);
            var result = _taskController.Details(5);

            Assert.Equal(typeof(RedirectToActionResult), result.GetType());
            Assert.Equal(TaskController.KEY_ERROR_MESSAGE, GetTempDataMessage(TaskController.KEY_ERROR_MESSAGE));
            Assert.Equal(TaskController.TASK_NOT_FOUND, _taskController.TempData[TaskController.KEY_ERROR_MESSAGE]);
            
        }

        [Fact]
        public void ShouldSubtasksBeGetWhenTaskIsValid()
        {
            _taskRepositoryMock.Setup(x => x.GetById(_activeTasks.First().Id)).Returns(_activeTasks.First());
            _taskRepositoryMock.Setup(x => x.GetSubtasksByTask(_activeTasks.First())).Returns(_activeSubTasks); 
            var result = _taskController.Details(_activeTasks.First().Id);

            Assert.Equal(typeof(ViewResult), result.GetType());
            Assert.Equal(null, GetTempDataMessage(TaskController.KEY_ERROR_MESSAGE));
            Assert.Equal(_activeSubTasks.Count(), ((TaskViewModel)((ViewResult)result).Model).Subtasks.Count());
        }

        [Fact]
        public void ShouldCorrectSavingRedirectToIndex()
        {
            ToDoTask task = new ToDoTask() { Title = "NewTitle", Content = "Abc", Difficulty = (int)Difficulty.Easy };
            _taskRepositoryMock.Setup(x => x.Save(task)).Returns(true);
            var result = _taskController.Index(task); 

            Assert.Equal(typeof(RedirectToActionResult), result.GetType());
            Assert.Equal(TaskController.KEY_SUCCESS_MESSAGE, GetTempDataMessage(TaskController.KEY_SUCCESS_MESSAGE));
            Assert.Equal(TaskController.NEW_TASK_CREATED, _taskController.TempData[TaskController.KEY_SUCCESS_MESSAGE]);
            Assert.Equal(null, _taskController.TempData[TaskController.KEY_ERROR_MESSAGE]);
        }


        [Fact]
        public void ShouldInCorrectSavingKeepEntries()
        {
            ToDoTask task = new ToDoTask() { Title = "NewTitle", Content = "Abc", Difficulty = (int)Difficulty.Easy };
            _taskRepositoryMock.Setup(x => x.Save(task)).Returns(false);
            var result = _taskController.Index(task);

            Assert.Equal(typeof(ViewResult), result.GetType());
            Assert.Equal(TaskController.KEY_ERROR_MESSAGE, GetTempDataMessage(TaskController.KEY_ERROR_MESSAGE));
            Assert.Equal(TaskController.TASK_NOT_CREATED, _taskController.TempData[TaskController.KEY_ERROR_MESSAGE]);
            Assert.Equal(task, ((TaskViewModel)((ViewResult)result).Model).ToDo);
            Assert.Equal(null, _taskController.TempData[TaskController.KEY_SUCCESS_MESSAGE]);
        }

        [Fact]
        public void ShouldRemoveWithNonExistentIdShowError()
        {
            _taskRepositoryMock.Setup(x => x.GetById(5)).Returns((ToDoTask)null);
            var result = _taskController.Remove(5);

            Assert.Equal(typeof(RedirectToActionResult), result.GetType());
            Assert.Equal(TaskController.KEY_ERROR_MESSAGE, GetTempDataMessage(TaskController.KEY_ERROR_MESSAGE));
            Assert.Equal(TaskController.TASK_NOT_FOUND, _taskController.TempData[TaskController.KEY_ERROR_MESSAGE]);
            Assert.Equal(null, _taskController.TempData[TaskController.KEY_SUCCESS_MESSAGE]);
        }

        [Fact]
        public void ShouldRemoveSuccessReturnMessage()
        {
            _taskRepositoryMock.Setup(x => x.GetById(_activeTasks.First().Id)).Returns(_activeTasks.First());
            _taskRepositoryMock.Setup(x => x.Remove(_activeTasks.First())).Returns(true);
            var result = _taskController.Remove(_activeTasks.First().Id);

            Assert.Equal(typeof(RedirectToActionResult), result.GetType());
            Assert.Equal(TaskController.KEY_SUCCESS_MESSAGE, GetTempDataMessage(TaskController.KEY_SUCCESS_MESSAGE));
            Assert.Equal(TaskController.SUCCESSFULLY_DELETED, _taskController.TempData[TaskController.KEY_SUCCESS_MESSAGE]);
            Assert.Equal(null, _taskController.TempData[TaskController.KEY_ERROR_MESSAGE]);
        }

        [Fact]
        public void ShouldRemoveErrorOnRepository()
        {
            _taskRepositoryMock.Setup(x => x.GetById(_activeTasks.First().Id)).Returns(_activeTasks.First());
            _taskRepositoryMock.Setup(x => x.Remove(_activeTasks.First())).Returns(false);
            var result = _taskController.Remove(_activeTasks.First().Id);

            Assert.Equal(typeof(RedirectToActionResult), result.GetType());
            Assert.Equal(TaskController.KEY_ERROR_MESSAGE, GetTempDataMessage(TaskController.KEY_ERROR_MESSAGE));
            Assert.Equal(TaskController.REMOVING_PROBLEM, _taskController.TempData[TaskController.KEY_ERROR_MESSAGE]);
            Assert.Equal(null, _taskController.TempData[TaskController.KEY_SUCCESS_MESSAGE]);
        }


        [Fact]
        public void ShouldShowDetailsGoThroughtWithoutError()
        {
            _taskRepositoryMock.Setup(x => x.GetById(_activeTasks.First().Id)).Returns(_activeTasks.First());
            var result = _taskController.Edit(_activeTasks.First().Id);

            Assert.Equal(typeof(ViewResult), result.GetType());
            Assert.Equal(null, GetTempDataMessage(TaskController.KEY_ERROR_MESSAGE));
        }

        [Fact]
        public void ShouldShowDetailsGoThroughtWithError()
        {
            _taskRepositoryMock.Setup(x => x.GetById(_activeTasks.First().Id)).Returns((ToDoTask)null);
            var result = _taskController.Edit(_activeTasks.First().Id);

            Assert.Equal(typeof(ViewResult), result.GetType());
            Assert.Equal(TaskController.KEY_ERROR_MESSAGE, GetTempDataMessage(TaskController.KEY_ERROR_MESSAGE));
            Assert.Equal(TaskController.TASK_NOT_FOUND, _taskController.TempData[TaskController.KEY_ERROR_MESSAGE]);
            Assert.Equal(null, _taskController.TempData[TaskController.KEY_SUCCESS_MESSAGE]);
        }

        [Fact]
        public void ShouldEditThrowAnError()
        {
            _taskRepositoryMock.Setup(x => x.GetById(_activeTasks.First().Id)).Returns(_activeTasks.FirstOrDefault());
            _taskRepositoryMock.Setup(x => x.Update(_activeTasks.First())).Returns(false); 
            var result = _taskController.Edit(_activeTasks.First());

            Assert.Equal(typeof(ViewResult), result.GetType());
            Assert.Equal(TaskController.KEY_ERROR_MESSAGE, GetTempDataMessage(TaskController.KEY_ERROR_MESSAGE));
            Assert.Equal(TaskController.ERROR_TASK_NOT_SAVED, _taskController.TempData[TaskController.KEY_ERROR_MESSAGE]);
            Assert.Equal(null, _taskController.TempData[TaskController.KEY_SUCCESS_MESSAGE]);
        }

        private string GetTempDataMessage(string message)
        {
            string errorKey = null;
            foreach (var key in _taskController.TempData.Keys)
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
