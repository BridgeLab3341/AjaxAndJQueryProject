using AjaxAndJQueryProject.Entity;
using AjaxAndJQueryProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AjaxAndJQueryProject.Controllers
{
    public class UserLoginController : Controller
    {
        private readonly ApplicationContext context;
        public UserLoginController(ApplicationContext context)
        {
            this.context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
        public JsonResult UserList()
        {
            var user = new UserLogin();
            return new JsonResult(user);
        }
        [HttpPost]
        public JsonResult AddUser(UserLogin login,int id)
        {
            var userId=context.AjaxEmployee.FirstOrDefault(x=>x.Id== id);
            var user = new UserLogin()
            {
                Email = login.Email,
                Password = login.Password,
            };
            context.UserLogin.Add(user);
            context.SaveChanges();
            return new JsonResult("Data is Saved");
        }
    }
}
