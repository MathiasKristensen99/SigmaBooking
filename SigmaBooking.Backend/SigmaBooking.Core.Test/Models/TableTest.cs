using SigmaBooking.Core.Models;
using Xunit;

namespace SigmaBooking.Core.Test.Models;

public class TableTest
{
    [Fact]
    public void TableExists()
    {
        var table = new Table();
        Assert.NotNull(table);
    }
}