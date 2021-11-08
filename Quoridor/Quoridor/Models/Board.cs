using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Quoridor.Models
{
    public class Board
    {
        public static int Size { get; set; } = 9;

        //сетка клеток размером Size x Size, вершины графа
        public readonly Cell[,] cells = new Cell[Size, Size];

        //ребра графа
        public Fence[] AllFences { get; set; } = new Fence[20];

        readonly string[] letters = { "a", "b", "c", "d", "e", "f", "g", "h", "i" };
        public readonly Dictionary<int, string> indexes = new Dictionary<int, string>();

        public Board()
        {
            for (int i = 0; i < letters.Length; i++)
            {
                indexes.Add(i, letters[i]);
            }
            InitCells();
        }
        public void InitCells()
        {
            for (int i = 0; i < cells.GetLength(1); i++)
            {
                for (int j = 0; j < cells.GetLength(0); j++)
                {
                    cells[i, j] = new Cell
                    {
                        Id = Size * j + i,
                        X = letters[j],
                        Y = i
                    };
                    cells[i, j].Name = letters[j].ToString() + i;
                }
            }
        }

        //fences check
        // пригодится для поиска по графу
        public bool IsMovePawnPossible(Cell currentCell, Direction direction)
        {
            switch (direction)
            {
                case Direction.South:
                    return !currentCell.SouthWall;
                case Direction.North:
                    return !currentCell.NorthWall;
                case Direction.West:
                    return !currentCell.WestWall;
                case Direction.East:
                    return !currentCell.EastWall;
                default:
                    break;
            }
            throw new ArgumentException("Wrong direction was chosen.");
        }

        public List<string> PossibleFences()
        {
            List<string> fences = new List<string>();
            for(int i = 0; i < cells.GetLength(1) - 1; i++)
            {
                for (int j = 0; j < cells.GetLength(0) - 1; j++)
                {
                    fences.Add(cells[i, j].Name + cells[i, j + 1].Name);
                    fences.Add(cells[i, j].Name + cells[i + 1, j].Name);
                }
            }

            foreach(var fence in AllFences)
            {
                for(int i=0;i<fences.Count;i++)
                {
                    if (!(fence is null) && fence.Name.Substring(1, 4) == fences[i])
                    {
                        fences.Remove(fences[i]);
                        if(fences[i][0] == fences[i][2])
                        {
                            fences.RemoveAll(f => fences[i][0] == fences[i][2] && 
                                (f.Contains(fences[i].Substring(0, 2)) || f.Contains(fences[i].Substring(2, 2))));
                            
                        }
                        if(fences[i][1] == fences[i][3])
                        {
                            fences.RemoveAll(f => fences[i][1] == fences[i][3] && 
                                (f.Contains(fences[i].Substring(0, 2)) || f.Contains(fences[i].Substring(2, 2))));
                            
                        }
                    }
                }
            }

            return fences;
        }
        //можно ли поставить перегородку?
        public bool IsFencePossible(string fenceName)
        {
            string firstpart = fenceName.Substring(1, 4);
            string secondpart = fenceName.Substring(5, 4);

            string crossfence = "";
            if (fenceName[0] == 'h')
            {
                crossfence += "v";

                //верхняя/левая половина
                crossfence += secondpart.Substring(0, 2);
                crossfence += firstpart.Substring(0, 2);
                //нижняя/правая половина
                crossfence += secondpart.Substring(2, 2);
                crossfence += firstpart.Substring(2, 2);
                
                
            }
            else
            {
                crossfence += "h";

                //нижняя/правая половина
                crossfence += firstpart.Substring(2, 2);
                crossfence += secondpart.Substring(2, 2);
                //верхняя/левая половина
                crossfence += firstpart.Substring(0, 2);
                crossfence += secondpart.Substring(0, 2);
                
            }
            ////верхняя/левая половина
            //crossfence += firstpart.Substring(0, 2);
            //crossfence += secondpart.Substring(0, 2);
            ////нижняя/правая половина
            //crossfence += firstpart.Substring(2, 2);
            //crossfence += secondpart.Substring(2, 2);

            //проверить есть ли уже такая в списке allfences
            if (Array.Exists(AllFences, x => x != null && (x.Name.Contains(fenceName) || x.Name.Equals(fenceName))))
            {
                return false;
            }
            //проверить есть ли половинка в allfences 
            else if (Array.Exists(AllFences, x => x != null && (x.Name.Contains(firstpart) || x.Name.Contains(secondpart))))
            {
                return false;
            }
            //проверить, нет ли перекрёстных
            else if (Array.Exists(AllFences, x => x != null && (x.Name.Contains(crossfence) || x.Name.Equals(crossfence))))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //X, Y - для координат перегородки
        //PawnCell1 - клетка с пешкой игрока, который ставит перегородку
        //PawnCell2 - клетка с пешкой игрока-противника
        public bool SetFence(Cell X, Cell Y, Cell PawnCell1, Cell PawnCell2)
        {

            //формирование имени Fence
            string newName;
            //наименование перегородок + ИД
            //горизонтальные
            if (X.Name.Substring(1, 1) == Y.Name.Substring(1, 1))
            {
                int cur_row = Convert.ToInt16(X.Name.Substring(1, 1));
                int next_row = cur_row + 1;
                newName = "h" + X.Name + X.Name.Substring(0, 1) + next_row.ToString() + Y.Name + Y.Name.Substring(0, 1) + next_row.ToString();
            }
            //вертикальные
            else if (X.Name.Substring(0, 1) == Y.Name.Substring(0, 1))
            {
                string cur_col = X.Name.Substring(0, 1);
                //столбец левее
                int ind = indexes.FirstOrDefault(x => x.Value == cur_col).Key;
                string next_col = indexes[ind - 1];//AllFences[Array.IndexOf(AllFences, cur_col) - 1];
                newName = "v" + next_col + X.Name.Substring(1, 1) + X.Name + next_col + Y.Name.Substring(1, 1) + Y.Name;
            }
            else
            {
                return false;
            }

            if (AllFences[0] != null && (!IsFencePossible(newName) || !DijkstraCheck(PawnCell1, PawnCell2, newName)))
            {
                return false;
            }
            //горизонтальные
            if (X.Name.Substring(1, 1) == Y.Name.Substring(1, 1))
            {
                for (int i = 0; i < cells.GetLength(0); i++)
                {
                    for (int j = 0; j < cells.GetLength(1); j++)
                    {
                        if (X.Name == cells[i, j].Name || Y.Name == cells[i, j].Name)
                        {
                            cells[i, j].SouthWall = true;
                            if (i < cells.GetLength(0) - 1)
                            {
                                cells[i + 1, j].NorthWall = true;
                            }
                        }
                    }
                }
            }
            //вертикальные
            else if (X.Name.Substring(0, 1) == Y.Name.Substring(0, 1))
            {
                for (int i = 0; i < cells.GetLength(0); i++)
                {
                    for (int j = 0; j < cells.GetLength(1); j++)
                    {
                        if (X.Name == cells[i, j].Name || Y.Name == cells[i, j].Name)
                        {
                            cells[i, j].WestWall = true;
                            if (j > 0)
                            {
                                cells[i, j - 1].EastWall = true;
                            }
                        }
                    }
                }
            }
            //ищу индекс первого пустого элемента в массиве

            bool exists = Array.Exists(AllFences, x => x == null);
            int index = 0;

            if (exists)
            {
                index = Array.FindIndex(AllFences, i => i == null);
            }
            else return false;

            AllFences[index] = new Fence()
            {
                Id = AllFences.Count(x => x != null),
                Name = newName
            };
            
            return true;
        }

        //top side - сторона противника?
        //bottom side - сторона игрока?
        public bool DijkstraCheck(Cell currentCell1, Cell currentCell2, string newFence)
        {
            List<Vertex> finalVertices1 = new List<Vertex>();
            List<Vertex> finalVertices2 = new List<Vertex>();

            bool result1 = false;
            bool result2 = false;

            //выполняется проверка в обе стороны для двух игроков         
            Vertex currentVertex1 = Dijkstra.StartQuoridorDijkstra(currentCell1, cells.Cast<Cell>().ToArray(), AllFences, newFence);

            //закинуть в finalCells нужный ряд клеток в виде Vertex, взяв их из Dijkstra.Vertices
            for (int i = 0; i < Size; i++)
            {
                Vertex sideVertex1 = Array.Find(Dijkstra.Vertices, v => v.Name == cells[0, i].Name);
                finalVertices1.Add(sideVertex1);
                Vertex sideVertex2 = Array.Find(Dijkstra.Vertices, v => v.Name == cells[Size-1, i].Name);
                finalVertices2.Add(sideVertex2);
            }

            foreach (Vertex final in finalVertices1)
            {
                Dijkstra.FindShortestPath(currentVertex1);
                bool local_res = Dijkstra.GetShortestPath(final);
                if(!local_res)
                    continue;
                result1 = true;
            }
            Vertex currentVertex2 = Dijkstra.StartQuoridorDijkstra(currentCell2, cells.Cast<Cell>().ToArray(), AllFences, newFence);
            foreach (Vertex final in finalVertices2)
            {
                Dijkstra.FindShortestPath(currentVertex2);
                bool local_res = Dijkstra.GetShortestPath(final);
                if(!local_res)
                    continue;
                result2 = true;
            }
            return (result1 && result2);
        }
    }
}
