using GraphLib.Domain;
using GraphLib.Domain.Serializers;
using GraphLib.DTOs;
using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace GraphServices
{
    /// <summary>
    /// Service to support loader
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class GraphApi : GraphServiceBase, IGraphApi
    {
        protected ISerializer<GraphDTO> Serializer;

        public GraphApi(IGraph graph, ISerializer<GraphDTO> serializer) : base(graph)
        {
            Serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "graph")]
        public string GetGraph()
        {
            return Serializer.Serialize(Graph.GraphIntance);
        }
    }
}
