
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
    }
}
