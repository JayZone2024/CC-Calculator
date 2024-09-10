using CalculatorService.Library.CalculatorOperators;

namespace CalculatorService.Library.ExpressionParsers;

public interface ICalculatorOperationParser
{
    bool IsOperationDefined(char operand);

    void ParseOperand(CalculatorContext context);
}

public class CalculatorOperationParser(
    ICalculatorOperationFactory calculatorOperationFactory) : ICalculatorOperationParser
{
    private const char OpenBracket = '(';

    public bool IsOperationDefined(char operand) => calculatorOperationFactory.DoesOperationExists(operand);

    public void ParseOperand(CalculatorContext context)
    {
        var currentOperation = (char)context.ExpressionReader!.Read();

        var calculatorOperation = calculatorOperationFactory.GetCalculatorOperation(currentOperation);

        var operations = context.CalculatorOperations;
        var nextOperationOperand = (char)context.ExpressionReader.Peek();

        var nextOperationPrecedence =
            !calculatorOperationFactory.DoesOperationExists(nextOperationOperand)
                ? 0
                : calculatorOperationFactory.GetCalculatorOperation(nextOperationOperand)!.Precedence;


        bool Func() =>
            operations.Count > 0 &&
            operations.Peek() != OpenBracket &&
            !char.IsDigit(operations.Peek()) &&
            calculatorOperation!.Precedence <= nextOperationPrecedence;

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
            var operation = calculatorOperationFactory.GetCalculatorOperation(operationType);

            expressions.Push(operation.Apply(left, right));
        }
    }
}