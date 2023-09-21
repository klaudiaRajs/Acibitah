using Acibitah.Data.Repositories.Interfaces;
using Acibitah.Tests.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
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
        public void IndexTest()
        {
            Assert.True(false, "This test needs an implementation");
        }
    }
}