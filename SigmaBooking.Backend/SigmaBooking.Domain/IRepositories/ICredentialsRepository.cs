using SigmaBooking.Core.Models;

namespace SigmaBooking.Domain.IRepositories;

public interface ICredentialsRepository
{
    CredentialsModel CreateCredentials(CredentialsModel credentialsModel);
    CredentialsModel GetCredentials(string id);
    void DeleteCredentials(string id);
}