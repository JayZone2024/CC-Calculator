using System.Linq.Expressions;

namespace CalculatorService.Library.CalculatorOperators;

public interface ICalculatorOperation
{
    string Name { get; }

    string Operator { get; }
    
    Expression Apply(Expression left, Expression right);
}