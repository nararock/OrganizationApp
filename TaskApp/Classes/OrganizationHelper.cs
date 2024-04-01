using TaskApp.Models;
using TaskApp.TaskDb;

namespace TaskApp.Classes
{
    public class OrganizationHelper
    {
        public void CreateNewOrganization(OrganizationCreateModel organization, TaskContext taskDb)
        {
            Organization newOrganization = new()
            {
                Name = organization.Name,
                INN = organization.INN,
                LegalAddress = organization.LegalAddress,
                ActualAddress = organization.ActualAddress,
            };
            taskDb.Organizations.Add(newOrganization);
            taskDb.SaveChanges();
        }
    }
}
