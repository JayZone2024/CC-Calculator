using System.Linq.Expressions;
using System.Text.RegularExpressions;
using CalculatorService.Library.CalculatorOperators;

namespace CalculatorService.Library;

public class MathExpressionEvaluator(IExpressionParser expressionParser, IEnumerable<ICalculatorOperation> calculatorOperations) : IExpressionEvaluator
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

    private decimal EvaluateExpression(CalculatorContext context)
    {
        var expressions = context.CalculatorExpressions;
        var operations = context.CalculatorOperations;

        while (context.CalculatorOperations.Count > 0)
        {
            var right = expressions.Pop();
            var left = expressions.Pop();

            var operationType = operations.Pop();
            var operation = calculatorOperations.Single(_ => _.CanApply(operationType)).CalculatorOperation;

            expressions.Push(operation.Apply(left, right));
        }

        var compiled = Expression.Lambda<Func<decimal>>(expressions.Pop()).Compile();
        return compiled();
    }
}