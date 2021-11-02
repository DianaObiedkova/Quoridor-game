using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Quoridor.Models
{
    public class Vertex 
    {
        public int Id { get; set; }  //добавлено для StartQuoridorDijkstra
        public string Name { get; set; }
        public int value;
        public bool IsChecked { get; set; }
        public Vertex PrevVertex { get; set; }

        public Vertex(string name, int value, bool IsChecked)
        {
            this.Name = name;
            this.value = value;
            this.IsChecked = IsChecked;
            PrevVertex = null;
        }
        public Vertex()
        {

        }

        public void resetVertex()
        {
            this.value = int.MaxValue;
            this.IsChecked = false;
            this.PrevVertex = null;
        }
    }

    public class Edge
    {
        public int Id { get; set; }
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

        public static Vertex StartQuoridorDijkstra(Cell currentCell, Cell[] cells, Fence[] AllFences, string newFenceName)
        {
            Vertices = new Vertex[cells.Length];
            foreach(Cell cell in cells)
            {
                bool exists = Array.Exists(Vertices, x => x == null);
                int index = 0;

                if (exists)
                {
                    index = Array.FindIndex(Vertices, i => i == null);
                }
                else throw new ArgumentException("Index is not found.");//return false; не возвращаем, так как void
                //уже не void

                Vertices[index] = new Vertex(cell.Name, int.MaxValue, false);
            }

            //отловить в Vertices currentCell в виде Vertex по имени и установить её value=0
            startVertex = Array.Find(Vertices, v => v.Name == currentCell.Name);
            startVertex.value = 0;

            Edges = new Edge[144]; //9*8*2
            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    if(i!=8) //если не последний ряд
                    {
                        bool exists = Array.Exists(Edges, x => x == null);
                        int index = 0;
                        if (exists)
                            index = Array.FindIndex(Edges, i => i == null);
                        else throw new ArgumentException("Index is not found.");
                        Edges[index] = new Edge(Vertices[i*9+j], Vertices[i*9+j+9], 1); //добавить ребро к клетке снизу
                    }
                    if(j!=8) //если не последний столбец
                    {
                        bool exists = Array.Exists(Edges, x => x == null);
                        int index = 0;
                        if (exists)
                            index = Array.FindIndex(Edges, i => i == null);
                        else throw new ArgumentException("Index is not found.");
                        Edges[index] = new Edge(Vertices[i*9+j], Vertices[i*9+j+1], 1); //добавить ребро к клетке справа
                    }
                }
            }

            foreach(Fence fence in AllFences)
            {
                if(fence == null)
                    continue;
                string f1name = fence.Name.Substring(1, 2); //точно ли начинаем с индекса 1, или все-таки 0?
                //точно, там первый символ v/h 
                string s1name = fence.Name.Substring(3, 2);
                string f2name = fence.Name.Substring(5, 2);
                string s2name = fence.Name.Substring(7, 2);
 
                Vertex f1 = Array.Find(Vertices, v => v.Name == f1name);
                Vertex s1 = Array.Find(Vertices, v => v.Name == s1name);
                //Edges = удалить сell new Edge(f1, s1, 1)
                Edge deleted1 = Array.Find(Edges, e => (e != null && (e.FirstVertex == f1 && e.SecondVertex == s1)) || (e != null && (e.FirstVertex == s1 && e.SecondVertex == f1)));
                int ind1 = Array.IndexOf(Edges, deleted1);
                Edges[ind1] = null;
                deleted1 = null;
                
                Vertex f2 = Array.Find(Vertices, v => v.Name == f2name);
                Vertex s2 = Array.Find(Vertices, v => v.Name == s2name);
                //Edges = удалить сell new Edge(f2, s2, 1)
                Edge deleted2 = Array.Find(Edges, e => (e != null && (e.FirstVertex == f2 && e.SecondVertex == s2)) || (e != null &&  (e.FirstVertex == s2 && e.SecondVertex == f2)));
                int ind2 = Array.IndexOf(Edges, deleted2);
                Edges[ind2] = null;
                deleted2 = null;
            }
            //удаление текущей перегородки
            string f1new = newFenceName.Substring(1, 2);
            string s1new = newFenceName.Substring(3, 2);
            string f2new = newFenceName.Substring(5, 2);
            string s2new = newFenceName.Substring(7, 2);
 
            Vertex f1n = Array.Find(Vertices, v => v.Name == f1new);
            Vertex s1n = Array.Find(Vertices, v => v.Name == s1new);

            Edge deleted1new = Array.Find(Edges, e => (e != null && (e.FirstVertex == f1n && e.SecondVertex == s1n)) || (e != null && (e.FirstVertex == s1n && e.SecondVertex == f1n)));
            int ind1new = Array.IndexOf(Edges, deleted1new);
            Edges[ind1new] = null;
            deleted1new = null;
              
            Vertex f2n = Array.Find(Vertices, v => v.Name == f2new);
            Vertex s2n = Array.Find(Vertices, v => v.Name == s2new);

            Edge deleted2new = Array.Find(Edges, e => (e != null && (e.FirstVertex == f2n && e.SecondVertex == s2n)) || (e != null &&  (e.FirstVertex == s2n && e.SecondVertex == f2n)));
            int ind2new = Array.IndexOf(Edges, deleted2new);
            Edges[ind2new] = null;
            deleted2new = null;

            return startVertex;
        }

        public static Vertex StartAIDijkstra(Cell currentCell, Cell[] cells, Fence[] AllFences)
        {
            Vertices = new Vertex[cells.Length];
            foreach(Cell cell in cells)
            {
                bool exists = Array.Exists(Vertices, x => x == null);
                int index = 0;

                if (exists)
                {
                    index = Array.FindIndex(Vertices, i => i == null);
                }
                else throw new ArgumentException("Index is not found.");

                Vertices[index] = new Vertex(cell.Name, int.MaxValue, false);
            }

            //отловить в Vertices currentCell в виде Vertex по имени и установить её value=0
            startVertex = Array.Find(Vertices, v => v.Name == currentCell.Name);
            startVertex.value = 0;

            Edges = new Edge[144]; //9*8*2
            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    if(i!=8) //если не последний ряд
                    {
                        bool exists = Array.Exists(Edges, x => x == null);
                        int index = 0;
                        if (exists)
                            index = Array.FindIndex(Edges, i => i == null);
                        else throw new ArgumentException("Index is not found.");
                        Edges[index] = new Edge(Vertices[i*9+j], Vertices[i*9+j+9], 1); //добавить ребро к клетке снизу
                    }
                    if(j!=8) //если не последний столбец
                    {
                        bool exists = Array.Exists(Edges, x => x == null);
                        int index = 0;
                        if (exists)
                            index = Array.FindIndex(Edges, i => i == null);
                        else throw new ArgumentException("Index is not found.");
                        Edges[index] = new Edge(Vertices[i*9+j], Vertices[i*9+j+1], 1); //добавить ребро к клетке справа
                    }
                }
            }

            foreach(Fence fence in AllFences)
            {
                if(fence == null)
                    continue;
                string f1name = fence.Name.Substring(1, 2); //точно ли начинаем с индекса 1, или все-таки 0?
                //точно, там первый символ v/h 
                string s1name = fence.Name.Substring(3, 2);
                string f2name = fence.Name.Substring(5, 2);
                string s2name = fence.Name.Substring(7, 2);
 
                Vertex f1 = Array.Find(Vertices, v => v.Name == f1name);
                Vertex s1 = Array.Find(Vertices, v => v.Name == s1name);
                //Edges = удалить сell new Edge(f1, s1, 1)
                Edge deleted1 = Array.Find(Edges, e => (e != null && (e.FirstVertex == f1 && e.SecondVertex == s1)) || (e != null && (e.FirstVertex == s1 && e.SecondVertex == f1)));
                int ind1 = Array.IndexOf(Edges, deleted1);
                Edges[ind1] = null;
                deleted1 = null;
                
                Vertex f2 = Array.Find(Vertices, v => v.Name == f2name);
                Vertex s2 = Array.Find(Vertices, v => v.Name == s2name);
                //Edges = удалить сell new Edge(f2, s2, 1)
                Edge deleted2 = Array.Find(Edges, e => (e != null && (e.FirstVertex == f2 && e.SecondVertex == s2)) || (e != null &&  (e.FirstVertex == s2 && e.SecondVertex == f2)));
                int ind2 = Array.IndexOf(Edges, deleted2);
                Edges[ind2] = null;
                deleted2 = null;
            }

            return startVertex;
        }

        //start.weight = 0
        //othervertices[index].weight = INT_MAX
        //allvertices[index].IsChecked = false
        public static void FindShortestPath(Vertex start)
        {
            if (!Vertices.Any() || !Edges.Any())
            {
                throw new ArgumentException("dijkstra data error");
            }
            startVertex = start;

            //reset for a new path //WARNING: resets all values to int.MaxValue
            foreach(Vertex vert in Vertices)
            {
                vert.resetVertex();
            }
            startVertex.value = 0;

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
                if(!item.IsChecked)
                {
                    int newvalue = currentVertex.value + GetEdge(item, currentVertex).Weight;
                    if(item.value > newvalue)
                    {
                        item.value = newvalue;

                        Vertex temp_curr = Array.Find(Vertices, v => v.Name == currentVertex.Name);
                        int ind = Array.IndexOf(Vertices, temp_curr);
                        item.PrevVertex = Vertices[ind];
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
        //alt: public static List<Vertex> GetShortestPath(Vertex last)
        //     return path;
        public static bool GetShortestPath(Vertex last)
        {
            List<Vertex> path = new List<Vertex>();
            Vertex temp_curr = Array.Find(Vertices, v => v.Name == last.Name);
            int ind = Array.IndexOf(Vertices, temp_curr);
            Vertex current = Vertices[ind];
            while (current != startVertex)
            {
                if(current == null){
                    return false;
                }
                path.Add(current);
                current = current.PrevVertex;
            }
            return true;
        }

        public static int GetShortestPathLength(Vertex last)
        {
            List<Vertex> path = new List<Vertex>();
            Vertex temp_curr = Array.Find(Vertices, v => v.Name == last.Name);
            int ind = Array.IndexOf(Vertices, temp_curr);
            Vertex current = Vertices[ind];
            while (current != startVertex)
            {
                path.Add(current);
                if(current == null){
                    return -1;
                }
                current = current.PrevVertex;
            }
            return path.Count;
        }

        private static IEnumerable<Vertex> GetNeighbours(Vertex current)
        {
            IEnumerable<Vertex> first = from ed in Edges where (ed != null && ed.FirstVertex == current) select ed.SecondVertex;
            IEnumerable<Vertex> second = from ed in Edges where (ed != null && ed.SecondVertex == current) select ed.FirstVertex;
            IEnumerable<Vertex> result = first.Concat<Vertex>(second);
            return result;
        }

        private static Edge GetEdge(Vertex first, Vertex second)
        {
            IEnumerable<Edge> newEdges = from ed in Edges where (ed != null && ed.FirstVertex == first && ed.SecondVertex == second) ||
                (ed != null && ed.FirstVertex == second && ed.SecondVertex == first) select ed;
            if(!newEdges.Any())
            {
                throw new ArgumentException("edge not found"); //тогда путь не найден (?) 
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
            IEnumerable<Vertex> @unchecked = from vert in Vertices where vert.IsChecked == false select vert;
            if(@unchecked.Any())
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