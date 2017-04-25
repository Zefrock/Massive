using GraphLib.DTOs;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

namespace GraphLib.DTOs
{
    [DataContract(Name = "Graph")]
    public class GraphDTO
    {
        public GraphDTO(IEnumerable<NodeDTO> nodes = null)
        {
            //empty graph
            if (nodes == null)
                Nodes = new List<NodeDTO>();

            Nodes = nodes.OrderBy(n => n.NodeId).ToList();
        }

        [DataMember]
        public List<NodeDTO> Nodes;
    }
}
