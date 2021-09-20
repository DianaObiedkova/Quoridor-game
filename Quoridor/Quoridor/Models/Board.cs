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
        Cell[][] cells = new Cell[Size][Size];

        public Board() {
            foreach (var cell in cells[0])
            {
                cell.northEdge = true;
            }
            foreach (var cell in cells[Size-1])
            {
                cell.southEdge = true;
            }
            foreach (var row in cells)
            {
                row[0].westEdge = true;
                row[Size-1].eastEdge = true;
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
