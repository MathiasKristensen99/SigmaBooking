using SigmaBooking.Core.Models;

namespace SigmaBooking.Core.IServices;

public interface ICredentialsService
{
    CredentialsModel CreateCredentials(CredentialsModel credentialsModel);
    CredentialsModel GetCredentials(string credentials);
    void DeleteCredentials(string id);
}