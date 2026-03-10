using Xunit;

public class GradeCalculatorTests
{
    private readonly GradeCalculator calc = new GradeCalculator();

    [Theory]
    [InlineData(95, "A")]
    [InlineData(80, "B")]
    [InlineData(65, "C")]
    [InlineData(40, "F")]
    public void GetGrade_ReturnsExpectedGrade(int score, string expected)
    {
        var grade = calc.GetGrade(score);
        Assert.Equal(expected, grade);
    }
}
