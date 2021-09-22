using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quoridor.Models
{
    public class Board
    {
        public static int Size { get; set; } = 9;
        public Fence[] AllFences { get; set; } = new Fence[20];
        //сетка клеток размером Size x Size
        readonly Cell[,] cells = new Cell[Size,Size];
        readonly char[] letters = { 'a','b','c','d','e','f','g','h','i' };

        public Board() {

            InitMethod();
        }

        public void InitMethod()
        {
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    cells[i, j] = new Cell
                    {
                        Id = Size * j + (i + 1)
                    };
                }
                cells[0, i].northEdge = true;
                cells[Size - 1, i].southEdge = true;
            }

            //for (int i = 0; i < cells.GetLength(0); i++)
            //{
            //    cells[0, i].northEdge = true;
            //    cells[Size - 1, i].southEdge = true;
            //}

            for (int i = 0; i < cells.GetLength(1); i++)
            {
                cells[i, 0].westEdge = true;
                cells[Size - 1, 0].eastEdge = true;

                for (int j = 0; j < letters.Length; j++)
                {
                    cells[i, j].Name = letters[j].ToString() + i;
                }
            }
        }

    }
}
