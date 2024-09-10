namespace CalculatorService.Library.CalculatorOperators;

public interface ICalculatorOperationFactory
{
    bool DoesOperationExists(char operationType);

    ICalculatorOperation? GetCalculatorOperation(char operationType);
}

public class CalculatorOperationFactory(
    IEnumerable<ICalculatorOperation> calculatorOperations) : ICalculatorOperationFactory
{
    public bool DoesOperationExists(char operationType) =>
        calculatorOperations.SingleOrDefault(_ => _.CanApply(operationType)) != null;

    public ICalculatorOperation? GetCalculatorOperation(char operationType) =>
        calculatorOperations.SingleOrDefault(_ => _.CanApply(operationType));
}