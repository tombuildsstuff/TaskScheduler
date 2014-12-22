namespace TaskScheduler.Services.Files
{
    public interface IFileService
    {
        string GetContents(string filePath);
    }
}