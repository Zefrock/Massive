using System.ServiceModel;

namespace GraphServices
{
    [ServiceContract(Namespace = "Massive")]
    public interface IGraphApi
    {
        [OperationContract]
        string GetGraph();
    }
}
