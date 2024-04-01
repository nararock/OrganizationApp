namespace TaskApp.TaskDb
{
    public class Employee
    {
        public int Id { get; set; }
        public int OrganizationId { get; set;}
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateOnly Date {  get; set; }
        public required string PassportSeries { get; set; }
        public required string PassportNumber { get; set; }
        public required Organization Organization { get; set; }
    }
}
