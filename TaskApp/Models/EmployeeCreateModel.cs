using TaskApp.TaskDb;

namespace TaskApp.Models
{
    public class EmployeeCreateModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Date {  get; set; }
        public string PassportSeries { get; set; }
        public string PassportNumber { get; set; }
        public string OrganizationId { get; set; }
    }
}
