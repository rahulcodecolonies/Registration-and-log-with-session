using Microsoft.AspNetCore.Mvc;
using Registration.Data;
using Registration.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Registration.Controllers
{
    public class AccountController : Controller
    {
        //private IAccountService AccountService;
        //public AccountController(IAccountService accountService)
        //{
        //    AccountService = accountService;
        //}
        public IActionResult Index()
        {
            return View();
        }
        private readonly ApplicationDbContext _dbcontext;
        public AccountController(ApplicationDbContext context)
        {
            _dbcontext = context;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                _dbcontext.Account.Add(user);
                _dbcontext.SaveChanges();
                return RedirectToAction("Login", "Account");
            }
            return View(user);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User model)
        {
            if (ModelState.IsValid)
            {
                var user = _dbcontext.Account.FirstOrDefault(u => u.Email == model.Email);
                if (user != null && user.Password == model.Password)
                {
                    HttpContext.Session.SetString("Email", model.Email);
                    return RedirectToAction("Welcome", "Account");
                }
                else
                {
                    ViewBag.Message = "Invalid";
                    return View();
                }


            }
            return View();
        }

        [SessionCheck]
        public IActionResult Welcome()
        {
            ViewBag.Message = "success";
            ViewBag.Message2 = HttpContext.Session.GetString("Email");
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";
            return View();
        }
        public IActionResult Logout()
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
