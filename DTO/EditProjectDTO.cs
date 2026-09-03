using Portfolio_Management_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portfolio_Management_Application.DTO
{
    public class EditProjectDTO
    {
       public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Technology { get; set; }
        public float Cost { get; set; }
        public string GithubUrl { get; set; }
        public string LiveUrl { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public int userId { get; set; }
        public User User { get; set; }
    }
}
    
