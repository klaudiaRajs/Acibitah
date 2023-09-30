using Acibitah.Data.Repositories;
using Acibitah.Data.Repositories.Interfaces;
using Acibitah.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Acibitah.Controllers
{
    public class TaskController : BaseController
    {
        public const string TASK_NOT_FOUND = "Task not found, please try again later.";
        public const string NEW_TASK_CREATED = "New task created.";
        public const string TASK_NOT_CREATED = "Problem with creating the task. Try fixing some data or try again later.";
        public const string REMOVING_PROBLEM = "Problem with removing task. Try again later. ";
        public const string ERROR_TASK_NOT_SAVED = "Problem with saving the task";
        public const string ERROR_VALUES_NOT_CORRECT = "Provided values are not correct.";
        private ITaskRepository _taskRepository;
        private TaskViewModel _taskViewModel;
        private IHabitRepository _habitRepository;
        private IDailyRepository _dailyRepository;

        public TaskController(ITaskRepository taskRepository, IHabitRepository habitRepository, IDailyRepository dailyRepository)
        {
            _taskRepository = taskRepository;
            _taskViewModel = new TaskViewModel();
            _taskViewModel.Tasks = _taskRepository.GetAll().ToList();
            _habitRepository = habitRepository;
            _dailyRepository = dailyRepository;

        }

        [HttpGet]
        public IActionResult Index()
        {
            _taskViewModel.Tasks = _taskRepository.GetAll().ToList();
            return View(_taskViewModel);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            _taskViewModel.ToDo = _taskRepository.GetById(id);
            if( _taskViewModel.ToDo == null)
            {
                TempData[KEY_ERROR_MESSAGE] = TASK_NOT_FOUND;
                return RedirectToAction("Index");
            }

            _taskViewModel.Subtasks = _taskRepository.GetSubtasksByTask(_taskViewModel.ToDo);
            return View(_taskViewModel); 
        }

        [HttpPost]
        public IActionResult Index(ToDoTask task)
        {
            var result = _taskRepository.Save(task);
            if( result) 
            {
                TempData[KEY_SUCCESS_MESSAGE] = NEW_TASK_CREATED;
                return RedirectToAction("Index");
            } else
            {
                TempData[KEY_ERROR_MESSAGE] = TASK_NOT_CREATED;
                _taskViewModel.ToDo = task; 
                return View(_taskViewModel);
            }
        }
        public IActionResult Remove(int id)
        {
            ToDoTask? taskToBeRemoved = _taskRepository.GetById(id);
            if( taskToBeRemoved == null)
            {
                TempData[KEY_ERROR_MESSAGE] = TASK_NOT_FOUND;
            } else
            {
                var result = _taskRepository.Remove(taskToBeRemoved);
                if( !result )
                {
                    TempData[KEY_ERROR_MESSAGE] = REMOVING_PROBLEM;
                } else
                {
                    TempData[KEY_SUCCESS_MESSAGE] = SUCCESSFULLY_DELETED;
                }
            }

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            _taskViewModel.ToDo = _taskRepository.GetById(id);
            if(  _taskViewModel.ToDo == null )
            {
                TempData[KEY_ERROR_MESSAGE] = TASK_NOT_FOUND; 
            }
            return View("Index", _taskViewModel);
        }

        [HttpPost]
        public IActionResult Edit(ToDoTask task)
        {
            _taskViewModel.ToDo = _taskRepository.GetById(task.Id);
            if( _taskViewModel.ToDo == null)
            {
                TempData[KEY_ERROR_MESSAGE] = TASK_NOT_FOUND; 
            } else {
                var result = _taskRepository.Update(task);
                if (!result)
                {
                    TempData[KEY_ERROR_MESSAGE] = ERROR_TASK_NOT_SAVED;
                    return View(_taskViewModel);
                } else
                {
                    TempData[KEY_SUCCESS_MESSAGE] = SUCCESS_SAVED; 
                }
            }
            
            _taskViewModel.ToDo = new ToDoTask();
            return RedirectToAction("Index");
        }

        public IActionResult IncreaseStreaks(int id, bool positive, bool negative)
        {
            Habit? habit = _habitRepository.GetById(id);
            if ( habit == null )
            {
                TempData[KEY_ERROR_MESSAGE] = TASK_NOT_FOUND;
                return RedirectToAction("Index", "Home");
            }
            habit.StreakPositive = positive ? ++habit.StreakPositive : habit.StreakPositive; 
            habit.StreakNegative = negative ? ++habit.StreakNegative : habit.StreakNegative;
            var result = _habitRepository.IncreaseStreak(habit);
            if( !result)
            {
                TempData[KEY_ERROR_MESSAGE] = ERROR_SAVING; 
            } else
            {
                TempData[KEY_SUCCESS_MESSAGE] = SUCCESS_SAVED; 
            }
            return RedirectToAction("Index", "Home");
        }

        public void CheckDaily(int id)
        {
            Daily daily = _dailyRepository.GetById(id); 
            if ( daily == null )
            {
                TempData[KEY_ERROR_MESSAGE] = TASK_NOT_FOUND;
                return; 
            }
            var result =_dailyRepository.MarkAsDone(daily); 
            if( !result )
            {
                TempData[KEY_ERROR_MESSAGE] = ERROR_SAVING;
            }
        }

        public void CheckToDo(int id)
        {
            ToDoTask todo = _taskRepository.GetById(id);
            if (todo == null)
            {
                TempData[KEY_ERROR_MESSAGE] = TASK_NOT_FOUND;
                return;
            }
            var result = _taskRepository.MarkAsDone(todo);
            if (!result)
            {
                TempData[KEY_ERROR_MESSAGE] = ERROR_SAVING;
            }
        }
    }
}
