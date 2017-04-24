using GraphLib.DTOs;
using GraphLib.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GraphLoader
{
    public class Graph
    {
        public GraphDTO GraphDTO { get; private set; }

        public void Load(IEnumerable<string> nodes)
        {
            List<NodeDTO> graphNodes = new List<NodeDTO>();

            foreach (var node in nodes)
            {
                using (var reader = new StringReader(node))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(NodeDTO));
                    var nodeDTO = (NodeDTO)serializer.Deserialize(reader);
                    graphNodes.Add(nodeDTO);
                }
            }

            var duplicates = graphNodes
                .GroupBy(n => n.NodeId)
                .Where(g => g.Count() > 1)
                .ToList();

            if (duplicates.Count() > 0)
                throw new Exception($"Found duplicated node definitions for nodes with IDs : { duplicates.Select(g => g.Key.ToString()).Aggregate((a, b) => a + ", " + b)}");

            GraphDTO = new GraphDTO(graphNodes);
        }
    }
}
