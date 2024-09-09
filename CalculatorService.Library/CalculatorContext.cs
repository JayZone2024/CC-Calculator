﻿using System.Linq.Expressions;

namespace CalculatorService.Library;

public class CalculatorContext
{
    public string? Expression { get; init; }

    public StringReader? ExpressionReader { get; init; }

    public Stack<Expression> CalculatorExpressions { get; } = new();

    public Stack<char> CalculatorOperations { get; } = new();

    public static CalculatorContext CreateWith(string expression, StringReader expressionReader) => new()
    {
        Expression = expression,
        ExpressionReader = expressionReader
    };
}