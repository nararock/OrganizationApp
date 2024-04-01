namespace TaskApp.Models
{
    public class OrganizationCreateModel
    {
        public required string Name { get; set; }
        public required string INN { get; set; }
        public required string LegalAddress { get; set; }
        public required string ActualAddress { get; set; }
    }
}
