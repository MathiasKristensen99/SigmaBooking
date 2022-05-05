using Moq;
using SigmaBooking.Domain.IRepositories;
using Xunit;


namespace SigmaBooking.Domain.Test.IRepositories;

public class IBookingRepositoryTest
{
    [Fact]
    public void IBookingRepositoryExists()
    {
        var bookingRepository = new Mock<IBookingRepository>();
        Assert.NotNull(bookingRepository);
    }
    
}