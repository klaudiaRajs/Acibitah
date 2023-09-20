﻿using Acibitah.Data.Repositories;
using Acibitah.Data.Repositories.Interfaces;
using Acibitah.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Acibitah.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HomeViewModel _homeViewModel;
        private IHabitRepository _habitRepository; 
        private IDailyRepository _dailyRepository;
        private ITaskRepository _taskRepository;

        public HomeController(ILogger<HomeController> logger, IHabitRepository habitRepository, IDailyRepository dailyRepository, ITaskRepository taskRepository)
        {
            _logger = logger;
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
            return View(_homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}