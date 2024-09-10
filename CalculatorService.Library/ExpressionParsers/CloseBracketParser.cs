using CalculatorService.Library.CalculatorOperators;

namespace CalculatorService.Library.ExpressionParsers;

public interface ICloseBracketParser
{
    bool IsCloseBracket(char operand);

    void ParseValue(CalculatorContext context);
}

public class CloseBracketParser(ICalculatorOperationFactory calculatorOperationFactory) : ICloseBracketParser
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

        context.CalculatorOperations.Pop();
    }

    private void EvaluateOperation(Func<bool> condition, CalculatorContext context)
    {
        var expressions = context.CalculatorExpressions;
        var operations = context.CalculatorOperations;

        while (condition())
        {
            var right = expressions.Pop();
            var left = expressions.Pop();

            var operationType = operations.Pop();
            var operation = calculatorOperationFactory.GetCalculatorOperation(operationType);

            expressions.Push(operation.Apply(left, right));
        }
    }
}