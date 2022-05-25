using SigmaBooking.Core.Models;

namespace SigmaBooking.Core.IServices;

public interface ICredentialsService
{
    CredentialsModel CreateCredentials(CredentialsModel credentialsModel);
    void DeleteCredentials(string id);
}