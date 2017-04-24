using GraphLib.Domain;
using System;

namespace GraphServices
{
    public abstract class GraphServiceBase
    {
        protected  IGraph Graph;

        public GraphServiceBase(IGraph graph)
        {
            Graph = graph ?? throw new ArgumentNullException(nameof(graph));
        }
    }
}
