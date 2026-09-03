using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Portfolio_Management_Application.Models
{
    public class User
    {
        [Key]
        public int Id { get;set; }
        public string Name { get;set; }
        public string Email { get;set; }
        public string Password { get;set; }
        public string ProfileImg { get; set; }
        public string Desgination { get; set; }

        public int roleId { get; set; }
        public Role Role { get; set; }
    }
}