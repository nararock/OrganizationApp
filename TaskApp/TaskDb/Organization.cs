namespace TaskApp.TaskDb
{
    public class Organization
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string INN { get; set; }
        public required string LegalAddress { get; set; }
        public required string ActualAddress { get; set; }
        public List<Employee> Employees { get; set; } = [];
    }
}
