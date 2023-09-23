using Acibitah.Models;
using Acibitah.Tests;
using Acibitah.Tests.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;

namespace Acibitah.Controllers.Tests
{
    public class HomeControllerTests : BaseTest
    {
        private HomeController _homeController;

        public HomeControllerTests()
        {
            _homeController = new HomeController(
                _habitRepositoryMock.Object,
                _dailyRepositoryMock.Object,
                _taskRepositoryMock.Object
            );
            ITempDataProvider tempDataProvider = Mock.Of<ITempDataProvider>();
            TempDataDictionaryFactory tempDataDictionaryFactory = new TempDataDictionaryFactory(tempDataProvider);
            ITempDataDictionary tempData = tempDataDictionaryFactory.GetTempData(new DefaultHttpContext());
            _homeController.TempData = tempData;
        }

        [Fact]
        public void IndexCorrectTest()
        {
            _habitRepositoryMock.Setup(method => method.GetAll()).Returns(_habits);
            _dailyRepositoryMock.Setup(method => method.GetAll()).Returns(_dailies);
            _taskRepositoryMock.Setup(method => method.GetAll()).Returns(_activeTasks);

            var result = _homeController.Index();
            Assert.Equal(typeof(ViewResult), result.GetType());
            ViewResult iActionResult = (ViewResult)result;
            HomeViewModel model = (HomeViewModel)iActionResult.Model;
            Assert.Equal(_habits.Count(), model.Habits.Count());
            Assert.Equal(_dailies.Count(), model.Dailies.Count());
            Assert.Equal(_activeTasks.Count(), model.ToDos.Count());
            Assert.Equal(0, model.ToDo.Id);
            Assert.Equal(null, GetTempDataMessage(HomeController.KEY_ERROR_MESSAGE, _homeController.TempData));
        }


        [Theory]
        [MemberData(nameof(TestDataGenerator.GetPersonFromDataGenerator), MemberType = typeof(TestDataGenerator))]
        public void IndexErrorOnReturningHabitsTest(List<Habit> habits, List<Daily> dailies, List<ToDoTask> toDoTasks)
        {
            _habitRepositoryMock.Setup(method => method.GetAll()).Returns(habits);
            _dailyRepositoryMock.Setup(method => method.GetAll()).Returns(dailies);
            _taskRepositoryMock.Setup(method => method.GetAll()).Returns(toDoTasks);

            var result = _homeController.Index();
            ViewResult iActionResult = (ViewResult)result;
            HomeViewModel model = (HomeViewModel)iActionResult.Model;
            Assert.Equal(HomeController.KEY_ERROR_MESSAGE, GetTempDataMessage(HomeController.KEY_ERROR_MESSAGE, _homeController.TempData));
            Assert.Equal(BaseController.ERROR_RETRIEVING, _homeController.TempData[HomeController.KEY_ERROR_MESSAGE]);
        }

        [Fact]
        public void QuickAddTaskWithTagsNoViewModelPassedTest()
        {
            var result = _homeController.QuickAddTaskWithTags(null);
            Assert.Equal(typeof(RedirectToActionResult), result.GetType());
            Assert.Equal(HomeController.KEY_ERROR_MESSAGE, GetTempDataMessage(HomeController.KEY_ERROR_MESSAGE, _homeController.TempData));
            Assert.Equal(BaseController.ERROR_SAVING, _homeController.TempData[HomeController.KEY_ERROR_MESSAGE]);
        }

        [Fact]
        public void QuickAddTaskWithTagsNoToDoInViewModelTest()
        {
            HomeViewModel model = new HomeViewModel();
            model.ToDo = new ToDoTask() { Title = null }; 
            var result = _homeController.QuickAddTaskWithTags(model);
            Assert.Equal(typeof(RedirectToActionResult), result.GetType());
            Assert.Equal(HomeController.KEY_ERROR_MESSAGE, GetTempDataMessage(HomeController.KEY_ERROR_MESSAGE, _homeController.TempData));
            Assert.Equal(BaseController.ERROR_SAVING, _homeController.TempData[HomeController.KEY_ERROR_MESSAGE]);
        }

        [Fact]
        public void QuickAddTaskWithTagsProblemWithSavingFromDbTest()
        {
            HomeViewModel model = new HomeViewModel();
            model.ToDo = new ToDoTask() { Title = "TestTitle" };
            _taskRepositoryMock.Setup(method => method.Save(model.ToDo)).Returns(false); 
            var result = _homeController.QuickAddTaskWithTags(model);
            Assert.Equal(typeof(RedirectToActionResult), result.GetType());
            Assert.Equal(HomeController.KEY_ERROR_MESSAGE, GetTempDataMessage(HomeController.KEY_ERROR_MESSAGE, _homeController.TempData));
            Assert.Equal(BaseController.ERROR_SAVING, _homeController.TempData[HomeController.KEY_ERROR_MESSAGE]);
        }

        [Fact]
        public void QuickAddTaskWithTagsSavedCorrectlyTest()
        {
            HomeViewModel model = new HomeViewModel();
            model.ToDo = new ToDoTask() { Title = "TestTitle" };
            _taskRepositoryMock.Setup(method => method.Save(model.ToDo)).Returns(true);
            var result = _homeController.QuickAddTaskWithTags(model);
            Assert.Equal(typeof(RedirectToActionResult), result.GetType());
            Assert.Equal(HomeController.KEY_SUCCESS_MESSAGE, GetTempDataMessage(HomeController.KEY_SUCCESS_MESSAGE, _homeController.TempData));
            Assert.Equal(null, _homeController.TempData[HomeController.KEY_ERROR_MESSAGE]);
            Assert.Equal(BaseController.SUCCESS_SAVED, _homeController.TempData[HomeController.KEY_SUCCESS_MESSAGE]);
        }

        [Fact]
        public void QuickAddHabitWithTagsNoViewModelPassedTest()
        {
            var result = _homeController.QuickAddHabitWithTags(null);
            Assert.Equal(typeof(RedirectToActionResult), result.GetType());
            Assert.Equal(HomeController.KEY_ERROR_MESSAGE, GetTempDataMessage(HomeController.KEY_ERROR_MESSAGE, _homeController.TempData));
            Assert.Equal(BaseController.ERROR_SAVING, _homeController.TempData[HomeController.KEY_ERROR_MESSAGE]);
        }

        [Fact]
        public void QuickAddHabitWithTagsNoToDoInViewModelTest()
        {
            HomeViewModel model = new HomeViewModel();
            model.Habit = new Habit() { Name = null };
            var result = _homeController.QuickAddHabitWithTags(model);
            Assert.Equal(typeof(RedirectToActionResult), result.GetType());
            Assert.Equal(HomeController.KEY_ERROR_MESSAGE, GetTempDataMessage(HomeController.KEY_ERROR_MESSAGE, _homeController.TempData));
            Assert.Equal(BaseController.ERROR_SAVING, _homeController.TempData[HomeController.KEY_ERROR_MESSAGE]);
        }

        [Fact]
        public void QuickAddHabitWithTagsProblemWithSavingFromDbTest()
        {
            HomeViewModel model = new HomeViewModel();
            model.Habit = new Habit() { Name = "TestName" };
            _habitRepositoryMock.Setup(method => method.Save(model.Habit)).Returns(false);
            var result = _homeController.QuickAddHabitWithTags(model);
            Assert.Equal(typeof(RedirectToActionResult), result.GetType());
            Assert.Equal(HomeController.KEY_ERROR_MESSAGE, GetTempDataMessage(HomeController.KEY_ERROR_MESSAGE, _homeController.TempData));
            Assert.Equal(BaseController.ERROR_SAVING, _homeController.TempData[HomeController.KEY_ERROR_MESSAGE]);
        }

        [Fact]
        public void QuickAddHabitWithTagsSavedCorrectlyTest()
        {
            HomeViewModel model = new HomeViewModel();
            model.Habit = new Habit() { Name = "TestName" };
            _habitRepositoryMock.Setup(method => method.Save(model.Habit)).Returns(true);
            var result = _homeController.QuickAddHabitWithTags(model);
            Assert.Equal(typeof(RedirectToActionResult), result.GetType());
            Assert.Equal(HomeController.KEY_SUCCESS_MESSAGE, GetTempDataMessage(HomeController.KEY_SUCCESS_MESSAGE, _homeController.TempData));
            Assert.Equal(null, _homeController.TempData[HomeController.KEY_ERROR_MESSAGE]);
            Assert.Equal(BaseController.SUCCESS_SAVED, _homeController.TempData[HomeController.KEY_SUCCESS_MESSAGE]);
        }
    }
}