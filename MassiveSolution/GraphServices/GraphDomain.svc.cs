using GraphLib.Domain;
using System;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace GraphServices
{
    /// <summary>
    /// Service to support Thin Client
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class GraphDomain : GraphServiceBase, IGraphDomain
    {
        public GraphDomain(IGraph graph) : base(graph)
        {
        }

        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "graph")]
        public string GetShortestPath(int node1, int node2)
        {
            throw new NotImplementedException();
        }
    }
}
