using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data
{
    public class BlogDBContext : IdentityDbContext<IdentityUser>
    {
        public BlogDBContext()
        {
            
        }
        public BlogDBContext(DbContextOptions<BlogDBContext> options) : base(options) 
        {
            
        }
    }
}
