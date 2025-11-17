namespace WebApplication1.csproj.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MangerName { get; set; }

        // Add this navigation property to fix CS1061
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
