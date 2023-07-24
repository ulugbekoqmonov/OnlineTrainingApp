using Application.CQRS.Users.CreateUserCommand;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class UserController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            await _mediator.Send(command);
            return View("Login");
        }
    }
}
