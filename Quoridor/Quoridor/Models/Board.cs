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
        public readonly Cell[,] cells = new Cell[Size,Size];

        //ребра графа
        public Fence[] AllFences { get; set; } = new Fence[20];

        readonly string[] letters = { "a","b","c","d", "e", "f", "g", "h", "i" };
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
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
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
            //if (Direction.South)
            //    return NoCellSouthFence(currentCell);
            //else if (Direction.North)
            //    return NoCellNorthFence(currentCell);
            //else if (Direction.West)
            //    return NoCellWestFence(currentCell);
            //else if (Direction.East)
            //    return NoCellEastFence(currentCell);
            //else
            //    Debug.LogError("CanMovePawn error");
            //    return false;
        }
        //private bool NoCellSouthFence(Cell currentCell)
        //{
        //    return !(currentCell.SouthWall);
        //} 
        //private bool NoCellNorthFence(Cell currentCell)
        //{
        //    return !(currentCell.NorthWall);
        //} 
        //private bool NoCellWestFence(Cell currentCell)
        //{
        //    return !(currentCell.WestWall);
        //} 
        //private bool NoCellEastFence(Cell currentCell)
        //{
        //    return !(currentCell.EastWall);
        //}

        //IsMovePawnPossible - равноценная замена
       

        //pawn check
        /*private bool NoCellPawn(Cell currentCell)
        {
            return !(currentCell.hasPawn());    
        }*/

        //можно ли поставить перегородку?
        public bool IsFencePossible()
        {
            //проверить есть ли уже такая в списке allfences
            return default;
        }

        public bool SetFence(Cell X, Cell Y)
        {
            //передавать параметры в IsFencePossible() DijkstraCheck()
            //нужно формировать имя для Fence ещё раньше(?) для идентификации
            if(!IsFencePossible() || !DijkstraCheck())
            {
                return false;
            }
            //ищу индекс первого пустого элемента в массиве

            bool exists = Array.Exists(AllFences, x => x == null || x.Id == 0 || string.IsNullOrEmpty(x.Name));
            int index = 0;

            if (exists)
            {
                index = Array.FindIndex(AllFences, i => i == null || i.Id == 0 || string.IsNullOrEmpty(i.Name));
            }
            else return false;

            //наименование перегородок + ИД
            //горизонтальные
            if (X.Name.Substring(1, 1) == Y.Name.Substring(1, 1))
            {
                int cur_row = Convert.ToInt16(X.Name.Substring(1, 1));
                int next_row = cur_row + 1;
                AllFences[index] = new Fence()
                {
                    Id = AllFences.Count(x => x != null),
                    Name = "h" + X.Name + X.Name.Substring(0, 1) + next_row.ToString() + Y.Name + Y.Name.Substring(0, 1) + next_row.ToString()
                };
            }
            //вертикальные
            else if (X.Name.Substring(0, 1) == Y.Name.Substring(0, 1))
            {
                string cur_col = X.Name.Substring(0, 1);
                //столбец левее
                string next_col = AllFences[Array.IndexOf(AllFences, cur_col) - 1];
                AllFences[index] = new Fence()
                {
                    Id = AllFences.Count(x => x != null),
                    Name = "v" + next_col + X.Name.Substring(1, 1) + X.Name + next_col + Y.Name.Substring(1, 1) + Y.Name
                };
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool DijkstraCheck(Cell currentCell)
        {
            ////Dijkstra.Vertices = клетки Cells в виде new Vertex(INT_MAX, false)
            ////возможно, добавить в Vertex свойство Name для идентификации
            ////отловить в Dijkstra.Vertices currentCell в виде Vertex и установить её value=0
            ////Dijkstra.Edges = добавлять new Edge(f, s, 1), где
            ////f берём из Dijkstra.Vertices по имени1
            ////s берём Dijkstra.Vertices по имени2
            ////имя1 - надо вытащить имя клетки из имени перегородки(?)
            ////имя2 - надо вытащить имя клетки из имени перегородки(?)
            ////перегородка касается 4 клеток, поэтому 1 перегородка == 2 ребра в графе
            //Dijkstra.FindShortestPath(currentCell);
            //List<Vertex> finalCells = new List<Vertex>();
            ////закинуть в finalCells нужный ряд клеток в виде Vertex, взяв их из Dijkstra.Vertices(!)
            //foreach (Vertex final in finalCells)
            //{
            //    try
            //    {
            //        Dijkstra.GetShortestPath(final);
            //    }
            //    catch (NullReferenceException e)
            //    {
            //        continue;
            //    }
            //    return true;
            //}
            return false;
        }
    }
}
