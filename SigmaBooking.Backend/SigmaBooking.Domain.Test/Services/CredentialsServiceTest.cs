using System;
using System.Collections.Generic;
using Moq;
using SigmaBooking.Core.IServices;
using SigmaBooking.Core.Models;
using SigmaBooking.Domain.IRepositories;
using SigmaBooking.Domain.Services;
using Xunit;

namespace SigmaBooking.Domain.Test.Services;

public class CredentialsServiceTest
{
    [Fact]
    public void CredentialsService_IsCredentialsService()
    {
        var repoMock = new Mock<ICredentialsRepository>();
        var credentialsService = new CredentialsService(repoMock.Object);

        Assert.IsAssignableFrom<ICredentialsService>(credentialsService);
    }

    [Fact]
    public void GetCredentials()
    {
        var expected = new CredentialsModel
        {
            Id = "id1",
            Credentials = "Test"
        };

        var repoMock = new Mock<ICredentialsRepository>();
        repoMock.Setup(repository => repository.GetCredentials(expected.Credentials)).Returns(expected);
        var credentialsService = new CredentialsService(repoMock.Object);
        
        Assert.Equal(expected, credentialsService.GetCredentials("Test"), new CredentialsComparer());
    }
    
    [Fact]
    public void CreateCredentials()
    {
        var expected = new CredentialsModel
        {
            Id = "id1",
            Credentials = "Test"
        };

        var repoMock = new Mock<ICredentialsRepository>();
        repoMock.Setup(repository => repository.CreateCredentials(expected)).Returns(expected);
        var credentialsService = new CredentialsService(repoMock.Object);
        
        Assert.Equal(expected, credentialsService.CreateCredentials(expected), new CredentialsComparer());
    }

    [Fact]
    public void DeleteCredentials()
    {
        var credential = new CredentialsModel
        {
            Id = "id1",
            Credentials = "Test"
        };

        var repoMock = new Mock<ICredentialsRepository>();
        repoMock.Setup(repository => repository.DeleteCredentials(credential.Id));

        var credentialsService = new CredentialsService(repoMock.Object);
        
        credentialsService.DeleteCredentials(credential.Id);
        
        repoMock.Verify(repository => repository.DeleteCredentials(credential.Id));
    }

    public class CredentialsComparer : IEqualityComparer<CredentialsModel>
    {
        public bool Equals(CredentialsModel x, CredentialsModel y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Id == y.Id && x.Credentials == y.Credentials;
        }

        public int GetHashCode(CredentialsModel obj)
        {
            return HashCode.Combine(obj.Id, obj.Credentials);
        }
    }
}