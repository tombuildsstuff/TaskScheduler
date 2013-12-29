namespace TaskScheduler
{
    public class TaskConfiguration
    {
        public string Name { get; set; }
        public string CommandType { get; set; }
        public string CommandParameters { get; set; }
        public string Frequency { get; set; }
    }
}