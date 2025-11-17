using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.csproj.Models;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    
    
        public class DepartmentController : Controller
        {
            private readonly ITIContext _context;

            public DepartmentController(ITIContext context)
            {
                _context = context;
            }
            public IActionResult Index()
            {
                List<Department> departmentList = _context.Departments.Include(x => x.Employees).ToList();
                return View("Index",departmentList);    
            }

            public IActionResult ShowDepartment()
            {
                var departments = _context.Departments.ToList();
                return View("ShowDep", departments);
            }

            public IActionResult Add() {
                return View("Add");
            }

        //(Video 7) // جزئيه بسيطه لو انا عاوز اHandle the request [Get , Post]
        //[HttpPost]
        //[HttpGet]

        [HttpPost] // Fillter
        public IActionResult SaveAdd(Department department)
        {
            if (department.Name != null)
            {
                _context.Departments.Add(department);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            // Return the Add view again if the department name is null
            return View("Add", department);
        }
        }
    }

