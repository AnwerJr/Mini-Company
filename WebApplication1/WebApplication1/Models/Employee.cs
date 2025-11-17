using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.csproj.Models
{
    public class Employee
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
        public String JobTitle { get; set; }
        public String ImageURL { get; set; }
        public String Address { get; set; }

        [ForeignKey("Department")]
        public int  DepartmentId { get; set; }
        public Department Department { get; set; }

    }
}
