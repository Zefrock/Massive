using AutoMapper;
using GraphLib.AutoMapper;
using GraphLib.DBModel;
using GraphLib.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Xml.Serialization;

namespace GraphLoader
{
    public class Graph
    {
        public GraphDTO GraphDTO { get; private set; }

        static Graph()
        {
            GraphMapper.Configure();
        }

        /// <summary>
        /// Loads a new graph onto this instance, from an enumerable collection of strings each representing a serialized node
        /// </summary>
        /// <param name="nodes"></param>
        public GraphDTO Load(IEnumerable<string> nodes)
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

            //Assuming it's ok to remove self referencing nodes
            foreach(var node in graphNodes.Where(n => n.AdjacentNodes.Ids.Any(a => a == n.NodeId)))
            {
                node.AdjacentNodes.Ids.Remove(node.NodeId);
            }

            //Validate for duplicate
            //Assuming it's not acceptable to have the same node (nodeID) defined twice
            var duplicates = graphNodes
                .GroupBy(n => n.NodeId)
                .Where(g => g.Count() > 1)
                .ToList();

            if (duplicates.Count() > 0)
                throw new Exception($"Found duplicated node definitions for nodes with IDs : { duplicates.Select(g => g.Key.ToString()).Aggregate((a, b) => a + ", " + b)}");

            GraphDTO = new GraphDTO(graphNodes);

            return GraphDTO;
        }

        /// <summary>
        /// Persists the current Graph in this instance to the database deleting the previously persisted graph
        /// </summary>
        public void Persist()
        {
            //Map DTOs to DB nodes
            var dbNodes = Mapper.Map<List<NodeDTO>, List<Node>>(GraphDTO.Nodes);

            using (var context = new GraphDBContext())
            {
                context.Database.ExecuteSqlCommand("DELETE FROM Node");
                
                foreach(var node in dbNodes)
                {
                    context.Nodes.Add(node);
                }

                context.SaveChanges();
            }
        }
    }
}
