using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quoridor.Models
{
    public class Vertex 
    {
        public int value;
        public bool IsChecked { get; set; }
        public Vertex PrevVertex { get; set; }

        public Vertex(int value, bool IsChecked)
        {
            this.value = value;
            this.IsChecked = IsChecked;
            PrevVertex = new Vertex();
        }
        public Vertex()
        {

        }
    }

    public class Edge
    {
        public Vertex FirstVertex { get; private set; }
        public Vertex SecondVertex { get; private set; }
        public int Weight { get; private set; }

        public Edge(Vertex first, Vertex second, int weightValue)
        {
            FirstVertex = first;
            SecondVertex = second;
            Weight = weightValue;
        }
    }

    public static class Dijkstra
    {
        public static Vertex[] Vertices { get; private set; }
        public static Edge[] Edges { get; private set; }
        public static Vertex startVertex;

        //public Dijkstra(Vertex[] graphVertices, Edge[] graphEdges)
        //{
        //    vertices = graphVertices;
        //    edges = graphEdges;
        //}

        //start.weight = 0
        //othervertices[index].weight = INT_MAX
        //allvertices[index].IsChecked = false
        public static int FindShortestPathLength(Vertex start)
        {
            if (Vertices.Any() || Edges.Any())
            {
                throw new Exception("dijkstra data error");
            }
            startVertex = start;

            //первая итерация
            IterateDijkstra(startVertex);
            //в цикл foreach vert in vertices
            //Vertex nextVertex = ближайшая к ней unchecked // = GetNearestUnchecked()
            /*if(nextVertex != null)
            {
                IterateDijkstra(nextVertex);
            }
            else
                break;
            */
            //if all vertices are checked, exit/break

            return default;
        }
        static public void IterateDijkstra(Vertex currentVertex)
        {
            //todo:
            //получить neigbours = GetNeighbours(currentVertex)
            //пройтись по ним foreach item in neigbours:
            //  if(item.IsChecked==false)
            //  {
            //      int newvalue = currentVertex.value + GetEdge(item, currentVertex).weight
            //      если item.value > newvalue
            //          то назначить для item.value = newwalue, item.prevVertex = currentVertex
            //      иначе else {} //ничего не делать
            //  }
            
            //в конце
            currentVertex.IsChecked = true;
        }

        //todo: метод для получения списка соседей GetNeighbours(Vertex current): 
        //  Concat списков (secondVertex где у ребер firstVertex==current)+(firstVertex где у ребер secondVertex==current)

        //todo: поиск ребра между двумя вершинами GetEdge(Vertex first, Vertex second) returns Edge

        /*private Vertex GetNearestUnchecked()
        {
            //список Vertex unchecked = выбрать vert из this.vertices где vert.IsChecked == false
            if(unchecked.Count != 0)
            {
                Vertex minVertex = unchecked[0]; // нужен первый элемент
                int minValue = unchecked[0].value; //нужен первый элемент
                foreach (Vertex vert in unchecked)
                {
                    if(vert.value < minValue)
                    {
                        minValue = vert.value;
                        minVertex = vert;
                    }
                }
                return minVertex;
            }
            else
                return null;
        }*/
    }
}