using System.Collections.Generic;
using System.ServiceModel;

namespace GraphServices
{
    [ServiceContract(Namespace = "Massive")]
    public interface IGraphDataServices
    {
        [OperationContract]
        void LoadGraph(IEnumerable<string> graph);
    }
}
