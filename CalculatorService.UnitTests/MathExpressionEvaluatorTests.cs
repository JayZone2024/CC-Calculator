﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculatorService.Library;
using CalculatorService.Library.CalculatorOperators;
using CalculatorService.Library.ExpressionParsers;
using FluentAssertions;

namespace CalculatorService.UnitTests;

public class MathExpressionEvaluatorTests
{
    private readonly MathExpressionEvaluator _evaluator;

    public MathExpressionEvaluatorTests()
    {
        var calculatorOperations = new List<ICalculatorOperation>
        {
            new AddOperation(),
            new DivisionOperation(),
            new MultiplyOperation(),
            new SubtractOperation()
        };

        var calculatorOperationParser = new CalculatorOperationParser(calculatorOperations);
        var numericValueParser = new NumericValueParser();
        var openBracketParser = new OpenBracketParser();
        var closeBracketParser = new CloseBracketParser(calculatorOperations);

        _evaluator = new MathExpressionEvaluator(
            calculatorOperationParser,
            numericValueParser,
            openBracketParser,
            closeBracketParser);
    }

    [Fact]
    public void Test1()
    {
        // Arrange
        const string expression = "(2 + 2) * 5";
        const decimal expectedResult = 4;

        // Act
        var result = _evaluator.Evaluate(expression);

        // Assert
        result.Should().Be(expectedResult);
    }
}