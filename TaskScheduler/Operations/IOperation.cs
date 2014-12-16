namespace TaskScheduler.Operations
{
    public interface IOperation
    {
        OperationResponse Execute(string parameters);
    }
}