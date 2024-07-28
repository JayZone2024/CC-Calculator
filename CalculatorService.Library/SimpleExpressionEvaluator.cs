namespace CalculatorService.Library;

public class SimpleExpressionEvaluator : IExpressionEvaluator
{
    private readonly ICalculatorFunctionFactory _calculatorFunctionFactory;
    private readonly IExpressionParser _expressionParser;

    public SimpleExpressionEvaluator()
    {
        _calculatorFunctionFactory = new CalculatorFunctionFactory();
        _expressionParser = new ExpressionParser(_calculatorFunctionFactory);
    }

    public decimal Evaluate(string expression)
    {
        var parsedExpression = _expressionParser.ParsedExpression(expression);
        var calculatorFunction = _calculatorFunctionFactory.GetCalculatorFunction(parsedExpression.Operation);

        var sum = calculatorFunction.ExecuteFunction(parsedExpression.Value1, parsedExpression.Value2);

        return sum;
    }
}

