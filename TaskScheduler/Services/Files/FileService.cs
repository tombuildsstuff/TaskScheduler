using System.IO;

namespace TaskScheduler.Services.Files
{
    public class FileService : IFileService
    {
        public string GetContents(string filePath)
        {
            return File.OpenText(filePath).ReadToEnd();
        }
    }
}