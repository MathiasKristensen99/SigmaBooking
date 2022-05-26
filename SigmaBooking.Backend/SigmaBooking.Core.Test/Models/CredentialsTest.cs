using SigmaBooking.Core.Models;
using Xunit;

namespace SigmaBooking.Core.Test.Models;

public class CredentialsTest
{
    [Fact]
    public void CredentialsExists()
    {
        var credentials = new CredentialsModel();
        Assert.NotNull(credentials);
    }
}