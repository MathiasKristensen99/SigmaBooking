using Moq;
using SigmaBooking.Core.IServices;
using Xunit;

namespace SigmaBooking.Core.Test.IServices;

public class ICredentialsServiceTest
{
    [Fact]
    public void ICredentialsServiceExists()
    {
        var serviceMock = new Mock<ICredentialsService>();
        Assert.NotNull(serviceMock);
    }
}