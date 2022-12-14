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
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<NguyenQuynhAnhBTH2.Models.Employee> Employee { get; set; }
        public DbSet<NguyenQuynhAnhBTH2.Models.Student> Student { get; set; }
        public DbSet<NguyenQuynhAnhBTH2.Models.Customer> Customer { get; set; }
        public DbSet<NguyenQuynhAnhBTH2.Models.Person> Person { get; set; }
    }
}