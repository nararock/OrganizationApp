using Microsoft.AspNetCore.Mvc;
using TaskApp.Classes;
using TaskApp.Models;
using TaskApp.TaskDb;

namespace TaskApp.Controllers
{
    public class OrganizationController : Controller
    {
        private TaskContext TaskContext;
        public OrganizationController(TaskContext taskContext)
        {
            TaskContext = taskContext;
        }

        public IActionResult Index()
        {
            EmployeeHelper employeeHelper = new EmployeeHelper();
            List<OrganizationModel> organizations = employeeHelper.getOrganizations(TaskContext);
            return View(organizations);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Response response = new Response
            {
                State = true,
            };
            return View(response);
        }

        [HttpPost]
        public IActionResult Create(OrganizationCreateModel organization)
        {
            OrganizationHelper organizationHelper = new OrganizationHelper();
            Response response = organizationHelper.CreateNewOrganization(organization, TaskContext);
            if (response.State)
                return Redirect("/Organization/Index");
            return View(response);
        }
    }
}
