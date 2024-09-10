﻿using CalculatorService.Library.CalculatorOperators;

namespace CalculatorService.Library.ExpressionParsers;

public interface ICalculatorOperationParser
{
    bool IsOperationDefined(CalculatorContext context);

    void ParseOperand(CalculatorContext context);
}

public class CalculatorOperationParser(IEnumerable<ICalculatorOperation> calculatorOperations) : ICalculatorOperationParser
{
    private const char OpenBracket = '(';

    public bool IsOperationDefined(CalculatorContext context)
    {
        var calculatorOperation = calculatorOperations.SingleOrDefault(_ => _.CanApply(context.NextOperand));

        return calculatorOperation != null;
    }

    public void ParseOperand(CalculatorContext context)
    {
        var currentOperation = (char)context.ExpressionReader!.Read();

        var calculatorOperation = calculatorOperations.Single(_ => _.CanApply(currentOperation));

        var operations = context.CalculatorOperations;
        var nextOperationOperand = (char)context.ExpressionReader.Peek();
        var a = calculatorOperations.SingleOrDefault(_ => _.CanApply(nextOperationOperand));

        var nextOperationPrecedence =
            calculatorOperations.SingleOrDefault(_ => _.CanApply(nextOperationOperand)) == null
                ? 0
                : calculatorOperations.SingleOrDefault(_ => _.CanApply(nextOperationOperand)).Precedence;


        bool Func() =>
            operations.Count > 0 &&
            operations.Peek() != OpenBracket &&
            !char.IsDigit(operations.Peek()) &&
            calculatorOperation.Precedence <= nextOperationPrecedence;

        EvaluateOperation(Func, context);

        context.CalculatorOperations.Push(currentOperation);
    }

    private static void EvaluateOperation(Func<bool> condition, CalculatorContext context)
    {
        var expressions = context.CalculatorExpressions;
        var operations = context.CalculatorOperations;

        while (condition())
        {
            var right = expressions.Pop();
            var left = expressions.Pop();

            expressions.Push(((Operation)operations.Pop()).Apply(left, right));
        }
    }
}