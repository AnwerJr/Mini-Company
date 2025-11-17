using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.csproj.Models;
using WebApplication1.Models;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ITIContext context;

        public EmployeeController(ITIContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var employees = context.Employees.ToList();
            return View("Index", employees);
        }
        public IActionResult Edit(int id)
        {
            Employee employee = context.Employees.FirstOrDefault(e=>e.Id == id);
            List<Department> departmentList = context.Departments.ToList();
            //--------------------=> create view mode Mapping
            EmpWithDeptListViewModel empWithDeptListViewModel = new EmpWithDeptListViewModel();
            empWithDeptListViewModel.Id = employee.Id;
            empWithDeptListViewModel.Name = employee.Name;
            empWithDeptListViewModel.Address = employee.Address;
            empWithDeptListViewModel.ImageURL = employee.ImageURL;
            empWithDeptListViewModel.JobTitle = employee.JobTitle;
            empWithDeptListViewModel.Salary = employee.Salary;
            empWithDeptListViewModel.DepartmentId = employee.DepartmentId;
            empWithDeptListViewModel.DepartmentList = departmentList;

            if (employee == null)
            {
                return View("Error");
            }
            else
            {
                ViewBag.Departments = new SelectList(context.Departments.ToList(), "Id", "Name");

                return View("Edit", empWithDeptListViewModel);
            }

        }
        [HttpPost]
        public IActionResult SaveEdit(Employee EmpFromRequest)
        {
            if (EmpFromRequest != null)
            {
                Employee empFromDB = context.Employees.FirstOrDefault(e => e.Id == EmpFromRequest.Id);
                if (empFromDB == null)
                {
                    return NotFound();
                }

                // Update properties
                empFromDB.Name = EmpFromRequest.Name;
                empFromDB.Salary = EmpFromRequest.Salary;
                empFromDB.JobTitle = EmpFromRequest.JobTitle;
                empFromDB.ImageURL = EmpFromRequest.ImageURL;
                empFromDB.DepartmentId = EmpFromRequest.DepartmentId;

                try
                {
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException)
                {
                    // لو حصل خطأ بسبب ForeignKey
                    ViewBag.Departments = new SelectList(context.Departments.ToList(), "Id", "Name");
                    ModelState.AddModelError("", "القسم المحدد غير موجود.");
                    return View("Edit", EmpFromRequest);
                }
            }

            ViewBag.Departments = new SelectList(context.Departments.ToList(), "Id", "Name");
            return View("Edit", EmpFromRequest);
        }






        public IActionResult Details(int id)
        {

            String MSG = "Hello from Employee Controller Details Action";
            int TEMP = 50;

            //List of Strings (Branches)
            List<string> Branches = new List<string>()
            {
                "Qena",
                "Alex",
                "CAiro",
                "Banha",
            };

            // Additional info sent to the view using ViewData from action 
            ViewData["Message"] = MSG ;
            ViewData["Temprature"] = TEMP;
            ViewData["Branches"] = Branches;
            


            Employee employeeModel = context.Employees.FirstOrDefault(x => x.Id == id);
            if (employeeModel == null)
            {
                return NotFound();
            }
            return View(employeeModel);
        }

        // خاص بال View ,Model
        public IActionResult DetailsMV(int id)
        {
            Employee employee = context.Employees.Include(d => d.Department).FirstOrDefault(z => z.Id == id);

            List<string> Branches = new List<string>()
            {
                "Qena",
                "Alex",
                "CAiro",
                "Banha",
            };
            // Declare and initialize View Model
            EmpDeptColorTempMsgBranchesViewModel EmpVM = new EmpDeptColorTempMsgBranchesViewModel
            {
                //Mapping
                EmpName = employee?.Name,
                DepName = employee?.Department?.Name,
                Branches = Branches,
                Color = "blue", // or any logic to determine color
                Temp = 50, // or any logic to determine temp
                Msg = "Hello from Employee Controller DetailsMV Action"
            };
            if (employee == null)
            {
                return NotFound();
            }
            return View("DetailsMV", EmpVM);
        }
    }
}
