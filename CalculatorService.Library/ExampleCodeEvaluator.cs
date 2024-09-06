using System.Linq.Expressions;

namespace CalculatorService.Library;

public class ExampleCodeEvaluator : IExpressionEvaluator
{
    private readonly Stack<Expression> _expressions = new();
    private readonly Stack<char> _operators = new();
    
    public decimal Evaluate(string expression)
    {
        _expressions.Clear();
        _operators.Clear();

        using var reader = new StringReader(expression);

        int peek;

        while ((peek = reader.Peek()) > -1)
        {
            var next = (char)peek;

            if (char.IsDigit(next))
            {
                _expressions.Push(ReadOperand(reader));
                continue;
            }

            if (Operation.IsDefined(next))
            {
                var operation = ReadOperation(reader);

                EvaluateWhile(() =>
                    _operators.Count > 0 &&
                    _operators.Peek() != '(' &&
                    operation.Precedence <= ((Operation)_operators.Peek()).Precedence);

                _operators.Push(next);
                continue;
            }

            if (next == '(')
            {
                reader.Read();
                _operators.Push('(');

                continue;
            }

            if (next == ')')
            {
                reader.Read();

                EvaluateWhile(() => _operators.Count > 0 && _operators.Peek() != '(');

                _operators.Pop();

                continue;
            }

            if (next != ' ')
            {
                throw new ArgumentException($"Encountered invalid character {next}", nameof(expression));
            }
        }

        EvaluateWhile(() => _operators.Count > 0);

        var compiled = Expression.Lambda<Func<decimal>>(_expressions.Pop()).Compile();
        return compiled();
    }

    private static Expression ReadOperand(StringReader reader)
    {
        var myChar = (char)reader.Read();
        decimal val = decimal.Parse(myChar.ToString());

        return Expression.Constant(val);
    }

    private static Operation ReadOperation(StringReader reader)
    {
        var myChar = (char)reader.Read();

        return Operation.GetOperation(myChar);
    }

    private void EvaluateWhile(Func<bool> condition)
    {
        while (condition())
        {
            var right = _expressions.Pop();
            var left = _expressions.Pop();

            _expressions.Push(((Operation)_operators.Pop()).Apply(left, right));
        }
    }
}