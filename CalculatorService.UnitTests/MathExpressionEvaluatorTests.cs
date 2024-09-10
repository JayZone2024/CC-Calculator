﻿using CalculatorService.Library;
using CalculatorService.Library.CalculatorOperators;
using CalculatorService.Library.ExpressionParsers;
using FluentAssertions;

using ExpressionParser = CalculatorService.Library.ExpressionParser;

namespace CalculatorService.UnitTests;

public class MathExpressionEvaluatorTests
{
    private readonly ExpressionParser _expressionParser;
    private readonly IEnumerable<ICalculatorOperation> _calculatorOperations;


    public MathExpressionEvaluatorTests()
    {
        _calculatorOperations = new List<ICalculatorOperation>
        {
            new AddOperation(),
            new DivisionOperation(),
            new MultiplyOperation(),
            new SubtractOperation()
        };

        var calculatorOperationParser = new CalculatorOperationParser(_calculatorOperations);
        
        var numericValueParser = new NumericValueParser();
        var openBracketParser = new OpenBracketParser();
        var closeBracketParser = new CloseBracketParser(_calculatorOperations);
        
        _expressionParser = new ExpressionParser(
            calculatorOperationParser,
            numericValueParser,
            openBracketParser,
            closeBracketParser);
    }

    [Theory]
    [InlineData("2+2", 4)]
    [InlineData("2 + 2", 4)]
    [InlineData("((2 + 2) * 5) + 100", 120)]
    [InlineData("2*(5+3)", 16)]
    [InlineData("(5+3)*2", 16)]
    [InlineData("(100 * 2) + (1000 - 500)", 700)]
    public void When_Expression_Is_Evaluated_Then_Calculated_Value_Should_Be_Valid(string mathExpression, decimal expectedValue)
    {
        // Arrange
        var evaluator = new MathExpressionEvaluator(_expressionParser, _calculatorOperations);

        // Act
        var result = evaluator.Evaluate(mathExpression);

        // Assert
        result.Should().Be(expectedValue);
    }
}