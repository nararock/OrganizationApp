using TaskApp.Models;
using TaskApp.TaskDb;

namespace TaskApp.Classes
{
    public class EmployeeHelper
    {
        public List<OrganizationModel> getOrganizations(TaskContext taskContext)
        {
            List<OrganizationModel> organizations  = taskContext.Organizations.Select(o => new OrganizationModel { Id = o.Id, Name = o.Name}).ToList();
            return organizations;
        }

        public Response createNewEmployee(EmployeeCreateModel employee, TaskContext taskContext)
        {
            Response response = checkDataEmployee(employee);    
            if (response.State)
            {
                Employee newEmployee = new()
                            {
                                Name = employee.Name,
                                Surname = employee.Surname,
                                Patronymic = employee.Patronymic,
                                Date = DateOnly.Parse(employee.Date),
                                PassportNumber = employee.PassportNumber,
                                PassportSeries = employee.PassportSeries,
                                OrganizationId = int.Parse(employee.OrganizationId),
                            };
                taskContext.Employees.Add(newEmployee);
                taskContext.SaveChanges();
            }
            return response;            
        }

        public Response checkDataEmployee(EmployeeCreateModel employee)
        {
            DateOnly date = new DateOnly();
            bool answer = DateOnly.TryParse(employee.Date, out date);
            int year = DateTime.Now.Year; 
            Response response = new Response
            {
                State = true
            };
            if (employee.Name == null || employee.Surname == null || employee.Date == null || employee.PassportSeries == null || employee.PassportNumber == null)
            {
                response.State = false;
                response.TextError = "Заполнены не все поля.";
                return response;
            }
            else if (!answer || date.Year >= year)
            {
                response.State = false;
                response.TextError = "Неправильный формат даты рождения.";
                return response;
            }
            else if (employee.OrganizationId == "0")
            {
                response.State = false;
                response.TextError = "Не выбрана организация.";
                return response;
            }
            return response;
        }
    }
}
