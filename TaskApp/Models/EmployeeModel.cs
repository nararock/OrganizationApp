namespace TaskApp.Models
{
    public class EmployeeModel
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateOnly Date { get; set; }
        public required string PassportSeries { get; set; }
        public required string PassportNumber { get; set; }
    }
}
