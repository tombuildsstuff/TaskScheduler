namespace TaskScheduler.Operations
{
    public interface IOperationResolver
    {
        IOperation Resolve(string operationName);
    }
}