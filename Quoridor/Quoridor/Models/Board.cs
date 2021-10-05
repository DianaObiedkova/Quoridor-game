using System;
using System.Collections.Generic;
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
                        Id = Size * j + i
                    };
                    cells[i, j].Name = letters[j].ToString() + i;
                }
            }
        }

        //fences check
        // пригодится для поиска по графу
        public bool IsMovePawnPossible(Cell currentCell, Direction direction)
        {
            if (Direction.South)
                return NoCellSouthFence(currentCell);
            else if (Direction.North)
                return NoCellNorthFence(currentCell);
            else if (Direction.West)
                return NoCellWestFence(currentCell);
            else if (Direction.East)
                return NoCellEastFence(currentCell);
            else
                Debug.LogError("CanMovePawn error");
                return false;
        }
        private bool NoCellSouthFence(Cell currentCell)
        {
            return !(currentCell.SouthWall);
        } 
        private bool NoCellNorthFence(Cell currentCell)
        {
            return !(currentCell.NorthWall);
        } 
        private bool NoCellWestFence(Cell currentCell)
        {
            return !(currentCell.WestWall);
        } 
        private bool NoCellEastFence(Cell currentCell)
        {
            return !(currentCell.EastWall);
        } 

        //pawn check
        /*private bool NoCellPawn(Cell currentCell)
        {
            return !(currentCell.hasPawn());    
        }*/

        //можно ли поставить перегородку?
        public bool IsFencePossible()
        {
            //проверить по каждым координатам (из 2 пар) для двух клеток по сторонам от них
            return default;
        }

        // отдельно выделить алгоритм поиска пути в графе
        // для проверки IsFencePossible()
    }
}
