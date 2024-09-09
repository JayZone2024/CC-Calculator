namespace CalculatorService.Library.CalculatorOperators;

public interface ICalculatorOperation
{
    string Name { get; }

    int Precedence { get; }

    bool CanApply(char operand);

    Operation CalculatorOperation { get; }
}