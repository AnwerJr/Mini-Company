using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class StudentController : Controller
    {
        
        public IActionResult ShowAll()
        {
            StudentBL studentBL = new StudentBL();
            List<Student> students = studentBL.GetAll(); // كده معناه هيجيبلي كل الطلاب في الmodel
            return View("ShowAll" ,students); // students دي هتبقي الmodel اللي هتبعت للview
        }

        public IActionResult Details(int id)
        {
            StudentBL studentBL = new StudentBL();
            Student StudentModel = studentBL.GetById(id); // كده معناه هيجيبلي كل الطلاب في الmodel
            return View("ShowDetails", StudentModel); // students دي هتبقي الmodel اللي هتبعت للview
        }
    }
}
