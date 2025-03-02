using Acibitah.Models;
using Microsoft.AspNetCore.Mvc;

namespace Acibitah.Controllers
{
    public class StatisticsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TaskStatistics(int id)
        {
            var model = new StatisticsViewModel()
            {
                Id = id,
                Items = new List<StatisticItem>()
                {
                    new StatisticItem()
                    {
                        Date = DateTime.Now,
                        FactorName = "Number of positive clicked",
                        Value = 5
                    },
                    new StatisticItem()
                    {
                        Date = DateTime.Now,
                        FactorName = "Number of negative clicked",
                        Value = 10

                    }
                }
            };
            return View(model);
        }
    }
}
