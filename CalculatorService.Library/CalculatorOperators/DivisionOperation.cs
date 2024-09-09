using System.Linq.Expressions;

namespace CalculatorService.Library.CalculatorOperators;

public class DivisionOperation : ICalculatorOperation
{
    private const char DivisionOperand = '/';

    public string Name => nameof(DivisionOperation);

    public int Precedence => 2;

    public bool CanApply(char operand) => operand.Equals(DivisionOperand);

    public Operation CalculatorOperation => new(Precedence, Expression.Divide, "Divide");
}