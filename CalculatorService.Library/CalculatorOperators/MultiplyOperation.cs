using System.Linq.Expressions;

namespace CalculatorService.Library.CalculatorOperators;

public class MultiplyOperation : ICalculatorOperation
{
    private const char OperandX = 'x';
    private const char OperandStar = '*';

    public string Name => nameof(MultiplyOperation);

    public int Precedence => 2;

    public bool CanApply(char operand) => operand.Equals(OperandX) || operand.Equals(OperandStar);

    public Operation2 CalculatorOperation => new(Precedence, Expression.Multiply, "Multiply");
}