using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NguyenQuynhAnhBTH2.Models;

namespace NguyenQuynhAnhBTH2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Student { get; set; }
        public DbSet<NguyenQuynhAnhBTH2.Models.Faculty> Faculty { get; set; } = default!;
    }
}
