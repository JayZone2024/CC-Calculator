using System.Linq.Expressions;

namespace CalculatorService.Library;

public sealed class Operation2(int precedence, Func<Expression, Expression, Expression> operation, string name)
{
    private readonly string _name = name;

    public int Precedence { get; } = precedence;

    public Expression Apply(Expression left, Expression right) => operation(left, right);
}