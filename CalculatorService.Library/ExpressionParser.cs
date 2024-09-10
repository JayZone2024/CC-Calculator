using CalculatorService.Library.Exceptions;
using CalculatorService.Library.ExpressionParsers;

namespace CalculatorService.Library;

public interface IExpressionParser
{
    void Parse(CalculatorContext context);
}

public class ExpressionParser(
    ICalculatorOperationParser calculatorOperation,
    INumericValueParser numericValueParser,
    IOpenBracketParser openBracketParser,
    ICloseBracketParser closeBracketParser) : IExpressionParser
{
    public void Parse(CalculatorContext context)
    {
        int peek;
        var reader = context.ExpressionReader!;

        while ((peek = reader.Peek()) > -1)
        {
            var nextOperand = (char)peek;

            if (numericValueParser.IsNumericValue(nextOperand, context, out var number))
            {
                numericValueParser.ParseValue(context, number!.Value);
                continue;
            }

            if (calculatorOperation.IsOperationDefined(nextOperand))
            {
                calculatorOperation.ParseOperand(context);
                continue;
            }

            if (openBracketParser.IsOpenBracket(nextOperand))
            {
                openBracketParser.ParseValue(context);
                continue;
            }

            if (closeBracketParser.IsCloseBracket(nextOperand))
            {
                closeBracketParser.ParseValue(context);
                continue;
            }

            throw new UnknownOperandException(
                nextOperand,
                $"Invalid operand '{nextOperand}' detected in expression.");
        }
    }
}

