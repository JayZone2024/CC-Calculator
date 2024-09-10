using System.Linq.Expressions;

namespace CalculatorService.Library.CalculatorOperators;

public interface ICalculatorOperation
{
    string Name { get; }

    int Precedence { get; }

    bool CanApply(char operand);

    Expression Apply(Expression left, Expression right);
}