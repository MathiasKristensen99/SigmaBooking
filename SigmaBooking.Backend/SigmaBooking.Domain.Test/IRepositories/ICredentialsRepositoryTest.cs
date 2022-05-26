using Moq;
using SigmaBooking.Domain.IRepositories;
using Xunit;

namespace SigmaBooking.Domain.Test.IRepositories;

public class ICredentialsRepositoryTest
{
    [Fact]
    public void ICredentialsRepositoryExists()
    {
        var credentialsRepository = new Mock<ICredentialsRepository>();
        Assert.NotNull(credentialsRepository);
    }
}