using Microsoft.AspNetCore.Mvc;
using TaskApp.Classes;
using TaskApp.Models;
using TaskApp.TaskDb;

namespace TaskApp.Controllers
{
    public class EmployeeController : Controller
    {
        private TaskContext TaskContext;
        public EmployeeController(TaskContext taskContext)
        {
            TaskContext = taskContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            EmployeeHelper employeeHelper = new EmployeeHelper();
            List<OrganizationModel> organizations = employeeHelper.getOrganizations(TaskContext);
            CreatePageModel pageModel = new CreatePageModel
            {
                Organizations = organizations,
                Response = new Response
                {
                    State = true
                }
            };
            return View(pageModel);
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateModel employee)
        {
            EmployeeHelper employeeHelper = new EmployeeHelper();
            Response response =  employeeHelper.createNewEmployee(employee, TaskContext);
            if (response.State)
                return Redirect("/Employee/Index");
            List<OrganizationModel> organizations = employeeHelper.getOrganizations(TaskContext);
            CreatePageModel pageModel = new CreatePageModel
            {
                Organizations = organizations,
                Response = new Response
                {
                    State = response.State,
                    TextError = response.TextError,
                }
            };
            return View(pageModel);
        }
    }
}
