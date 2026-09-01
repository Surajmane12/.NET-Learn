using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.DTO;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class DashBoardController(AppDbContext dbContext) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var Users = await GetUsers();
            ViewBag.Users = Users;
            return View(Users);
        }

        private async Task<List<User>> GetUsers()
        {
            var UsersData = await dbContext.Users.ToListAsync();          
            return UsersData;
        }
        public async Task<IActionResult> Edit(int Id)
        {
            if (Id == 0)
            {
                ViewBag.ErrorMessage = "Invalid Id";
                return Redirect("DashBoard");
            }
            var UserData = await dbContext.Users.FindAsync(Id);
            if(UserData == null) 
                {
                ViewBag.ErrorMessage = "User Not Found";
                return NotFound();
                }
            var newUserDTO = new UserDTO
            {
                Id = UserData.Id,
                Name = UserData.Name,
                Email = UserData.Email,
                Password = UserData.Password
            };
            return View(newUserDTO);
        }

        public async Task<IActionResult> EditUser(UserDTO dto)
        {
            if(dto==null || string.IsNullOrEmpty(dto.Email)||string.IsNullOrEmpty(dto.Password) || string.IsNullOrEmpty(dto.Name))
            {
                ViewBag.ErrorMessage = "Please fill all details";
                return View(dto);
            }
            var ExistingData = await dbContext.Users.FindAsync(dto.Id);
            ExistingData.Email = dto.Email;
            ExistingData.Password = dto.Password;
            ExistingData.Name = dto.Name;

            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
            
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            if(Id==0)
            {
                ViewBag.ErrorMessage = "Invalid Id";
                return View();
            }
            var user = await dbContext.Users.FindAsync(Id);
            if(user is null)
            {
                ViewBag.ErrorMessage = "User Not Found";
                return NotFound();
            }
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
