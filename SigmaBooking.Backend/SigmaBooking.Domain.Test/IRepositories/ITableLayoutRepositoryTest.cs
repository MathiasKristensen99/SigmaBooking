using Moq;
using SigmaBooking.Domain.IRepositories;
using Xunit;

namespace SigmaBooking.Domain.Test.IRepositories;

public class ITableLayoutRepositoryTest
{
    [Fact]
    public void ITableLayoutRepositoryExists()
    {
        var tableLayoutRepository = new Mock<ITableLayoutRepository>();
        Assert.NotNull(tableLayoutRepository);
    }
}