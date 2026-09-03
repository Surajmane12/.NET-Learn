using Portfolio_Management_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portfolio_Management_Application.Controllers
{
    public class HomeController : Controller
    {
        AddDbContext dbContext=new AddDbContext();
        public ActionResult Index()
        {
            ViewBag.users = dbContext.User.ToList();
            return View();
        }

        //[Authorize(Roles="Admin")]
        public ActionResult About()
        {

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}