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
        public bool IsFencePossible(string fenceName)
        {
            //проверить есть ли уже такая в списке allfences
            if(Array.Exists(AllFences, x => x.Name.Contains(fenceName)||x.Name.Equals(fenceName)))
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
                int ind = indexes.FirstOrDefault(x => x.Value == X.X).Key;
                string next_col = indexes[ind - 1];//AllFences[Array.IndexOf(AllFences, cur_col) - 1];
                newName = "v" + next_col + X.Name.Substring(1, 1) + X.Name + next_col + Y.Name.Substring(1, 1) + Y.Name;
            }
            else
            {
                return false;
            }

            if(AllFences[0] != null)
            {
                if(!IsFencePossible(newName) || !DijkstraCheck(PawnCell1, PawnCell2)) 
                {
                    return false;
                }
            }
                        
            //ищу индекс первого пустого элемента в массиве

            bool exists = Array.Exists(AllFences, x => x == null || x.Id == 0 || string.IsNullOrEmpty(x.Name));
            int index = 0;

            if (exists)
            {
                index = Array.FindIndex(AllFences, i => i == null || i.Id == 0 || string.IsNullOrEmpty(i.Name));
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
        public bool DijkstraCheck(Cell currentCell1, Cell currentCell2)
        {
            List<Vertex> finalVertices1 = new List<Vertex>();
            List<Vertex> finalVertices2 = new List<Vertex>();
            
            bool result = false;

            //выполняется проверка в обе стороны для двух игроков         
            Vertex currentVertex1 = Dijkstra.StartQuoridorDijkstra(currentCell1, cells.Cast<Cell>().ToArray(), AllFences);

            //закинуть в finalCells нужный ряд клеток в виде Vertex, взяв их из Dijkstra.Vertices
            for(int i=0; i<Size; i++) 
            {
                Vertex sideVertex1 = Array.Find(Dijkstra.Vertices, v => v.Name == cells[0, i].Name);
                finalVertices1.Add(sideVertex1);
                Vertex sideVertex2 = Array.Find(Dijkstra.Vertices, v => v.Name == cells[Size-1, i].Name);
                finalVertices2.Add(sideVertex2);
            }

            Dijkstra.FindShortestPath(currentVertex1);
            foreach (Vertex final in finalVertices1)
            {
                try
                {
                    Dijkstra.GetShortestPath(final);
                }
                catch (NullReferenceException e)
                {
                    continue;
                }
                result = true;
            }
            Vertex currentVertex2 = Dijkstra.StartQuoridorDijkstra(currentCell2, cells.Cast<Cell>().ToArray(), AllFences);
            Dijkstra.FindShortestPath(currentVertex2);
            foreach (Vertex final in finalVertices2)
            {
                try
                {
                    Dijkstra.GetShortestPath(final);
                }
                catch (NullReferenceException e)
                {
                    continue;
                }
                result = true;
            }
            return result;
        }
    }
}
