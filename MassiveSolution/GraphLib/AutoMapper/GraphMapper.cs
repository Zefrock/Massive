using AutoMapper;
using GraphLib.DTOs;
using GraphLib.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLib.AutoMapper
{
    public static class GraphMapper
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                //Map between DTo and DB entity
                cfg.CreateMap<NodeDTO, Node>()
                .ForMember(
                    destination => destination.AdjacentNodes,
                    options => options.ResolveUsing(
                        source =>
                        {
                            var adjNodes = source.AdjacentNodes.Ids.Select(adjId => new AdjacentNode { NodeId = source.NodeId, AdjacentNodeId = adjId });
                            var nodes = new HashSet<AdjacentNode>(adjNodes);
                            return nodes;
                        })
                )
                .ReverseMap()
                .ForMember(
                    destination => destination.AdjacentNodes,
                    options => options.ResolveUsing(source =>
                    {
                        var adjNodesIds = source.AdjacentNodes.Select(node => node.AdjacentNodeId).ToList();
                        var adjacentNodesDTO = new AdjacentNodesDTO { Ids = adjNodesIds };
                        return adjacentNodesDTO;
                    }));

                ////Map between collections of DTOs and DB entities
                //cfg.CreateMap<List<NodeDTO>, List<Node>>()
                //    .ReverseMap();
            });

            ////Validate configuration, usually sits in a unit test
            Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}
