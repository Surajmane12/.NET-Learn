using Portfolio_Management_Application.DTO;
using Portfolio_Management_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Portfolio_Management_Application.Controllers
{
    public class UserController : Controller
    {
        AddDbContext dbContext=new AddDbContext();
        // GET: User

        private async Task<List<UserDTO>> GetUsers()
        {
            var users = dbContext.User.
                 Include(u => u.Role)
                .Select(u => new UserDTO
                {
                    Id = u.Id,
                    Email = u.Email,
                    Name = u.Name,
                    Desgination = u.Desgination,
                    ProfileImg = u.ProfileImg,
                    roleId = u.roleId,
                    Role = u.Role.Name,
                }).ToList();

            return users;
        }
        public async Task<ActionResult> Index()
        {
       
            var users = await GetUsers();

            ViewBag.Roles = dbContext.Role.ToList();
           
            return View(users);
        }

        public async Task<ActionResult> Create(CreateUserDTO dto)
        { if (dto is null || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Name) ||
                string.IsNullOrEmpty(dto.Desgination) || string.IsNullOrEmpty(dto.ProfileImg) || 
                dto.roleId == 0)
            {
               ViewBag.ErrorMsg = "Please fill all fields";
                ViewBag.OpenModal = true;
                var users=await GetUsers();
                ViewBag.CreateUserDTO = dto;
                ViewBag.Roles = dbContext.Role.ToList();
                return View("Index", users);
            }
            var newData = new User()
            {
                Email = dto.Email,
                Name = dto.Name,
                Password=dto.Password,
                Desgination = dto.Desgination,
                roleId = dto.roleId,

            };
            dbContext.User.Add(newData);
            await dbContext.SaveChangesAsync();
            TempData["SuccessMsg"] = "User Created Successfully!!";
            return RedirectToAction("Index","User");
        }
        
        public ActionResult Edit(int? Id)
        {
            if(Id==0 || !Id.HasValue)
            {
                return RedirectToAction("Index");
            }
            var user = dbContext.User.Where(u => u.Id == Id)
                .Select(u => new UpdateUserDTO
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Password=u.Password,
                    Desgination = u.Desgination,
                    ProfileImg = u.ProfileImg,
                    roleId = u.roleId
                }).FirstOrDefault();

            ViewBag.UpdateUserDTO = user;
            ViewBag.Roles=dbContext.Role.ToList();
            
            return View("Edit",user);
        }
        public ActionResult Update(UpdateUserDTO dto)
        {
            
            var u = dbContext.User.Find(dto.Id);

            u.Email = dto.Email;
            u.Name = dto.Name;
            u.Desgination = dto.Desgination;
            u.Password= dto.Password;
            u.roleId = dto.roleId;

            dbContext.SaveChanges();
         
            return RedirectToAction("Index", "User");
        }
        

        public ActionResult Delete(int Id)
        {
            if(Id<=0)
            {
                ViewBag.ErrorMsg = "Invalid ID";
                return RedirectToAction("Index", "User");
            }
            var user=dbContext.User.Find(Id);
            if(user==null)
            {
                ViewBag.ErrorMsg = "User Not Found";
                return RedirectToAction("Index", "User");
            }
            TempData["SuccessMsg"] = "User Deleted Successfully!!";
            dbContext.User.Remove(user);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}