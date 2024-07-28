using CalculatorService.Library.Functions;

namespace CalculatorService.Library;

public interface ICalculatorFunctionFactory
{
    IEnumerable<ICalculatorFunction> CalculatorFunctions { get; }

    ICalculatorFunction GetCalculatorFunction(char mathOperator);
}

public class CalculatorFunctionFactory : ICalculatorFunctionFactory
{
    public IEnumerable<ICalculatorFunction> CalculatorFunctions => new ICalculatorFunction[]
    {
        new AddFunction(),
        new SubtractFunction()
    };

    public ICalculatorFunction GetCalculatorFunction(char mathOperator) => CalculatorFunctions
        .Single(_ => _.FunctionType == mathOperator);
}