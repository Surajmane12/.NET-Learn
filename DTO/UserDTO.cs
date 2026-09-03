using Portfolio_Management_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portfolio_Management_Application.DTO
{
    public class UserDTO
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ProfileImg { get; set; }
        public string Desgination { get; set; }
        public int roleId { get; set; }
        public string Role { get; set; }
    }
}