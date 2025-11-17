namespace WebApplication1.Models
{
    public class StudentBL
    {
        List<Student> students ;
        public StudentBL()
        {
            // ليست باسامي الطلاب انا مدخلها static
            students = new List<Student>();
            
               students.Add(new Student() {Id=1,Name="Mona",ImageURL= "1.jpg" });
               students.Add(new Student() {Id=2,Name="Mohamed",ImageURL="m.png"});
               students.Add(new Student() {Id=3,Name="Ali",ImageURL= "m.png" });
               students.Add(new Student() {Id=4,Name="Sara",ImageURL= "1.jpg" });
               students.Add(new Student() {Id=5,Name="Saed",ImageURL= "m.png" });
            }

        public List<Student> GetAll()
        {
             return students;
        }

        public Student GetById(int id)
        {
            return students.FirstOrDefault(s => s.Id == id);
        }
    }
}
