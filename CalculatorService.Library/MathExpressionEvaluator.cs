using CalculatorService.Library.Exceptions;
using CalculatorService.Library.ExpressionParsers;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace CalculatorService.Library;

public class MathExpressionEvaluator(
    ICalculatorOperationParser calculatorOperation,
    INumericValueParser numericValueParser,
    IOpenBracketParser openBracketParser,
    ICloseBracketParser closeBracketParser) : IExpressionEvaluator
{
    private const char OpenBracket = '(';

    public decimal Evaluate(string expression)
    {
        expression = Regex
            .Replace(expression, @"\s+", string.Empty)
            .Trim();

        var reader = new StringReader(expression);

        var context = CalculatorContext.CreateWith(expression, reader);
        int peek;

        while ((peek = reader.Peek()) > -1)
        {
            var nextOperand = (char)peek;
            context.NextOperand = nextOperand;

            if (numericValueParser.IsNumericValue(nextOperand, reader, out var number))
            {
                numericValueParser.ParseValue(context, number!.Value);
                continue;
            }

            if (calculatorOperation.IsOperationDefined(context))
            {
                calculatorOperation.ParseOperand(context);
                continue;
            }

            if (openBracketParser.IsOpenBracket(nextOperand))
            {
                openBracketParser.ParseValue(context);
                continue;
            }

            if (closeBracketParser.IsCloseBracket(nextOperand))
            {
                closeBracketParser.ParseValue(context);
                continue;
            }

            throw new UnknownOperandException(
                nextOperand,
                $"Invalid operand '{nextOperand}' detected in expression.");
        }

        var value = EvaluateExpression(context);

        return value;
    }

    private static decimal EvaluateExpression(CalculatorContext context)
    {
        var expressions = context.CalculatorExpressions;
        var operations = context.CalculatorOperations;

        while (context.CalculatorOperations.Count > 0)
        {
            var right = expressions.Pop();
            var left = expressions.Pop();

            expressions.Push(((Operation)operations.Pop()).Apply(left, right));
        }

        var compiled = Expression.Lambda<Func<decimal>>(expressions.Pop()).Compile();
        return compiled();
    }
}