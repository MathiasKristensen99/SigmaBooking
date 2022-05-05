using Moq;
using SigmaBooking.Core.IServices;
using Xunit;


namespace SigmaBooking.Core.Test.IServices;


public class ITableServiceTest
{
    [Fact]
    public void ITableServiceExists()
    {
        var serviceMock = new Mock<ITableService>();
        Assert.NotNull(serviceMock);
    }
    
}