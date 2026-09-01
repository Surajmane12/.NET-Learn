using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.DTO;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class AuthController(AppDbContext dbContext):Controller
    {
        //private readonly AppDbContext dbContext;
        //public AuthController(AppDbContext dbContext)
        //{
        //    this.dbContext = dbContext;
        //}
        public IActionResult Login()
        {

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> CreateUser(UserDTO dto)
        {
            if (dto is null || string.IsNullOrEmpty(dto.Name) || string.IsNullOrEmpty(dto.Email))
            {
                ViewBag.ErrorMessage = "Please fill all the details";
                return View("Register");
            }
            if(dto.Password!=dto.ConfirmPassword)
            {
                ViewBag.ErrorMessage = "Password Mismatch";
                return View("Register");
             
            }
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(u=>u.Email==dto.Email);
            if(existingUser!= null)
            {
                ViewBag.ErrorMessage = "User Email Already Exists!!";
                return View("Register");
            }
            else
            {
                var newUser = new User
                {
                    Name=dto.Name,
                    Email = dto.Email,
                    Password = dto.Password

                };
                dbContext.Users.Add(newUser);
                await dbContext.SaveChangesAsync();
            };
            TempData["SuccessMessage"] = "User Registered Successfully!!";
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> UserLogin(LoginUserDTO dto)
        {
            if (dto is null || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
            {
                ViewBag.ErrorMessage = "Please fill out the details";
                return View("Login");
            }
            var ExistUser = await dbContext.Users.FirstOrDefaultAsync(u=>u.Email==dto.Email);
            if (ExistUser == null) {
                ViewBag.ErrorMessage = "Email not Exists. Please Check again";
                return View("Login");
                
            }
            if (ExistUser.Password != dto.Password)
            {
                ViewBag.ErrorMessage = "Incorrect Password";
                return View("Login");
            }
            TempData["successMsg"] = "Logged in  Successful!!";
            return RedirectToAction("Index","DashBoard");
        }

    }
}
