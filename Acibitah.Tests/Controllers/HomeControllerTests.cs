using Acibitah.Models;
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
            Assert.Equal(null, GetTempDataMessage(TaskController.KEY_ERROR_MESSAGE, _homeController.TempData));
        }

        [Fact]
        public void IndexErrorOnReturningHabitsTest()
        {

        }

        [Fact]
        public void QuickAddTaskWithTagsCorrectlySavedToDoTest()
        {

        }
    }
}