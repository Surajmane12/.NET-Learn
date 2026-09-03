using Portfolio_Management_Application.DTO;
using Portfolio_Management_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;
namespace Portfolio_Management_Application.Controllers
{
    public class ProjectController : Controller
    {
        AddDbContext dbContext = new AddDbContext();

        private void LoadUsers()
        {
            ViewBag.Users = dbContext.User.ToList();
        }
       
        private async Task<List<GetProjectDTO>> GetProjects()
        {
            var projects = dbContext.Projects.
                Include(u => u.User).
                Select(u => new GetProjectDTO
                {
                    Id = u.Id,
                    Name = u.Name,
                    Description = u.Description,
                    Cost = u.Cost,
                    LiveUrl = u.LiveUrl,
                    GithubUrl = u.GithubUrl,
                    CreatedDate = u.CreatedDate,
                    Technology = u.Technology,
                    ImageUrl = u.ImageUrl,
                    User = u.User,
                    userId = u.userId
                }).OrderByDescending(u => u.CreatedDate).ToList();


            return projects;
        }
        public async Task<ActionResult> Index()
        {
            List<GetProjectDTO> projects = await GetProjects();
            ViewBag.Projects = projects;
            return View(projects);
        }

        
        public ActionResult Create()
        {
            LoadUsers();
            return View();
        }

        public async Task<ActionResult> CreateProject(CreateProjectDTO dto)
        {
           
            if (dto is null)
            {
                ViewBag.ErrorMsg = "Please Fill All Details!!";
                LoadUsers();
                return View("Create",dto);
            }
            var user = dbContext.User.Find(dto.userId);

            if(user is null)
            {
                LoadUsers();
                ViewBag.ErrorMsg = "User Not Found.Please Select Correct User";
                return View("Create",dto);
            }
            var project = new Projects()
            {
                Name = dto.Name,
                Description = dto.Description,
                Cost = dto.Cost,
                Technology = dto.Technology,
                userId = dto.userId,
                GithubUrl = dto.GithubUrl,
                LiveUrl = dto.LiveUrl,
                ImageUrl = dto.ImageUrl,
                CreatedDate = DateTime.Now
            };
            dbContext.Projects.Add(project);
           await dbContext.SaveChangesAsync();

            return RedirectToAction("Index","Project");
        }

        public ActionResult Edit(int? Id)
        {   
            if(!Id.HasValue)
            {
                return RedirectToAction("Index","Project");
            }
            LoadUsers();

            var project = dbContext.Projects.Find(Id);
            var editproject = new EditProjectDTO()
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                LiveUrl = project.LiveUrl,
                GithubUrl = project.GithubUrl,
                ImageUrl = project.ImageUrl,
                userId = project.userId,
                Technology = project.Technology,
                Cost = project.Cost,
            };
            
            return View(editproject);
        }

        public async Task<ActionResult> EditProject(EditProjectDTO dto)
        {
            if (dto is null)
            {
                return Redirect("Index");
            }
            var project = dbContext.Projects.Find(dto.Id);
            project.Name = dto.Name;
            project.Description = dto.Description;
            project.Cost = dto.Cost;
            project.LiveUrl = dto.LiveUrl;
            project.GithubUrl = dto.GithubUrl;
            project.ImageUrl = dto.ImageUrl;
            project.userId = dto.userId;

           await dbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Project");
        }
        public ActionResult DeleteProject(int? Id)
        {
            if (!Id.HasValue)
            {
                ViewBag.ErrorMsg = "Invalid Request";
                return RedirectToAction("Index","Project");
            }
            var project = dbContext.Projects.Find(Id);
            dbContext.Projects.Remove(project);
            dbContext.SaveChanges();
            return RedirectToAction("Index","Project");
        }


        public  ActionResult Details(int? Id)
        {
            if (!Id.HasValue)
            {
                 ViewBag.ErrorMsg = "Invalid Request";
                return RedirectToAction("Index","Project");
            }
            var project= dbContext.Projects.Include(u=>u.User).Where(u=>u.Id == Id).
                Select(u=>new GetProjectDTO
                {
                    Name = u.Name,
                    Description = u.Description,
                    Cost = u.Cost,
                    Technology = u.Technology,
                    userId = u.userId,
                    GithubUrl = u.GithubUrl,
                    LiveUrl = u.LiveUrl,
                    ImageUrl = u.ImageUrl,
                    User=u.User
                }).FirstOrDefault();
            return View(project);
        }
    }
}