using Acibitah.Controllers;
using Acibitah.Models;
using Acibitah.Models.Enums;
using Acibitah.Tests.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;

namespace Acibitah.Tests
{
    public class TaskControllerTests : BaseTest
    {
        private TaskController _taskController;
        private Habit _habit;
        private Daily _daily; 
        private ToDoTask _toDoTask;

        public TaskControllerTests()
        {
            _taskRepositoryMock.Setup(x => x.GetAll()).Returns(_activeTasks);
            

            _taskController = new TaskController(_taskRepositoryMock.Object, _habitRepositoryMock.Object, _dailyRepositoryMock.Object);

            ITempDataProvider tempDataProvider = Mock.Of<ITempDataProvider>();
            TempDataDictionaryFactory tempDataDictionaryFactory = new TempDataDictionaryFactory(tempDataProvider);
            ITempDataDictionary tempData = tempDataDictionaryFactory.GetTempData(new DefaultHttpContext());
            _taskController.TempData = tempData;
            _habit = _habits.FirstOrDefault();
            _daily = _dailies.FirstOrDefault(); 
            _toDoTask = _activeTasks.FirstOrDefault();
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
            ViewResult result = (ViewResult)_taskController.Index();
            Assert.Equal(typeof(TaskViewModel), result.Model.GetType());
            Assert.Empty(((TaskViewModel)result.Model).Tasks);
        }

        [Fact]
        public void ShouldSubtasksBeGetForDetails()
        {
            _taskRepositoryMock.Setup(x => x.GetById(_toDoTask.Id)).Returns(_toDoTask);
            _taskRepositoryMock.Setup(x => x.GetSubtasksByTask(_toDoTask)).Returns(_activeSubTasks);

            ViewResult result = (ViewResult)_taskController.Details(_toDoTask.Id);

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
            Assert.Equal(TaskController.KEY_ERROR_MESSAGE, GetTempDataMessage(TaskController.KEY_ERROR_MESSAGE, _taskController.TempData));
            Assert.Equal(TaskController.TASK_NOT_FOUND, _taskController.TempData[TaskController.KEY_ERROR_MESSAGE]);

        }

        [Fact]
        public void ShouldSubtasksBeGetWhenTaskIsValid()
        {
            _taskRepositoryMock.Setup(x => x.GetById(_toDoTask.Id)).Returns(_toDoTask);
            _taskRepositoryMock.Setup(x => x.GetSubtasksByTask(_toDoTask)).Returns(_activeSubTasks);
            var result = _taskController.Details(_toDoTask.Id);

            Assert.Equal(typeof(ViewResult), result.GetType());
            Assert.Equal(null, GetTempDataMessage(TaskController.KEY_ERROR_MESSAGE, _taskController.TempData));
            Assert.Equal(_activeSubTasks.Count(), ((TaskViewModel)((ViewResult)result).Model).Subtasks.Count());
        }

        [Fact]
        public void ShouldCorrectSavingRedirectToIndex()
        {
            ToDoTask task = new ToDoTask() { Title = "NewTitle", Content = "Abc", Difficulty = (int)Difficulty.Easy };
            _taskRepositoryMock.Setup(x => x.Save(task)).Returns(true);
            var result = _taskController.Index(task);

            Assert.Equal(typeof(RedirectToActionResult), result.GetType());
            Assert.Equal(TaskController.KEY_SUCCESS_MESSAGE, GetTempDataMessage(TaskController.KEY_SUCCESS_MESSAGE, _taskController.TempData));
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
            Assert.Equal(TaskController.KEY_ERROR_MESSAGE, GetTempDataMessage(TaskController.KEY_ERROR_MESSAGE, _taskController.TempData));
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
            Assert.Equal(TaskController.KEY_ERROR_MESSAGE, GetTempDataMessage(TaskController.KEY_ERROR_MESSAGE, _taskController.TempData));
            Assert.Equal(TaskController.TASK_NOT_FOUND, _taskController.TempData[TaskController.KEY_ERROR_MESSAGE]);
            Assert.Equal(null, _taskController.TempData[TaskController.KEY_SUCCESS_MESSAGE]);
        }

        [Fact]
        public void ShouldRemoveSuccessReturnMessage()
        {
            _taskRepositoryMock.Setup(x => x.GetById(_toDoTask.Id)).Returns(_toDoTask);
            _taskRepositoryMock.Setup(x => x.Remove(_toDoTask)).Returns(true);
            var result = _taskController.Remove(_toDoTask.Id);

            Assert.Equal(typeof(RedirectToActionResult), result.GetType());
            Assert.Equal(TaskController.KEY_SUCCESS_MESSAGE, GetTempDataMessage(TaskController.KEY_SUCCESS_MESSAGE, _taskController.TempData));
            Assert.Equal(TaskController.SUCCESSFULLY_DELETED, _taskController.TempData[TaskController.KEY_SUCCESS_MESSAGE]);
            Assert.Equal(null, _taskController.TempData[TaskController.KEY_ERROR_MESSAGE]);
        }

        [Fact]
        public void ShouldRemoveErrorOnRepository()
        {
            _taskRepositoryMock.Setup(x => x.GetById(_toDoTask.Id)).Returns(_toDoTask);
            _taskRepositoryMock.Setup(x => x.Remove(_toDoTask)).Returns(false);
            var result = _taskController.Remove(_toDoTask.Id);

            Assert.Equal(typeof(RedirectToActionResult), result.GetType());
            Assert.Equal(TaskController.KEY_ERROR_MESSAGE, GetTempDataMessage(TaskController.KEY_ERROR_MESSAGE, _taskController.TempData));
            Assert.Equal(TaskController.REMOVING_PROBLEM, _taskController.TempData[TaskController.KEY_ERROR_MESSAGE]);
            Assert.Equal(null, _taskController.TempData[TaskController.KEY_SUCCESS_MESSAGE]);
        }


        [Fact]
        public void ShouldShowDetailsGoThroughtWithoutError()
        {
            _taskRepositoryMock.Setup(x => x.GetById(_toDoTask.Id)).Returns(_toDoTask);
            var result = _taskController.Edit(_toDoTask.Id);

            Assert.Equal(typeof(ViewResult), result.GetType());
            Assert.Equal(null, GetTempDataMessage(TaskController.KEY_ERROR_MESSAGE, _taskController.TempData));
        }

        [Fact]
        public void ShouldShowDetailsGoThroughtWithError()
        {
            _taskRepositoryMock.Setup(x => x.GetById(_toDoTask.Id)).Returns((ToDoTask)null);
            var result = _taskController.Edit(_toDoTask.Id);

            Assert.Equal(typeof(ViewResult), result.GetType());
            Assert.Equal(TaskController.KEY_ERROR_MESSAGE, GetTempDataMessage(TaskController.KEY_ERROR_MESSAGE, _taskController.TempData));
            Assert.Equal(TaskController.TASK_NOT_FOUND, _taskController.TempData[TaskController.KEY_ERROR_MESSAGE]);
            Assert.Equal(null, _taskController.TempData[TaskController.KEY_SUCCESS_MESSAGE]);
        }

        [Fact]
        public void ShouldEditThrowAnError()
        {
            _taskRepositoryMock.Setup(x => x.GetById(_toDoTask.Id)).Returns(_toDoTask);
            _taskRepositoryMock.Setup(x => x.Update(_toDoTask)).Returns(false);
            var result = _taskController.Edit(_toDoTask);

            Assert.Equal(typeof(ViewResult), result.GetType());
            Assert.Equal(TaskController.KEY_ERROR_MESSAGE, GetTempDataMessage(TaskController.KEY_ERROR_MESSAGE, _taskController.TempData));
            Assert.Equal(TaskController.ERROR_TASK_NOT_SAVED, _taskController.TempData[TaskController.KEY_ERROR_MESSAGE]);
            Assert.Equal(null, _taskController.TempData[TaskController.KEY_SUCCESS_MESSAGE]);
        }

        [Fact]
        public void ShouldEditThrowAnErrorWhenNoTask()
        {
            _taskRepositoryMock.Setup(x => x.GetById(_toDoTask.Id)).Returns((ToDoTask)null);
            var result = _taskController.Edit(_toDoTask);

            _taskRepositoryMock.Verify(x => x.Update(_toDoTask), Times.Never()); 
            Assert.Equal(typeof(RedirectToActionResult), result.GetType());
            Assert.Equal(TaskController.KEY_ERROR_MESSAGE, GetTempDataMessage(TaskController.KEY_ERROR_MESSAGE, _taskController.TempData));
            Assert.Equal(TaskController.TASK_NOT_FOUND, _taskController.TempData[TaskController.KEY_ERROR_MESSAGE]);
            Assert.Equal(null, _taskController.TempData[TaskController.KEY_SUCCESS_MESSAGE]);
        }

        [Fact]
        public void ShouldEditShowSuccessMessage()
        {
            _taskRepositoryMock.Setup(x => x.GetById(_toDoTask.Id)).Returns(_toDoTask);
            _taskRepositoryMock.Setup(x => x.Update(_toDoTask)).Returns(true);
            var result = _taskController.Edit(_toDoTask);

            _taskRepositoryMock.Verify(x => x.Update(_toDoTask), Times.Once());
            Assert.Equal(typeof(RedirectToActionResult), result.GetType());
            Assert.Equal(null, GetTempDataMessage(TaskController.KEY_ERROR_MESSAGE, _taskController.TempData));
            Assert.Equal(TaskController.SUCCESS_SAVED, _taskController.TempData[TaskController.KEY_SUCCESS_MESSAGE]);
            Assert.Equal(null, _taskController.TempData[TaskController.KEY_ERROR_MESSAGE]);
        }

        [Fact]
        public void ShouldHabitNotFoundThrowException()
        {
            _habitRepositoryMock.Setup(m => m.GetById(_habit.Id)).Returns((Habit)null);
            _habitRepositoryMock.Verify(m => m.IncreaseStreak(_habit), Times.Never());
            var result = _taskController.IncreaseStreaks(_habit.Id, true, false);


            Assert.Equal(typeof(RedirectToActionResult), result.GetType());
            Assert.Equal(TaskController.TASK_NOT_FOUND, _taskController.TempData[TaskController.KEY_ERROR_MESSAGE]);
        }

        [Fact]
        public void ShouldIncreaseStreakBeCalledOnceFromRepoAndShowErrorMessage()
        {
            _habitRepositoryMock.Setup(m => m.GetById(_habit.Id)).Returns(_habit);
            _habitRepositoryMock.Setup(m => m.IncreaseStreak(_habit)).Returns(false); 

            var result = _taskController.IncreaseStreaks(_habit.Id, true, false);
            _habitRepositoryMock.Verify(m => m.IncreaseStreak(_habit), Times.Once());

            Assert.Equal(TaskController.ERROR_SAVING, _taskController.TempData[TaskController.KEY_ERROR_MESSAGE]);
        }

        [Fact]
        public void ShouldHabitPositiveStreakIncreaseWhenRepoReturnsTrue()
        {
            Assert.Equal(0, _habit.StreakPositive);
            _habitRepositoryMock.Setup(m => m.GetById(_habit.Id)).Returns(_habit);
            _habitRepositoryMock.Setup(m => m.IncreaseStreak(_habit)).Returns(true);

            var result = _taskController.IncreaseStreaks(_habit.Id, true, false);
            _habitRepositoryMock.Verify(m => m.IncreaseStreak(_habit), Times.Once());

            Assert.Equal(TaskController.SUCCESS_SAVED, _taskController.TempData[TaskController.KEY_SUCCESS_MESSAGE]);
            Assert.Equal(1, _habit.StreakPositive);
        }

        [Fact]
        public void ShouldHabitNegativeStreakIncreaseWhenRepoReturnsTrue()
        {
            Assert.Equal(0, _habit.StreakNegative);
            _habitRepositoryMock.Setup(m => m.GetById(_habit.Id)).Returns(_habit);
            _habitRepositoryMock.Setup(m => m.IncreaseStreak(_habit)).Returns(true);

            var result = _taskController.IncreaseStreaks(_habit.Id, false, true);
            _habitRepositoryMock.Verify(m => m.IncreaseStreak(_habit), Times.Once());

            Assert.Equal(TaskController.SUCCESS_SAVED, _taskController.TempData[TaskController.KEY_SUCCESS_MESSAGE]);
            Assert.Equal(1, _habit.StreakNegative);
        }

        [Fact]
        public void ShouldCheckDailyShowErrorMessageWhenDailyNotFound()
        {
            _dailyRepositoryMock.Setup(m => m.GetById(_daily.Id)).Returns((Daily)null);
            _taskController.CheckDaily(_daily.Id);

            Assert.Equal(TaskController.TASK_NOT_FOUND, _taskController.TempData[TaskController.KEY_ERROR_MESSAGE]);
            _dailyRepositoryMock.Verify(m => m.MarkAsDone(_daily), Times.Never()); 
        }

        [Fact]
        public void ShouldCheckDailyShowErrorMessageWhenRepoReturnsFalls()
        {
            _dailyRepositoryMock.Setup(m => m.GetById(_daily.Id)).Returns(_daily);
            _dailyRepositoryMock.Setup(m => m.MarkAsDone(_daily)).Returns(false);
            _taskController.CheckDaily(_daily.Id);

            Assert.Equal(TaskController.ERROR_SAVING, _taskController.TempData[TaskController.KEY_ERROR_MESSAGE]);
            _dailyRepositoryMock.Verify(m => m.MarkAsDone(_daily), Times.Once());
        }
        [Fact]
        public void ShouldCheckToDoShowErrorMessageWhenDailyNotFound()
        {
            _taskRepositoryMock.Setup(m => m.GetById(_toDoTask.Id)).Returns((ToDoTask)null);
            _taskController.CheckToDo(_toDoTask.Id);

            Assert.Equal(TaskController.TASK_NOT_FOUND, _taskController.TempData[TaskController.KEY_ERROR_MESSAGE]);
            _taskRepositoryMock.Verify(m => m.MarkAsDone(_toDoTask), Times.Never());
        }

        [Fact]
        public void ShouldCheckToDoShowErrorMessageWhenRepoReturnsFalls()
        {
            _taskRepositoryMock.Setup(m => m.GetById(_toDoTask.Id)).Returns(_toDoTask);
            _taskRepositoryMock.Setup(m => m.MarkAsDone(_toDoTask)).Returns(false);
            _taskController.CheckToDo(_toDoTask.Id);

            Assert.Equal(TaskController.ERROR_SAVING, _taskController.TempData[TaskController.KEY_ERROR_MESSAGE]);
            _taskRepositoryMock.Verify(m => m.MarkAsDone(_toDoTask), Times.Once());
        }



    }
}
