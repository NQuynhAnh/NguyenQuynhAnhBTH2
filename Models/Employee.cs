using System.ComponentModel.DataAnnotations;

namespace NguyenQuynhAnhBTH2.Models
{
    public class Employee
    {
        [Key]
        public string EmpID { get; set; }
        public string EmpName { get; set; }
        public string Address { get; set; }
    }
}