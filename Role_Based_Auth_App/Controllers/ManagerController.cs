using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Role_Based_Auth_App.Helper;

namespace Role_Based_Auth_App.Controllers
{

    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        public IActionResult ApproveRequest() => View();

        public IActionResult Requests()
        {
            var list = RequestStore.GetRequests();
            return View(list);
        }

        public IActionResult Approve(int id)
        {
            RequestStore.UpdateStatus(id, "Approved");
            return RedirectToAction("Requests");
        }

        public IActionResult Reject(int id)
        {
            RequestStore.UpdateStatus(id, "Rejected");
            return RedirectToAction("Requests");
        }
    }
}
