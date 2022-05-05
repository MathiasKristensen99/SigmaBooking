using Moq;
using SigmaBooking.Core.IServices;
using SigmaBooking.Domain.IRepositories;
using SigmaBooking.Domain.Services;
using Xunit;

namespace SigmaBooking.Domain.Test.Services;

public class TableServiceTest
{
    [Fact]
        public void TableService_IsITableService()
        {
            var repoMock = new Mock<ITableRepository>();
            var bookingService = new TableService(repoMock.Object);
            Assert.IsAssignableFrom<IBookingService>(bookingService);
        }
    
    
        [Fact]
        public void GetAll_NoParams_CallsTableRepositoryOnce()
        {
            // arrange
            var repoMock = new Mock<ITableRepository>();
            var tableService = new TableService(repoMock.Object);

            // act
            tableService.GetAllTables();
            
            // assert
            repoMock.Verify(repository => repository.GetAllTables(), Times.Once);
        }
    }
