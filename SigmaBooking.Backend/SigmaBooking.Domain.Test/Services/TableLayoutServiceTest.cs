using System;
using System.Collections.Generic;
using Moq;
using SigmaBooking.Core.IServices;
using SigmaBooking.Core.Models;
using SigmaBooking.Domain.IRepositories;
using SigmaBooking.Domain.Services;
using Xunit;

namespace SigmaBooking.Domain.Test.Services;

public class TableLayoutServiceTest
{
    #region TableLayoutService init

    [Fact]
    public void TableLayoutService_IsITableLayoutService()
    {
        var repoMock = new Mock<ITableLayoutRepository>();
        var tableLayoutService = new TableLayoutService(repoMock.Object);
        Assert.IsAssignableFrom<ITableLayoutService>(tableLayoutService);
    }
    #endregion

    [Fact]
    public void GetTableLayoutByDate()
    {
        var expected = new TableLayout
        {
            Id = "id1",
            Date = "01/01/2022",
            IsDefault = false,
            Tables = null,
            TableIds = null
        };

        var repoMock = new Mock<ITableLayoutRepository>();
        repoMock.Setup(repository => repository.GetTableLayout(expected.Date)).Returns(expected);

        var tableLayoutService = new TableLayoutService(repoMock.Object);
        
        Assert.Equal(expected, tableLayoutService.GetTableLayout("01/01/2022"), new TableLayoutComparer());
    }

    [Fact]
    public void DeleteTableLayout()
    {
        var tableLayout = new TableLayout
        {
            Id = "id1",
            Date = "01/01/2022",
            IsDefault = false,
            Tables = null,
            TableIds = null
        };

        var repoMock = new Mock<ITableLayoutRepository>();
        repoMock.Setup(repository => repository.DeleteTableLayout(tableLayout.Id));

        var tableLayoutService = new TableLayoutService(repoMock.Object);
        
        tableLayoutService.DeleteTableLayout(tableLayout.Id);
        
        repoMock.Verify(repository => repository.DeleteTableLayout(tableLayout.Id));
    }

    [Fact]
    public void CreateTableLayout()
    {
        var expected = new TableLayout
        {
            Id = "id1",
            Date = "01/01/2022",
            IsDefault = false,
            Tables = null,
            TableIds = null
        };

        var repoMock = new Mock<ITableLayoutRepository>();
        repoMock.Setup(repository => repository.CreateTableLayout(expected)).Returns(expected);

        var tableLayoutService = new TableLayoutService(repoMock.Object);
        
        Assert.Equal(expected, tableLayoutService.CreateTableLayout(expected), new TableLayoutComparer());
    }
    
    [Fact]
    public void UpdateTableLayout()
    {
        var expected = new TableLayout
        {
            Id = "id1",
            Date = "01/01/2022",
            IsDefault = false,
            Tables = null,
            TableIds = null
        };

        var repoMock = new Mock<ITableLayoutRepository>();
        repoMock.Setup(repository => repository.UpdateTableLayout(expected)).Returns(expected);

        var tableLayoutService = new TableLayoutService(repoMock.Object);
        
        Assert.Equal(expected, tableLayoutService.UpdateTableLayout(expected), new TableLayoutComparer());
    }

    private class TableLayoutComparer : IEqualityComparer<TableLayout>
    {
        public bool Equals(TableLayout x, TableLayout y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Id == y.Id && x.IsDefault == y.IsDefault && x.Date == y.Date && x.TableIds.Equals(y.TableIds) && x.Tables.Equals(y.Tables);
        }

        public int GetHashCode(TableLayout obj)
        {
            return HashCode.Combine(obj.Id, obj.IsDefault, obj.Date, obj.TableIds, obj.Tables);
        }
    }
}