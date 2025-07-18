using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Role_Based_Auth_App.Helper;
using Role_Based_Auth_App.Models;
using System.Reflection;



namespace Role_Based_Auth_App.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public IActionResult Users(string username)
        {
            var users = UserStore.GetUsers();

            
            var userToEdit = users.FirstOrDefault(u => u.Name == username);

            ViewBag.EditUser = userToEdit;
            return View(users);
           
            //var userList = UserStore.GetUsers();
            //return View(userList);
        }

        [HttpPost]
        public IActionResult Add(UserModel model)
        {

            string status = UserStore.AddUser(model);
            TempData["StatusMessage"] = status;
            return RedirectToAction("Users");
        }

        [HttpPost]
        public IActionResult Update(UserModel model)
        {
            string status = UserStore.UpdateUser(model);
            ViewBag.EditUsername = model.Name;
            TempData["StatusMessage"] = status;
            return RedirectToAction("Users");
        }
        public IActionResult Remove(string userName)
        {
            string status = UserStore.RemoveUser(userName);
            TempData["StatusMessage"] = status;
            return RedirectToAction("Users");
        }
    }
}
