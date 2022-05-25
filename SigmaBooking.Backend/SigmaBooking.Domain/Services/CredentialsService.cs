using SigmaBooking.Core.IServices;
using SigmaBooking.Core.Models;
using SigmaBooking.Domain.IRepositories;

namespace SigmaBooking.Domain.Services;

public class CredentialsService : ICredentialsService
{
    private readonly ICredentialsRepository _repository;

    public CredentialsService(ICredentialsRepository credentialsRepository)
    {
        _repository = credentialsRepository;
    }

    public CredentialsModel CreateCredentials(CredentialsModel credentialsModel)
    {
        return _repository.CreateCredentials(credentialsModel);
    }

    public CredentialsModel GetCredentials(string credentials)
    {
        return _repository.GetCredentials(credentials);
    }

    public void DeleteCredentials(string id)
    {
        _repository.DeleteCredentials(id);
    }
}