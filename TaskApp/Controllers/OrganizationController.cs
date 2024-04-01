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
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(OrganizationCreateModel organization)
        {
            OrganizationHelper organizationHelper = new OrganizationHelper();
            organizationHelper.CreateNewOrganization(organization, TaskContext);
            return Redirect("/Organization/Index");
        }
    }
}
