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

namespace GraphLib.Domain
{
    public class UnweightedGraph : IGraph
    {
        public GraphDTO GraphIntance { get; private set; }

        static UnweightedGraph()
        {
            //Configures DTO <-> DB entity mapper on first initialization
            //Should be in the ctor param, injected, will do if i get time 
            GraphMapper.Configure();
        }

        public UnweightedGraph(/*IMapper mapper, ISerializer serializer, IDataStore dataStore*/)
        {
        }

        /// <summary>
        /// Loads Graph from DB
        /// </summary>
        /// <returns></returns>
        public GraphDTO LoadFromDB()
        {
            List<NodeDTO> dtos;

            using (var context = new GraphDBContext())
            {
                List<Node> dbNodes = context.Set<Node>().ToList();
                dtos = Mapper.Map<List<Node>, List<NodeDTO>>(dbNodes);
            }

            GraphIntance = new GraphDTO(dtos);

            return GraphIntance;
        }

        /// <summary>
        /// Loads a new graph onto this instance, from an enumerable collection of strings each representing a serialized node
        /// </summary>
        /// <param name="nodes"></param>
        public GraphDTO LoadFrom(IEnumerable<string> nodes)
        {
            //Read the serialized nodes
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
            foreach (var node in graphNodes.Where(n => n.AdjacentNodes.Ids.Any(a => a == n.NodeId)))
            {
                node.AdjacentNodes.Ids.Remove(node.NodeId);
            }

            //Validate for duplicate
            //Assuming it's not acceptable to have the same node (nodeID) defined twice
            if (graphNodes.GroupBy(n => n.NodeId).Any(g => g.Count() > 1))
            {
                string duplicatedIds = graphNodes
                    .GroupBy(n => n.NodeId)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key.ToString())
                    .Aggregate((a, b) => a + ", " + b);

                throw new Exception($"Found duplicated node definitions for nodes with IDs : {duplicatedIds}");
            }

            GraphIntance = new GraphDTO(graphNodes);

            return GraphIntance;
        }

        /// <summary>
        /// Persists the current Graph in this instance to the database deleting the previously persisted graph
        /// </summary>
        public void Persist()
        {
            //Map DTOs to DB nodes
            var dbNodes = Mapper.Map<List<NodeDTO>, List<Node>>(GraphIntance.Nodes);

            using (var context = new GraphDBContext())
            {
                context.Database.ExecuteSqlCommand("DELETE FROM Node");

                foreach (var node in dbNodes)
                {
                    context.Nodes.Add(node);
                }

                context.SaveChanges();
            }
        }
    }
}
