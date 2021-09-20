using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quoridor.Models
{
    public class Board
    {
        public static int Size { get; set; } = 9;

        //сетка клеток размером Size x Size
        Cell[,] cells = new Cell[Size,Size];

        public Board() {

            for (int i = 0; i < cells.GetLength(0); i++)
            {
                cells[0, i].northEdge = true;
                cells[Size - 1, i].southEdge = true;
            }

            for (int i = 0; i < cells.GetLength(1); i++)
            {
                cells[i, 0].westEdge = true;
                cells[Size - 1, 0].eastEdge = true;
            }

        }

        //ставим перегородку
        //передать какие-то хотя бы 2 пары координат (для каждой половины по 1)
        public void SetFence() {
            // ставятся две половины перегородки
            // каждая половина ставится  в обе смежные клетки (north+south east+west)

            //if(IsFencePossible(передать координаты)) {
            //    
            //}
         }

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
