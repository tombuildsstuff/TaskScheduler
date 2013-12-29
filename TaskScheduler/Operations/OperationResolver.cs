using System.Collections.Generic;

namespace TaskScheduler.Operations
{
    public class OperationResolver : IOperationResolver
    {
        private readonly Dictionary<string, IOperation> _operationList;

        public OperationResolver()
        {
            _operationList = new Dictionary<string, IOperation>()
            {
                {"Post", new PostOperation()}
            };
        }
            
        public IOperation Resolve(string operationName)
        {
            return _operationList[operationName];
        }
    }
}