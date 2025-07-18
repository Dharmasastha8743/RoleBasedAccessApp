using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Role_Based_Auth_App.Helper;
using Role_Based_Auth_App.Models;

namespace Role_Based_Auth_App.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        public IActionResult MyData()
        {
            string user = User?.Identity?.Name ?? "";

            UserModel userData = UserStore.GetUserByName(user);
            var requests = RequestStore.GetRequestsByUser(user);

            ViewBag.Requests = requests;
            ViewBag.User = userData;
            return View();
        }

        public IActionResult Submit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Submit(string content)
        {
            var user = User.Identity?.Name ?? "";
            var request = new RequestModel
            {
                EmployeeName = user,
                Content = content
            };
            RequestStore.AddRequest(request);

            TempData["Msg"] = "Request submitted!";
            return RedirectToAction("Submit");
        }
    }
}
