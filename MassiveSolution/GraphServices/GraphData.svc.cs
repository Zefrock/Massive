using GraphLib.Domain;
using GraphLib.Domain.Serializers;
using GraphLib.DTOs;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace GraphServices
{
    /// <summary>
    /// Service to support Loader
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class GraphData : GraphServiceBase, IGraphData
    {
        public GraphData(IGraph graph) : base(graph)
        {
        }

        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "graph")]
        public void LoadGraph(IEnumerable<string> graph)
        {
            var graphDTO = Graph.LoadFrom(graph);
        }
    }
}