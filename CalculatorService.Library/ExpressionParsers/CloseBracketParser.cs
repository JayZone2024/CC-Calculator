namespace CalculatorService.Library.ExpressionParsers;

public interface ICloseBracketParser
{
    bool IsCloseBracket(char operand);

    void ParseValue(CalculatorContext context);
}

public class CloseBracketParser : ICloseBracketParser
{
    private const char OpenBracket = '(';
    private const char CloseBracket = ')';

    public bool IsCloseBracket(char operand) => operand.Equals(CloseBracket);

    public void ParseValue(CalculatorContext context)
    {
        context.ExpressionReader!.Read();

        var operations = context.CalculatorOperations;
        
        bool Func() => operations.Count > 0 && operations.Peek() != OpenBracket;

        EvaluateOperation(Func, context);

        context.CalculatorOperations.Push(CloseBracket);
    }

    private static void EvaluateOperation(Func<bool> condition, CalculatorContext context)
    {
        var expressions = context.CalculatorExpressions;
        var operations = context.CalculatorOperations;

        while (condition())
        {
            var right = expressions.Pop();
            var left = expressions.Pop();

            expressions.Push(((Operation)operations.Pop()).Apply(left, right));
        }
    }
}