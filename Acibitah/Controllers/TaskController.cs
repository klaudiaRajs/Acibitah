using Acibitah.Data.Repositories.Interfaces;
using Acibitah.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.DataClassification;

namespace Acibitah.Controllers
{
    public class TaskController : Controller
    {
        private ITaskRepository _taskRepository;
        private TaskViewModel _taskViewModel;
        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
            _taskViewModel = new TaskViewModel();
            _taskViewModel.Tasks = _taskRepository.GetAll().ToList();
        }

        [HttpGet]
        public IActionResult Index()
        {
            _taskViewModel.Tasks = _taskRepository.GetAll().ToList();
            return View(_taskViewModel);
        }

        [HttpPost]
        public IActionResult Index(ToDoTask task)
        {
            //TODO Opracuj result
            var result = _taskRepository.Save(task);
            return RedirectToAction("Index");
        }
        public IActionResult Remove(int id)
        {
            //TODO opracuj result
            var result = _taskRepository.Remove(id);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            _taskViewModel.ToDo = _taskRepository.GetById(id);
            return View("Index", _taskViewModel);
        }
        [HttpPost]
        public IActionResult Edit(ToDoTask task)
        {
            var result = _taskRepository.Update(task);
            _taskViewModel.ToDo = new ToDoTask();
            return RedirectToAction("Index");
        }
    }
}
