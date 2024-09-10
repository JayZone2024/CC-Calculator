using CalculatorService.Library.Exceptions;
using CalculatorService.Library.ExpressionParsers;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace CalculatorService.Library;

public class MathExpressionEvaluator(IExpressionParser expressionParser) : IExpressionEvaluator
{
    public decimal Evaluate(string expression)
    {
        expression = Regex
            .Replace(expression, @"\s+", string.Empty)
            .Trim();

        var reader = new StringReader(expression);

        var context = CalculatorContext.CreateWith(expression, reader);

        expressionParser.Parse(context);
        
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