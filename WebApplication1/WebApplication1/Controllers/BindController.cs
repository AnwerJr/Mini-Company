using Microsoft.AspNetCore.Mvc;
using WebApplication1.csproj.Models;

namespace WebApplication1.Controllers
{
    public class BindController : Controller
    {
        // المفروض هنا اول حاجه اعملها Request HTML (Data)     (1)
        // Binding Primitive (int .. , string... ,double....)   (2)
        //(URL) Bind/TestPrimitive?name=Amr&age=22 (3)

        //مثال علي ال form
        //<form action= "Bind/TestPrimitive" method="get">
        //    <input type="text" name="name">
        //    <input type="text" name="age">

        public IActionResult TestPrimitive(string name , int age)
        {
            return Content($"{name} \t {age}");
        }

        // في نوع تاني من ال Binding (Bind Collection) هنعملها Dictionary<Srting>Phones
        //(URL) Bind/TestDec?Phone[Amr]=01004790208

        public IActionResult TestDec(Dictionary<string, string> Phone)
        {
            return Content("OK");
        }



        //3) Binding OBJ    
        //(URL) Bind/TestObj?id=1&name=sd&managername=Saed

        public IActionResult TestObj(Department department)
        {
            return Content("OBJECT");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
