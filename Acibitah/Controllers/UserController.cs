
using Acibitah.Data.Repositories.Interfaces;
using Acibitah.Models;
using Microsoft.AspNetCore.Mvc;

namespace Acibitah.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserViewModel viewModel)
        {
            //TODO manage the result 
            var result = _userRepository.Save(viewModel.User, viewModel.Password);
            return RedirectToAction("Index", "Task");
        }
        public IActionResult LogIn()
        {
            //TODO manage the result 
            return View(); 
        }
        [HttpPost]
        public IActionResult Login(UserViewModel viewModel)
        {
            try
            {            
                //TODO manage the result 
                var user = _userRepository.LogIn(viewModel.User.Login, viewModel.Password);
                return RedirectToAction("Index", "Task");

            } catch(Exception e)
            {
                return View();
            }            
        }
    }
}
