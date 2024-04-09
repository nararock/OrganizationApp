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

        public StringBuilder getDataOrganizatuionFromDB(TaskContext taskContext)
        {
            List<CSVOrganizationModel> organizations = taskContext.Organizations.Select(o => new CSVOrganizationModel { 
                Name = o.Name,
                INN = o.INN,
                ActualAddress = o.ActualAddress,
                LegalAddress = o.LegalAddress,
            }).ToList();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var element in organizations)
            {
                stringBuilder.Append(element.Name + ";");
                stringBuilder.Append(element.INN + ";");
                stringBuilder.Append(element.ActualAddress + ";");
                stringBuilder.AppendLine(element.LegalAddress);
            }
            return stringBuilder;
        }

        public void UploadOrganizations(IFormFile file, TaskContext taskContext)
        {
            string content = new StreamReader(file.OpenReadStream(), Encoding.UTF8).ReadToEnd();
            string[] contentArr = content.Split('\n');
            
            foreach(var element in contentArr)
            {
                string[] parts = element.Split(';');
                if (parts.Length == 4)
                {
                    Organization organization = new Organization
                        {
                            Name = parts[0],
                            INN = parts[1],
                            ActualAddress = parts[2],
                            LegalAddress = parts[3],
                        };
                    taskContext.Organizations.Add(organization);
                    taskContext.SaveChanges();
                }
                
            }
        }
    }
}
