using System;
using System.Collections.Generic;
using Moq;
using SigmaBooking.Core.IServices;
using SigmaBooking.Core.Models;
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
            var tableService = new TableService(repoMock.Object);
            Assert.IsAssignableFrom<ITableService>(tableService);
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

        [Fact]
        public void DeleteTable()
        {
            var table = new Table
            {
                Id = "id1",
                Static = false,
                X = 1,
                Y = 1,
                W = 1,
                H = 1,
                I = "1"
            };

            var repoMock = new Mock<ITableRepository>();

            repoMock.Setup(repository => repository.DeleteTable(table.Id));

            var tableService = new TableService(repoMock.Object);
            
            tableService.DeleteTable(table.Id);
            
            repoMock.Verify(repository => repository.DeleteTable(table.Id));
        }

        [Fact]
        public void GetTableById()
        {
            var expected = new Table
            {
                Id = "id1",
                Static = false,
                X = 1,
                Y = 1,
                W = 1,
                H = 1,
                I = "1"
            };

            var repoMock = new Mock<ITableRepository>();
            repoMock.Setup(repository => repository.GetTableById(expected.Id)).Returns(expected);
            var tableService = new TableService(repoMock.Object);
            
            Assert.Equal(expected, tableService.GetTableById("id1"), new TableComparer());
        }
        
        [Fact]
        public void CreateTable()
        {
            var expected = new Table
            {
                Id = "id1",
                Static = false,
                X = 1,
                Y = 1,
                W = 1,
                H = 1,
                I = "1"
            };

            var repoMock = new Mock<ITableRepository>();

            repoMock.Setup(repository => repository.CreateTable(expected)).Returns(expected);

            var tableService = new TableService(repoMock.Object);
            
            Assert.Equal(expected, tableService.CreateTable(expected), new TableComparer());
        }

        [Fact]
        public void UpdateTable()
        {
            var expected = new Table
            {
                Id = "id1",
                Static = false,
                X = 1,
                Y = 1,
                W = 1,
                H = 1,
                I = "1"
            };
            
            var repoMock = new Mock<ITableRepository>();

            repoMock.Setup(repository => repository.UpdateTable(expected)).Returns(expected);

            var tableService = new TableService(repoMock.Object);
            
            Assert.Equal(expected, tableService.UpdateTable(expected), new TableComparer());
        }

        public class TableComparer : IEqualityComparer<Table>
        {
            public bool Equals(Table x, Table y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id == y.Id && x.Static == y.Static && x.X == y.X && x.Y == y.Y && x.W == y.W && x.H == y.H && x.I == y.I;
            }

            public int GetHashCode(Table obj)
            {
                return HashCode.Combine(obj.Id, obj.Static, obj.X, obj.Y, obj.W, obj.H, obj.I);
            }
        }
    }
