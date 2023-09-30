using Acibitah.Data.Repositories;
using Acibitah.Data.Repositories.Interfaces;
using Acibitah.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Acibitah.Controllers
{
    public class HomeController : BaseController
    {
        

        private HomeViewModel _homeViewModel;
        private IHabitRepository _habitRepository; 
        private IDailyRepository _dailyRepository;
        private ITaskRepository _taskRepository;

        public HomeController(IHabitRepository habitRepository, IDailyRepository dailyRepository, ITaskRepository taskRepository)
        {
            _homeViewModel = new HomeViewModel();
            _habitRepository = habitRepository;
            _dailyRepository = dailyRepository;
            _taskRepository = taskRepository; 
        }

        public IActionResult Index()
        {
            _homeViewModel.Habits = _habitRepository.GetAll(); 
            _homeViewModel.Dailies = _dailyRepository.GetAll();
            _homeViewModel.ToDos = _taskRepository.GetAll();

            if(  _homeViewModel.Habits == null || _homeViewModel.Dailies == null  || _homeViewModel.ToDos == null)
            {
                TempData[KEY_ERROR_MESSAGE] = ERROR_RETRIEVING; 
            }
            return View(_homeViewModel);
        }

        [HttpPost]
        public IActionResult QuickAddTaskWithTags(HomeViewModel viewModel)
        {
            if (viewModel == null || viewModel.ToDo.Title == null)
            {
                TempData[KEY_ERROR_MESSAGE] = ERROR_SAVING;
            } else
            {
                bool result = _taskRepository.Save(viewModel.ToDo);
                if(!result)
                {
                    TempData[KEY_ERROR_MESSAGE] = ERROR_SAVING;
                } else
                {
                    TempData[KEY_SUCCESS_MESSAGE] = SUCCESS_SAVED;
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult QuickAddHabitWithTags(HomeViewModel viewModel)
        {
            if (viewModel == null || viewModel.Habit.Name == null)
            {
                TempData[KEY_ERROR_MESSAGE] = ERROR_SAVING;
            }
            else
            {
                bool result = _habitRepository.Save(viewModel.Habit);
                if (!result)
                {
                    TempData[KEY_ERROR_MESSAGE] = ERROR_SAVING;
                }
                else
                {
                    TempData[KEY_SUCCESS_MESSAGE] = SUCCESS_SAVED;
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult QuickAddDaily(HomeViewModel viewModel)
        {
            if (viewModel == null || viewModel.Daily.Name == null)
            {
                TempData[KEY_ERROR_MESSAGE] = ERROR_SAVING;
            }
            else
            {
                bool result = _dailyRepository.Save(viewModel.Daily);
                if (!result)
                {
                    TempData[KEY_ERROR_MESSAGE] = ERROR_SAVING;
                }
                else
                {
                    TempData[KEY_SUCCESS_MESSAGE] = SUCCESS_SAVED;
                }
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}