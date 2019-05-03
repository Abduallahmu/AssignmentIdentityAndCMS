using System.Linq;
using System.Threading.Tasks;
using IdentityAndCms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAndCms.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(SignInManager<User> signInManager,
                                   UserManager<User> userManager,
                                 RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignIn(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var SingInResult = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, false);

                switch (SingInResult.ToString())
                {
                    case "Succeeded":
                        return RedirectToAction("Index", "Home");

                    case "Failed":
                        ViewBag.msg = "Failed - Username of/and Password is incorrect";
                        break;
                    case "Lockedout":
                        ViewBag.msg = "Locked Out";
                        break;
                    default:
                        ViewBag.msg = SingInResult.ToString();
                        break;
                }
            }
            return View(loginViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserVM createuser)
        {
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    UserName = createuser.UserName,
                    Email = createuser.Email
                };
                var result = await _userManager.CreateAsync(user, createuser.Password);

                if (result.Succeeded)
                {
                    ViewBag.msg = "User was created";
                    return RedirectToAction("CreateUser");
                }
                else
                {
                    ViewBag.errorlist = result.Errors;
                }
            }

            return View(createuser);
        }

        public IActionResult RoleList()
        {
            return View(_roleManager.Roles.ToList());
        }

        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return View();
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(name));

            if (result.Succeeded)
            {
                return RedirectToAction("RoleList");
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddUserToRole(string role)
        {
            ViewBag.Role = role;
            return View(_userManager.Users.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> AddUserToRoleSave(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.AddToRoleAsync(user, role);

            return RedirectToAction(nameof(RoleList));
        }
    }
}