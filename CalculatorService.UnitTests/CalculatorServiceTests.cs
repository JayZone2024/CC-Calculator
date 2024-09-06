using CalculatorService.Library;
using FluentAssertions;

namespace CalculatorService.UnitTests;

public class CalculatorServiceTests
{
    private readonly IExpressionEvaluator _expressionEvaluator = new ExampleCodeEvaluator();

    [Fact]
    public void When_Adding_Two_Plus_Two_Then_Result_Should_Be_Four()
    {
        // Arrange
        const string expression = "2+2.1";
        const decimal expectedResult = 4;

        // Act
        var result = _expressionEvaluator.Evaluate(expression);

        // Assert
        result.Should().Be(expectedResult);
    }
}
