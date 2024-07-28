using CalculatorService.Library.Extensions;
using CalculatorService.Library.Models;

namespace CalculatorService.Library;

public interface IExpressionParser
{
    ParsedExpressionModel ParsedExpression(string expression);
}

public class ExpressionParser : IExpressionParser
{
    private readonly char[] _mathOperators;

    public ExpressionParser(ICalculatorFunctionFactory calculatorFunctionFactory)
    {
        _mathOperators = calculatorFunctionFactory
            .CalculatorFunctions
            .Select(_ => _.FunctionType)
            .ToArray();
    }

    public ParsedExpressionModel ParsedExpression(string expression)
    {
        var parts = expression.Split(_mathOperators, StringSplitOptions.TrimEntries);
        var mathOperator = expression.First(o => _mathOperators.Any(a => a == o));

        return new ParsedExpressionModel
        {
            Value1 = parts[0].ToDecimal(),
            Value2 = parts[1].ToDecimal(),
            Operation = mathOperator
        };
    }
}

