﻿using GraphLib.Domain;
using GraphLib.Domain.Serializers;
using GraphLib.DTOs;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace GraphServices
{
    /// <summary>
    /// Service to support thin Client
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class GraphDataServices : GraphServiceBase, IGraphDataServices
    {
        public GraphDataServices(IGraph graph) : base(graph)
        {
        }

        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "graph")]
        public void LoadGraph(IEnumerable<string> graph)
        {
            var graphDTO = Graph.LoadFrom(graph);
        }
    }
}
