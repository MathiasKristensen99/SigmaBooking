using SigmaBooking.Core.Models;
using Xunit;

namespace SigmaBooking.Core.Test.Models;

public class BookingTest
{
    [Fact]
    public void BookingExists()
    {
        var booking = new Booking();
        Assert.NotNull(booking);
    }
    
}