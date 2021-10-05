using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quoridor.Models
{
    public class Vertex 
    {
        public int value
        public bool IsChecked { get; set; }
        public Vertex prevVertex { get; set; }

        public Vertex(int value, bool IsChecked)
        {
            this.value = value;
            this.IsChecked = IsChecked;
            prevVertex = new Vertex();
        }
        public Vertex()
        {

        }
    }

    public class Edge
    {
        public Vertex firstVertex { get; private set; }
        public Vertex secondVertex { get; private set; }
        public int weight { get; private set; }

        public Edge(Vertex first, Vertex second, int weightValue)
        {
            firstVertex = first;
            secondVertex = second;
            weight = weightValue;
        }
    }

    public static class Dijkstra
    {
        public Vertex[] vertices { get; private set; }
        public Edge[] edges { get; private set; }
        public Vertex startVertex

        public Dijkstra(Vertex[] graphVertices, Edge[] graphEdges)
        {
            vertices = graphVertices;
            edges = graphEdges;
        }

        public static int FindShortestPathLength(Vertex start)
        {
            if (this.vertices.Count() == 0 || this.edges.Count() == 0)
            {
                throw new Exception("dijkstra data error");
            }
            startVertex = start;
            IterateDijkstra(startVertex);

        }
        public void IterateDijkstra()
        {
            
        }
    }
}