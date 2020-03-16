using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using javora.Models.Database;

namespace javora.Models
{
    public class JavoraContext : IdentityDbContext<User>
    {
        public JavoraContext(DbContextOptions<JavoraContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<News> News { get; set; }

        public DbSet<News> Images { get; set; }

        public DbSet<Document> Documents { get; set; }
    }
}
