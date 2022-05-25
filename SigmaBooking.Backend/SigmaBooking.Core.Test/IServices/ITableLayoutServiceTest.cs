using Moq;
using SigmaBooking.Core.IServices;
using Xunit;

namespace SigmaBooking.Core.Test.IServices;

public class ITableLayoutServiceTest
{
    [Fact]
    public void ITableLayoutServiceExists()
    {
        var serviceMock = new Mock<ITableLayoutService>();
        Assert.NotNull(serviceMock);
    }
}