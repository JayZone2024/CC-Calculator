using System.Linq.Expressions;

namespace CalculatorService.Library.CalculatorOperators;

public class AddOperation : ICalculatorOperation
{
    private const char Operand = '+';

    public string Name => nameof(AddOperation);

    public int Precedence => 1;

    public bool CanApply(char operand) => operand.Equals(Operand);

    public Operation CalculatorOperation => new(Precedence, Expression.Add, "Add");
}