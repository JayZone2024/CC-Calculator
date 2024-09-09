using System.Linq.Expressions;

namespace CalculatorService.Library.CalculatorOperators;

public class SubtractOperation : ICalculatorOperation
{
    private const char Operand = '-';

    public string Name => nameof(SubtractOperation);

    public int Precedence => 1;

    public bool CanApply(char operand) => operand.Equals(Operand);

    public Operation CalculatorOperation => new(Precedence, Expression.Subtract, "Subtract");
}