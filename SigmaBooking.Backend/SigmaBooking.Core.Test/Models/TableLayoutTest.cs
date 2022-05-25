using SigmaBooking.Core.Models;
using Xunit;

namespace SigmaBooking.Core.Test.Models;

public class TableLayoutTest
{
    [Fact]
    public void TableLayoutExists()
    {
        var tableLayout = new TableLayout();
        Assert.NotNull(tableLayout);
    }

    [Fact]
    public void TableLayoutHasStringProperty()
    {
        var tableLayout = new TableLayout();
        tableLayout.Date = (string)"01/01/2022";
        Assert.Equal("01/01/2022", tableLayout.Date);
    }
}