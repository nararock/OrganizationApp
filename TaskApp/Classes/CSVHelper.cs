using CsvHelper;
using Microsoft.AspNetCore.Authentication;
using System.Formats.Asn1;
using System.Globalization;
using System.Text;
using TaskApp.Models;
using TaskApp.TaskDb;

namespace TaskApp.Classes
{
    public class CSVHelper
    {
        public List<EmployeeModel> getDataEmployeeFromDB(int id, TaskContext taskContext)
        {
            List<EmployeeModel> employees = taskContext.Employees.Where(e => e.OrganizationId == id).Select(e => new EmployeeModel
            {
                Name = e.Name,
                Surname = e.Surname,
                Patronymic = e.Patronymic,
                PassportNumber = e.PassportNumber,
                PassportSeries = e.PassportSeries,
                Date = e.Date
            }).ToList();
            return employees;
        }

        public CSVModel getStringDataEmployee(int id, TaskContext taskContext)
        {
            List<EmployeeModel> employees = getDataEmployeeFromDB(id, taskContext);
            StringBuilder stringBuilder = new StringBuilder();
            foreach(var element in employees)
            {
                stringBuilder.Append(element.Name + ";");
                stringBuilder.Append(element.Surname + ";");
                stringBuilder.Append(element.Patronymic + ";");
                stringBuilder.Append(element.Date + ";");
                stringBuilder.Append(element.PassportSeries + ";");
                stringBuilder.AppendLine(element.PassportNumber);
            }
            string? name = taskContext.Organizations.FirstOrDefault(o => o.Id == id).Name.ToString();
            CSVModel model = new CSVModel
            {
                StringBuilder = stringBuilder,
                Name = name,
            };
            return model;
        }

        public void ReadFile(IFormFile file, string id, TaskContext taskContext)
        {
            string content = new StreamReader(file.OpenReadStream(), Encoding.UTF8).ReadToEnd();
            int idOrg =int.Parse(id);
            string[] contentArr = content.Split('\n');
            foreach (string line in contentArr)
            {
                string[] parts = line.Split(';');
                if (parts.Length == 6)
                {
                    Employee employee = new Employee
                    {
                        Name = parts[0],
                        Surname = parts[1],
                        Patronymic = parts[2],
                        Date = DateOnly.Parse(parts[3]),
                        PassportSeries = parts[4],
                        PassportNumber = parts[5],
                        OrganizationId = idOrg,
                    };
                    taskContext.Employees.Add(employee);
                    taskContext.SaveChanges();
                }
            }
        }
    }
}
