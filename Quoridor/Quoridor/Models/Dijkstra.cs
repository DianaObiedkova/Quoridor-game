using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quoridor.Models
{
    public class Vertex 
    {
        public string Name { get; set; }
        public int value;
        public bool IsChecked { get; set; }
        public Vertex PrevVertex { get; set; }

        public Vertex(string name, int value, bool IsChecked)
        {
            this.Name = name;
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

        public static void StartQuoridorDijkstra(Cell currentCell, Cell[] cells, Fence[] AllFences)
        {
            Vertices = new Vertex[cells.GetLength()];
            foreach(Cell cell in cells)
            {
                bool exists = Array.Exists(Vertices, x => x == null || x.Id == 0 || string.IsNullOrEmpty(x.Name));
                int index = 0;

                if (exists)
                {
                    index = Array.FindIndex(Vertices, i => i == null || i.Id == 0 || string.IsNullOrEmpty(i.Name));
                }
                else return false;

                Vertices[index] = new Vertex(cell.Name, INT_MAX, false);
            }

            //TODO:
            //отловить в Vertices currentCell в виде Vertex по имени и установить её value=0
            //Array.Find(Vertices, ).value = 0

            Edges = new Edge[AllFences.GetLength()*2];
            foreach(Fence fence in AllFences)
            {
                string f1name = fence.Name[1] + fence.Name[2];
                string s1name = fence.Name[3] + fence.Name[4];
                string f2name = fence.Name[5] + fence.Name[6];
                string s2name = fence.Name[7] + fence.Name[8];
 
                //TODO:
                //Vertex f1 = Array.Find(Vertices, ); // найти по vertex.Name == f1name
                //Vertex s1 = Array.Find(Vertices, ); //s1name
                //Edges = добавить сell в виде new Edge(f1, s1, 1)
                //Vertex f2 = Array.Find(Vertices, ); //f2name
                //Vertex s2 = Array.Find(Vertices, ); //s2name
                //Edges = добавить сell в виде new Edge(f2, s2, 1)
            }
        }

        //start.weight = 0
        //othervertices[index].weight = INT_MAX
        //allvertices[index].IsChecked = false
        public static void FindShortestPath(Vertex start)
        {
            if (!Vertices.Any() || !Edges.Any())
            {
                throw new Exception("dijkstra data error");
            }
            startVertex = start;

            //первая итерация
            IterateDijkstra(startVertex);
            foreach(Vertex vert in Vertices)
            {
                Vertex nextVertex = GetNearestUnchecked();
                if(nextVertex != null)
                {
                    IterateDijkstra(nextVertex);
                }
                else
                {
                    break;
                }
            }
        }
        static public void IterateDijkstra(Vertex currentVertex)
        {
            IEnumerable<Vertex> neigbours = GetNeighbours(currentVertex);
            foreach(Vertex item in neigbours)
            {
                if(item.IsChecked == false)
                {
                    int newvalue = currentVertex.value + GetEdge(item, currentVertex).Weight;
                    if(item.value > newvalue)
                    {
                        item.value = newvalue;
                        item.PrevVertex = currentVertex;
                    }
                    else
                    {

                    }
                }
            }
            currentVertex.IsChecked = true;
        }

        //last - точки нужного края поля, метод принимает по одной
        //если current.prevVertex == null, то пути нет(!)
        //если нет пути ни в одну точку по итогам вызова для всех клеток края, то установка стены запрещена
        public static List<Vertex> GetShortestPath(Vertex last)
        {
            List<Vertex> path = new List<Vertex>();
            Vertex current = last;
            while (current != startVertex)
            {
                path.Add(current);
                current = current.PrevVertex;
            }
            return path;
        }

        private static IEnumerable<Vertex> GetNeighbours(Vertex current)
        {
            IEnumerable<Vertex> first = from ed in Edges where ed.FirstVertex == current select ed.SecondVertex;
            IEnumerable<Vertex> second = from ed in Edges where ed.SecondVertex == current select ed.FirstVertex;
            IEnumerable<Vertex> result = first.Concat<Vertex>(second);
            return result;
        }

        private static Edge GetEdge(Vertex first, Vertex second)
        {
            IEnumerable<Edge> newEdges = from ed in Edges where (ed.FirstVertex == first & ed.SecondVertex == second) ||
                (ed.FirstVertex == second & ed.SecondVertex == first) select ed;
            if(newEdges.Count() == 0)
            {
                throw new Exception("edge not found"); //тогда путь не найден (?) 
                //нужно обработать в try catch, чтобы всё не падало
                //это нормальная ситуация, много других клеток поля могут быть недостижимы
            }
            else
            {
                return newEdges.First();
            }
        }

        private static Vertex GetNearestUnchecked()
        {
            IEnumerable<Vertex> @unchecked = from vert in Vertices where !vert.IsChecked select vert;
            if(!@unchecked.Any())
            {
                Vertex minVertex = @unchecked.First();
                int minValue = @unchecked.First().value;
                foreach (Vertex vert in @unchecked)
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
            {
                return null;
            }
        }
    }
}