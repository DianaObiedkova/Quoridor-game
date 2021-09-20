using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quoridor.Models
{
    public class Cell
    {
        //наличие стен вокруг клетки
        public bool northWall { get; set; }
        public bool eastWall { get; set; }
        public bool southWall { get; set; }
        public bool westWall { get; set; }

        //проверка на край поля должна быть здесь?
    }
}