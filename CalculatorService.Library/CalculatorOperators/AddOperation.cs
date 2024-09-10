using System.Linq.Expressions;

namespace CalculatorService.Library.CalculatorOperators;

public class AddOperation : ICalculatorOperation
{
    private const char Operand = '+';

    private readonly Func<Expression, Expression, Expression> _operation = Expression.Add;
    
    public string Name => nameof(AddOperation);

    public int Precedence => 1;

    public bool CanApply(char operand) => operand.Equals(Operand);

    public Expression Apply(Expression left, Expression right) => _operation(left, right);
}