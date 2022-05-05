using Moq;
using Xunit;

namespace SigmaBooking.Domain.Test.IRepositories;

public class ITableRepository
{
    [Fact]
    public void ITableRepositoryExists()
    {
        var tableRepository = new Mock<ITableRepository>();
        Assert.NotNull(tableRepository);
    }
}