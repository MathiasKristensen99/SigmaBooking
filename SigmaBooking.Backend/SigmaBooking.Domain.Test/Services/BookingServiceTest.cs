using System.IO;
using Moq;
using SigmaBooking.Core.IServices;
using SigmaBooking.Domain.IRepositories;
using SigmaBooking.Domain.Services;
using Xunit;

namespace SigmaBooking.Domain.Test.Services;

public class BookingServiceTest
{
    [Fact]
    public void BookingService_IsIBookingService()
    {
        var repoMock = new Mock<IBookingRepository>();
        var bookingService = new BookingService(repoMock.Object);

        Assert.IsAssignableFrom<IBookingService>(bookingService);
    }
    
    
    [Fact]
    public void GetAll_NoParams_CallsAccountRepositoryOnce()
    {
        // arrange
        var repoMock = new Mock<IBookingRepository>();
        var bookingService = new BookingService(repoMock.Object);

        // act
        bookingService.GetAllBookings();
            
        // assert
        repoMock.Verify(repository => repository.GetAllBookings(), Times.Once);
    }
}