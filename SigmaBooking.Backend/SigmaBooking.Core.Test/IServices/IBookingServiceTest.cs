using Moq;
using SigmaBooking.Core.IServices;
using Xunit;

namespace SigmaBooking.Core.Test.IServices;

public class IBookingServiceTest
{
    [Fact]
    public void IBookingServiceExists()
    {
        var serviceMock = new Mock<IBookingService>();
        Assert.NotNull(serviceMock);
    }
    
}