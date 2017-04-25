using System.ServiceModel;

namespace GraphServices
{
    [ServiceContract(Namespace = "Massive")]
    public interface IGraphDomain
    {
        /// <summary>
        /// Clients send the ids for two nodes and receive a json representation of the shorthest path on the graph
        /// </summary>
        /// <param name="jsonGraph"></param>
        /// <returns></returns>
        [OperationContract]
        string GetShortestPath(int node1, int node2);
    }
}