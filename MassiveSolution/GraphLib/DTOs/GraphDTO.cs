using GraphLib.DTOs;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GraphLib.DTOs
{
    [DataContract(Name = "Graph")]
    public class GraphDTO
    {
        public GraphDTO(List<NodeDTO> nodes = null)
        {
            //empty graph
            if (nodes == null)
                Nodes = new List<NodeDTO>();

            Nodes = nodes;
        }

        [DataMember]
        public List<NodeDTO> Nodes;
    }
}
