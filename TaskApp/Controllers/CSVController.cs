﻿using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Http;
using TaskApp.Classes;
using TaskApp.Models;
using TaskApp.TaskDb;

namespace TaskApp.Controllers
{
    public class CSVController : Controller
    {
        private TaskContext TaskContext;
        public CSVController(TaskContext taskContext)
        {
            TaskContext = taskContext;
        }
        public IActionResult Index(string id)
        {
            int idOrg = int.Parse(id);
            CSVHelper CSVHelper = new CSVHelper();
            CSVModel model = CSVHelper.getStringDataEmployee(idOrg, TaskContext);
            return File(System.Text.Encoding.UTF8.GetBytes(model.StringBuilder.ToString()), "text/csv", model.Name + ".csv");
        }


        public IActionResult ImportFile([FromForm(Name ="data")] IFormFile file, [FromForm(Name = "organizationId")] string id)
        {
            CSVHelper helper = new CSVHelper();
            helper.ReadFile(file, id, TaskContext);
            return Redirect("/Home/Index");
        }

        public IActionResult DownloadOrganizations()
        {
            CSVHelper helper = new CSVHelper();
            StringBuilder organizations = helper.getDataOrganizatuionFromDB(TaskContext);
            return File(System.Text.Encoding.UTF8.GetBytes(organizations.ToString()), "text/csv",  "organizations.csv");
        }

        public IActionResult UploadOrganizations([FromForm(Name= "dataUpload")] IFormFile file)
        {
            CSVHelper helper = new CSVHelper();
            helper.UploadOrganizations(file, TaskContext);
            return Redirect("/Home/Index");
        }
    }
}
