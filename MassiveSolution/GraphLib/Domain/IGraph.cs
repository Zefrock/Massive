using System.Collections.Generic;
using GraphLib.DTOs;

namespace GraphLib.Domain
{
    public interface IGraph
    {
        GraphDTO GraphIntance { get; }
        GraphDTO LoadFromDB();
        GraphDTO LoadFrom(IEnumerable<string> nodes);
        void Persist();
    }
}