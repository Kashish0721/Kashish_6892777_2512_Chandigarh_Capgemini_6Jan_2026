using Xunit;

public class DiscountServiceTest
{
    private readonly DiscountServiceTest service = new DiscountServiceTest();

    [Theory]
    [InlineData(120, 108)]  // 10% discount
    [InlineData(60, 57)]    // 5% discount
    [InlineData(30, 30)]    // no discount
    public void ApplyDiscount_ReturnsExpectedTotal(decimal total, decimal expected)
    {
        var result = service.ApplyDiscount(total);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ApplyDiscount_ZeroTotal_ReturnsZero()
    {
        decimal total = 0;
        var result = service.ApplyDiscount(total);
        Assert.Equal(0, result);
    }
}
