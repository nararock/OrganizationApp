using TaskApp.Models;
using TaskApp.TaskDb;

namespace TaskApp.Classes
{
    public class OrganizationHelper
    {
        public Response CreateNewOrganization(OrganizationCreateModel organization, TaskContext taskDb)
        {
            Response response = checkOrganizationData(organization);
            if (response.State)
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
                return response;
            }
           return response;
        }

        public Response checkOrganizationData(OrganizationCreateModel organization)
        {
            Response response = new Response
            {
                State = true,
            };
            if (organization.Name == null || organization.INN  == null || organization.LegalAddress == null || organization.ActualAddress == null)
            {
                response.State = false;
                response.TextError = "Не все поля заполнены.";
                return response;
            }
            return response;
        }
    }
}
