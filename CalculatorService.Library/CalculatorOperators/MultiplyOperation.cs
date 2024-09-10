using System.Linq.Expressions;

namespace CalculatorService.Library.CalculatorOperators;

public class MultiplyOperation : ICalculatorOperation
{
    private const char OperandX = 'x';
    private const char OperandStar = '*';

    private readonly Func<Expression, Expression, Expression> _operation = Expression.Multiply;
    
    public string Name => nameof(MultiplyOperation);

    public int Precedence => 2;

    public bool CanApply(char operand) => operand.Equals(OperandX) || operand.Equals(OperandStar);

    public Expression Apply(Expression left, Expression right) => _operation(left, right);
}