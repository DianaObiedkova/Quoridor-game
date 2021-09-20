using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quoridor.Models
{
    public class Board
    {
        public int Size { get; set; }

        //сетка клеток размером Size x Size
        //Cell[,] cells = new Cell[Size,Size];

        //ставим перегородку
        public void SetFence() {
            // ставятся две половины перегородки
            // каждая половина ставится  в обе смежные клетки (north+south east+west)
         }

        //можно ли поставить перегородку?
        public bool IsFencePossible()
        {
            return default;
        }

        // отдельно выделить алгоритм поиска пути в графе
        // для проверки IsFencePossible()
    }
}
