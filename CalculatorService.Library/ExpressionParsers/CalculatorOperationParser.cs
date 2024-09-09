using CalculatorService.Library.CalculatorOperators;

namespace CalculatorService.Library.ExpressionParsers;

public interface ICalculatorOperationParser
{
    bool IsOperationDefined(char operand);

    void EvaluateOperand(CalculatorContext context);
}

public class CalculatorOperationParser(IEnumerable<ICalculatorOperation> calculatorOperations) : ICalculatorOperationParser
{
    public bool IsOperationDefined(char operand)
    {
        var calculatorOperation = calculatorOperations.FirstOrDefault(_ => _.CanApply(operand));

        return calculatorOperation != null;
    }

    public void EvaluateOperand(CalculatorContext context)
    {
        var currentOperation = (char)context.ExpressionReader!.Read();

        var calculatorOperation = calculatorOperations.Single(_ => _.CanApply(currentOperation));

        var operations = context.CalculatorOperations;
        var precedenceOfNextOperation = ((Operation)context.ExpressionReader.Peek()).Precedence;

        bool Func() =>
            operations.Count > 0 &&
            operations.Peek() != '(' &&
            calculatorOperation.Precedence <= precedenceOfNextOperation;

        EvaluateOperation(Func, context);
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