namespace TaskScheduler.Operations
{
    public interface IOperation
    {
        ResponseStatus Execute(string parameters);
    }
}