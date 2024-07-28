namespace CalculatorService.Library;

public interface IExpressionEvaluator
{
    decimal Evaluate(string expression);
}