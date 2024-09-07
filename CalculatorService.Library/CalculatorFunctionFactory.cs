using CalculatorService.Library.CalculatorOperators;

namespace CalculatorService.Library;

public interface ICalculatorFunctionFactory
{
    IEnumerable<ICalculatorOperation> CalculatorFunctions { get; }

    ICalculatorOperation GetCalculatorFunction(char mathOperator);
}

public class CalculatorFunctionFactory : ICalculatorFunctionFactory
{
    public IEnumerable<ICalculatorOperation> CalculatorFunctions => new ICalculatorOperation[]
    {
        new AddFunction(),
        new SubtractFunction()
    };

    public ICalculatorOperation GetCalculatorFunction(char mathOperator) => CalculatorFunctions
        .Single(_ => _.FunctionType == mathOperator);
}