using Portfolio_Management_Application.DTO;
using Portfolio_Management_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Portfolio_Management_Application.Controllers
{
    public class LoginController : Controller
    {
        AddDbContext dbContext = new AddDbContext();
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginUserDTO dto)
        {
            if (dto == null ||
                string.IsNullOrEmpty(dto.Email) ||
                string.IsNullOrEmpty(dto.Password))
            {
                ViewBag.ErrorMsg = "Please fill out all details";
                return View("Index", dto);
            }

            var user = dbContext.User
                .FirstOrDefault(u => u.Email == dto.Email);

            if (user == null)
            {
                ViewBag.ErrorMsg = "Invalid email or password";
                return View("Index", dto);
            }

            if (user.Password != dto.Password)
            {
                ViewBag.ErrorMsg = "Invalid Password";
                return View("Index", dto);
            }


            string role = user.roleId == 3 ? "Admin" : "User";


            FormsAuthenticationTicket ticket =
                new FormsAuthenticationTicket(
                    1,
                    user.Email,
                    DateTime.Now,
                    DateTime.Now.AddHours(1),
                    false,
                    role
                );

            string encryptedTicket =
                FormsAuthentication.Encrypt(ticket);

            Response.Cookies.Add(
                new HttpCookie(
                    FormsAuthentication.FormsCookieName,
                    encryptedTicket
                )
            );

            TempData["SucessMsg"] = "Logged in Successfully";

            return RedirectToAction("About", "Home");
        }
    }
}