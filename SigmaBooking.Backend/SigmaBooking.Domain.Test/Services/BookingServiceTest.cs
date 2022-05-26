using System;
using System.Collections.Generic;
using System.IO;
using Moq;
using SigmaBooking.Core.IServices;
using SigmaBooking.Core.Models;
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
    public void GetAll_NoParams_CallsBookingRepositoryOnce()
    {
        // arrange
        var repoMock = new Mock<IBookingRepository>();
        var bookingService = new BookingService(repoMock.Object);

        // act
        bookingService.GetAllBookings();
            
        // assert
        repoMock.Verify(repository => repository.GetAllBookings(), Times.Once);
    }
    
    [Fact]
    public void GetAllByDate()
    {
        var expected = new List<Booking>();
        expected.Add(new Booking
        {
            Id = "id1",
            Name = "test",
            Email = "test@gmail.com",
            Phone = "12345678",
            Description = "Test",
            TableId = "test",
            Table = null,
            Date = "01012022",
            StartTime = "18:00",
            EndTime = "24:00",
            PeopleCount = 2,
            IsEating = true
        });
        
        var repoMock = new Mock<IBookingRepository>();
        repoMock.Setup(repository => repository.GetBookingsByDate("01012022")).Returns(expected);
        var bookingService = new BookingService(repoMock.Object);
        
        Assert.Equal(expected, bookingService.GetBookingsByDate("01012022"), new BookingComparer());
    }
    
    [Fact]
    public void GetAllBookings()
    {
        var expected = new List<Booking>();
        expected.Add(new Booking
        {
            Id = "id1",
            Name = "test",
            Email = "test@gmail.com",
            Phone = "12345678",
            Description = "Test",
            TableId = "test",
            Table = null,
            Date = "01012022",
            StartTime = "18:00",
            EndTime = "24:00",
            PeopleCount = 2,
            IsEating = true
        });
        
        var repoMock = new Mock<IBookingRepository>();
        repoMock.Setup(repository => repository.GetAllBookings()).Returns(expected);
        var bookingService = new BookingService(repoMock.Object);
        
        Assert.Equal(expected, bookingService.GetAllBookings(), new BookingComparer());
    }
    
    [Fact]
    public void GetAllByDateAndTable()
    {
        var expected = new List<Booking>();
        expected.Add(new Booking
        {
            Id = "id1",
            Name = "test",
            Email = "test@gmail.com",
            Phone = "12345678",
            Description = "Test",
            TableId = "test",
            Table = null,
            Date = "01012022",
            StartTime = "18:00",
            EndTime = "24:00",
            PeopleCount = 2,
            IsEating = true
        });
        
        var repoMock = new Mock<IBookingRepository>();
        repoMock.Setup(repository => repository.GetBookingsByTableAndDate("test", "01012022")).Returns(expected);
        var bookingService = new BookingService(repoMock.Object);
        
        Assert.Equal(expected, bookingService.GetBookingsByTableAndDate("test", "01012022"), new BookingComparer());
    }

    [Fact]
    public void GetBookingById()
    {
        var expected = new Booking
        {
            Id = "id1",
            Name = "test",
            Email = "test@gmail.com",
            Phone = "12345678",
            Description = "Test",
            TableId = "test",
            Table = null,
            Date = "01/01/2022",
            StartTime = "18:00",
            EndTime = "24:00",
            PeopleCount = 2,
            IsEating = true
        };

        var repoMock = new Mock<IBookingRepository>();
        repoMock.Setup(repository => repository.GetBooking(expected.Id)).Returns(expected);
        var bookingService = new BookingService(repoMock.Object);
        
        Assert.Equal(expected, bookingService.GetBooking("id1"), new BookingComparer());
    }

    [Fact]
    public void DeleteBooking()
    {
        var booking = new Booking
        {
            Id = "id1",
            Name = "test",
            Email = "test@gmail.com",
            Phone = "12345678",
            Description = "Test",
            TableId = "test",
            Table = null,
            Date = "01/01/2022",
            StartTime = "18:00",
            EndTime = "24:00",
            PeopleCount = 2,
            IsEating = true
        };

        var repoMock = new Mock<IBookingRepository>();
        repoMock.Setup(repository => repository.DeleteBooking(booking.Id));

        var bookingService = new BookingService(repoMock.Object);
        
        bookingService.DeleteBooking(booking.Id);
        
        repoMock.Verify(repository => repository.DeleteBooking(booking.Id));
    }

    [Fact]
    public void CreateBooking()
    {
        var expected = new Booking
        {
            Id = "id1",
            Name = "test",
            Email = "test@gmail.com",
            Phone = "12345678",
            Description = "Test",
            TableId = "test",
            Table = null,
            Date = "01/01/2022",
            StartTime = "18:00",
            EndTime = "24:00",
            PeopleCount = 2,
            IsEating = true
        };

        var repoMock = new Mock<IBookingRepository>();
        repoMock.Setup(repository => repository.CreateBooking(expected)).Returns(expected);

        var bookingService = new BookingService(repoMock.Object);

        Assert.Equal(expected, bookingService.CreateBooking(expected), new BookingComparer());
    }
    
    [Fact]
    public void UpdateBooking()
    {
        var expected = new Booking
        {
            Id = "id1",
            Name = "test",
            Email = "test@gmail.com",
            Phone = "12345678",
            Description = "Test",
            TableId = "test",
            Table = null,
            Date = "01/01/2022",
            StartTime = "18:00",
            EndTime = "24:00",
            PeopleCount = 2,
            IsEating = true
        };

        var repoMock = new Mock<IBookingRepository>();
        repoMock.Setup(repository => repository.UpdateBooking(expected)).Returns(expected);

        var bookingService = new BookingService(repoMock.Object);

        Assert.Equal(expected, bookingService.UpdateBooking(expected), new BookingComparer());
    }

    public class BookingComparer : IEqualityComparer<Booking>
    {
        public bool Equals(Booking x, Booking y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Id == y.Id 
                   && x.TableId == y.TableId 
                   && x.Table.Equals(y.Table) 
                   && x.Name == y.Name 
                   && x.Phone == y.Phone 
                   && x.Email == y.Email 
                   && x.PeopleCount == y.PeopleCount 
                   && x.Date == y.Date 
                   && x.StartTime == y.StartTime 
                   && x.EndTime == y.EndTime 
                   && x.IsEating == y.IsEating 
                   && x.Description == y.Description;
        }

        public int GetHashCode(Booking obj)
        {
            var hashCode = new HashCode();
            hashCode.Add(obj.Id);
            hashCode.Add(obj.TableId);
            hashCode.Add(obj.Table);
            hashCode.Add(obj.Name);
            hashCode.Add(obj.Phone);
            hashCode.Add(obj.Email);
            hashCode.Add(obj.PeopleCount);
            hashCode.Add(obj.Date);
            hashCode.Add(obj.StartTime);
            hashCode.Add(obj.EndTime);
            hashCode.Add(obj.IsEating);
            hashCode.Add(obj.Description);
            return hashCode.ToHashCode();
        }
    }
}