using SigmaBooking.Core.Models;

namespace SigmaBooking.Domain.IRepositories;

public interface ICredentialsRepository
{
    CredentialsModel CreateCredentials(CredentialsModel credentialsModel);

    void DeleteCredentials(string id);
}