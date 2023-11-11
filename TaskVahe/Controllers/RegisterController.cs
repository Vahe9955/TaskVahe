using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskVahe.Data;
using TaskVahe.Models;

namespace TaskVahe.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationContext _context;

        public RegisterController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult SignUpAsync()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SignInAsync()
        {
            return View();
        }

        [HttpPost]
        [ActionName("logout")]
        public IActionResult LogoutAsync()
        {
            return RedirectToAction("Index", "Home");
        }



        [HttpPost]
        [ActionName("createUser")]
        public async Task<IActionResult> CreateUser([FromForm] UserRegistrationModel model)
        {
            if (model.Address == null)
            {
                ViewBag.Error = true;
                return SignUpAsync();
            }
            var addresSTR = model.Address.ToString().ToLower();
            addresSTR = addresSTR.Replace(".", " ");
            addresSTR = addresSTR.Replace("avenue", "AVE");
            addresSTR = addresSTR.Replace("street", "ST");
            addresSTR = addresSTR.Replace("no", "N");
            addresSTR = addresSTR.Replace("#", "");
            addresSTR = addresSTR.Replace("boulevard", "BLVD");
            addresSTR = addresSTR.ToUpper();

            var user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                FullName = model.FullName,
                Email = model.Email,
                Address = addresSTR,
                DateOfBirth = model.DateOfBirth,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                PasswordConfirmed = true,
                UserId = new Guid()
            };


            await _context.applicationUsers.AddAsync(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ActionName("getuser")]
        public async Task<IActionResult> LoginAsync([FromForm] UserRegistrationModel model)
        {
            if (model.PhoneNumber == null || model.Password == null)
            {
                ViewBag.Message = true;
                return RedirectToAction("SignIn","Register");
            }

            var user = await _context.applicationUsers
                .Where(x => x.PhoneNumber == model.PhoneNumber && x.Password == model.Password)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                ViewBag.Message1 = true;
                return RedirectToAction("SignIn", "Register");
            }

            // Redirect to the "Profile" action with user ID as a parameter
            return RedirectToAction("Profile", new { id = user.Id });
        }

        [HttpGet]
        [ActionName("profile")]
        public async Task<ActionResult> Profile(int id)
        {
            var user = await _context.applicationUsers.FindAsync(id);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }
    }
}