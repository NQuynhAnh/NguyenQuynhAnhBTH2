using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NguyenQuynhAnhBTH2.Models
{
    public class Student
    {
        public string StudentID { get; set; }
        public string StudentName { get; set; }
        public string StudentAddress { get; set; }
        public string FacultyID { get; set; }
        [ForeignKey("FacultyID")]
        public Faculty Faculty { get; set; }
    }
}