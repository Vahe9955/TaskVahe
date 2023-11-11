using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using TaskVahe.Data;
using TaskVahe.Models;

namespace TaskVahe.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationContext _context;
        public DashboardController(ApplicationContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _context.applicationUsers.ToListAsync();
            return View(users);
        }

        [HttpPost]
        [ActionName("remove")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.applicationUsers.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return RedirectToAction("Incorrect Data");
            }
            _context.applicationUsers.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Dashboard");
        }

        [HttpPost]
        [ActionName("update")]
        public async Task<IActionResult> UpdateItem([FromForm] UserUpdateModel model)
        {
            var user = await _context.applicationUsers.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (string.IsNullOrEmpty(model.FirstName) && string.IsNullOrEmpty(model.LastName) && string.IsNullOrEmpty(model.Address))
            {
                ViewBag.Massage = "Wrong Data";
            }
            if (model.DateOfBirth != DateTime.MinValue)
            {
                user.DateOfBirth = model.DateOfBirth;
            }
            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.Address = model.Address;
                user.PhoneNumber = model.PhoneNumber;
                await _context.SaveChangesAsync();
                ViewBag.Message = "Update successfully comleted.";
            }
            else
            {
                ViewBag.Message = "Wrong Data";
            }

            var users = await _context.applicationUsers.ToListAsync();
            return View("Index", users);
        }



    }
}
