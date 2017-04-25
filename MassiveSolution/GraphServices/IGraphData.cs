using System.Collections.Generic;
using System.ServiceModel;

namespace GraphServices
{
    [ServiceContract(Namespace = "Massive")]
    public interface IGraphData
    {
        [OperationContract]
        void LoadGraph(IEnumerable<string> graph);
    }
}
