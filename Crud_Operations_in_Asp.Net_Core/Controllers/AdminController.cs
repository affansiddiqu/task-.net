using Crud_Operations_in_Asp.Net_Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Crud_Operations_in_Asp.Net_Core.Controllers
{
    public class AdminController : Controller
    {
        private readonly AssignmentDbContext context;
        public AdminController(AssignmentDbContext _context)
        {
            this.context = _context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Signup(TblUser user)
        {
            var checkExictingUser = context.TblUsers.FirstOrDefault(x => x.Email == user.Email);

            if (checkExictingUser != null)
            {
                ViewBag.msg = "User Already Exists";
                return View();
            }

            var hasher = new PasswordHasher<string>();
            user.Password = hasher.HashPassword(user.Email, user.Password);
            context.TblUsers.Add(user);
            context.SaveChanges();
            return RedirectToAction("Login");
        }


        [HttpGet]
        public IActionResult Login()
        {
            if(User.Identity.IsAuthenticated)
            {
				return RedirectToAction("Index","Product");
			}

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(TblUser user)
        {
            bool IsAuthenticated = false;
            string controller = "";
            ClaimsIdentity identity = null;

            var checkUser = context.TblUsers.FirstOrDefault(u1 => u1.Email == user.Email);

            if (checkUser != null)
            {
                var hasher = new PasswordHasher<string>();
                var verifyPass = hasher.VerifyHashedPassword(user.Email, checkUser.Password, user.Password);
                if (verifyPass == PasswordVerificationResult.Success && checkUser.Role == 1)
                {
                  identity = new ClaimsIdentity(new[]
                  {
                    new Claim(ClaimTypes.Name ,checkUser.Username),
                    new Claim(ClaimTypes.Role,"Admin"),
                  },CookieAuthenticationDefaults.AuthenticationScheme);

                    HttpContext.Session.SetString("email", checkUser.Email);
                    HttpContext.Session.SetString("username", checkUser.Username);

                    IsAuthenticated = true;
                    controller = "Admin";

                }
                else if (verifyPass == PasswordVerificationResult.Success && checkUser.Role == 2)
                {
                    IsAuthenticated = true;
                    identity = new ClaimsIdentity(new[]
                   {
                    new Claim(ClaimTypes.Name ,checkUser.Username),
                    new Claim(ClaimTypes.Role ,"User"),
        }
                   , CookieAuthenticationDefaults.AuthenticationScheme);
                    controller = "Home";
                }
                else
                {
                    IsAuthenticated = false;

                }
                if (IsAuthenticated)
                {
                    var principal = new ClaimsPrincipal(identity);

                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", controller);
                }

                else
                {
                    ViewBag.msg = "Invalid Credentials";
                    return View();
                }

            }
            else
            {
                ViewBag.msg = "User not found";
                return View();
            }
        }
        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }






        //[HttpGet]
        //public IActionResult Login()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Login(string email, string password)
        //{
        //    bool isAuthenticated = false;
        //    bool isAdmin = false;
        //    ClaimsIdentity identity = null;

        //    if (email == "admin@gmail.com" && password == "admin123")
        //    {
        //        identity = new ClaimsIdentity(new[] {
        //            new Claim(ClaimTypes.Name,"Talha" ),
        //            new Claim(ClaimTypes.Role, "Admin")
        //        }, CookieAuthenticationDefaults.AuthenticationScheme);

        //        isAdmin = true;
        //        isAuthenticated = true;

        //    }else if(email == "user@gmail.com" && password == "user123"){
        //        identity = new ClaimsIdentity(new[] {
        //            new Claim(ClaimTypes.Name,"User1" ),
        //            new Claim(ClaimTypes.Role, "User")
        //        }, CookieAuthenticationDefaults.AuthenticationScheme);

        //        isAdmin = false;
        //        isAuthenticated = true;
        //    }
        //    if (isAuthenticated && isAdmin)
        //    {
        //        var principal = new ClaimsPrincipal(identity);

        //        var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        //        return RedirectToAction("Index", "Admin");

        //    }
        //    else if (isAuthenticated)
        //    {
        //        var principal = new ClaimsPrincipal(identity);

        //        var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        //        return RedirectToAction("Index", "Home");
        //    }
        //    else
        //    {
        //        ViewBag.msg = "Invalid credentials";
        //        return View();
        //    }
        //}
        //public IActionResult Logout()
        //{
        //    var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        //    return RedirectToAction("Login", "Admin");
        //}
    }
}
