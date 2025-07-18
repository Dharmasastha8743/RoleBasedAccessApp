using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Role_Based_Auth_App.Helper;
using System.Reflection;
using System.Security.Claims;

namespace Role_Based_Auth_App.Controllers
{
    public class AuthenticationController : Controller
    {
        //private static readonly Dictionary<string, (string Password, string Role)> _users = new()
        //{
        //    { "admin", ("123", "Admin") },
        //    { "manager", ("123", "Manager") },
        //    { "employee", ("123", "Employee") }
        //};


        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = UserStore.ValidateUser(username, password);
            if (user != null)
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role)
        };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View(); 
        }

        public async Task<IActionResult> Logout()
        {
            //await HttpContext.SignOutAsync("RoleBasedCookieAuth");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied() => View();

        public IActionResult Index()
        {
            return View();
        }
    }
}
