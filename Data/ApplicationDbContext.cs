using NguyenQuynhAnhBTH2.Models;
using Microsoft.EntityFrameworkCore;

namespace NguyenQuynhAnhBTH2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Student> Students {get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<NguyenQuynhAnhBTH2.Models.Employee> Employee { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}