using CalculatorService.Library.CalculatorOperators;

namespace CalculatorService.Library.ExpressionParsers;

public interface ICalculatorOperationParser
{
    bool IsOperationDefined(char operand);

    void ParseOperand(CalculatorContext context);
}

public class CalculatorOperationParser(IEnumerable<ICalculatorOperation> calculatorOperations) : ICalculatorOperationParser
{
    private const char OpenBracket = '(';

    public bool IsOperationDefined(char operand)
    {
        var calculatorOperation = calculatorOperations.SingleOrDefault(_ => _.CanApply(operand));

        return calculatorOperation != null;
    }

    public void ParseOperand(CalculatorContext context)
    {
        var currentOperation = (char)context.ExpressionReader!.Read();

        var calculatorOperation = calculatorOperations.Single(_ => _.CanApply(currentOperation));

        var operations = context.CalculatorOperations;
        var nextOperationOperand = (char)context.ExpressionReader.Peek();

        var nextOperationPrecedence =
            calculatorOperations.SingleOrDefault(_ => _.CanApply(nextOperationOperand)) == null
                ? 0
                : calculatorOperations.SingleOrDefault(_ => _.CanApply(nextOperationOperand)).Precedence;


        bool Func() =>
            operations.Count > 0 &&
            operations.Peek() != OpenBracket &&
            !char.IsDigit(operations.Peek()) &&
            calculatorOperation.Precedence <= nextOperationPrecedence;

        EvaluateOperation(Func, context);

        context.CalculatorOperations.Push(currentOperation);
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
            var operation = calculatorOperations.Single(_ => _.CanApply(operationType));

            expressions.Push(operation.Apply(left, right));
        }
    }
}