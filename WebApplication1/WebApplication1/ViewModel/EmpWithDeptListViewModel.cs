using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.csproj.Models;

namespace WebApplication1.ViewModel
{
    public class EmpWithDeptListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
        public String JobTitle { get; set; }
        public String ImageURL { get; set; }
        public String Address { get; set; }

        public int DepartmentId { get; set; }
        public List<Department> DepartmentList { get; set; }
    }
}
