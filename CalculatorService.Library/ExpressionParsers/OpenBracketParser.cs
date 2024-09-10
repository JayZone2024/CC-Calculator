namespace CalculatorService.Library.ExpressionParsers;

public interface IOpenBracketParser
{
    bool IsOpenBracket(char operand);

    void ParseValue(CalculatorContext context);
}

public class OpenBracketParser : IOpenBracketParser
{
    private const char OpenBracket = '(';

    public bool IsOpenBracket(char operand) => operand.Equals(OpenBracket);

    public void ParseValue(CalculatorContext context)
    {
        context.ExpressionReader!.Read();

        context.CalculatorOperations.Push(OpenBracket);
    }
}