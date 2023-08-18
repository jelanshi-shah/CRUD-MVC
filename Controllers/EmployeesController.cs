using CRUDMVC.Data;
using CRUDMVC.Models;
using CRUDMVC.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CRUDMVC.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MvcDbContext mvcDbContext;

        public EmployeesController(MvcDbContext mvcDbContext)
        {
            this.mvcDbContext = mvcDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await mvcDbContext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            {
                var employee = new Employee()
                {
                    Id = Guid.NewGuid(),
                    Name = addEmployeeRequest.Name,
                    Email = addEmployeeRequest.Email,
                    Salary = addEmployeeRequest.Salary,
                    Department = addEmployeeRequest.Department,
                    DateOfBirth = addEmployeeRequest.DateOfBirth
                };
                await mvcDbContext.Employees.AddAsync(employee);
                await mvcDbContext.SaveChangesAsync();
                return RedirectToAction("Index");

            }
        }

        [HttpGet]

        public async Task<IActionResult> View(Guid Id)
        {
            var employee = mvcDbContext.Employees.FirstOrDefault(x => x.Id == Id);

            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth
                };
                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDbContext.Employees.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.Department = model.Department;
                employee.DateOfBirth = model.DateOfBirth;

                await mvcDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDbContext.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                mvcDbContext.Employees.Remove(employee);    
                await mvcDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }


}



