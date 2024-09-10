using System.Linq.Expressions;

namespace CalculatorService.Library.CalculatorOperators;

public class DivisionOperation : ICalculatorOperation
{
    private const char DivisionOperand = '/';

    private readonly Func<Expression, Expression, Expression> _operation = Expression.Divide;

    public string Name => nameof(DivisionOperation);

    public int Precedence => 2;

    public bool CanApply(char operand) => operand.Equals(DivisionOperand);
    
    public Expression Apply(Expression left, Expression right) => _operation(left, right);
}