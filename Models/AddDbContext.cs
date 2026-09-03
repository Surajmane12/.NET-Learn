using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Portfolio_Management_Application.Models
{
    public class AddDbContext:DbContext
    {
        public DbSet<User> User { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<Projects> Projects { get; set; } 
    }
}