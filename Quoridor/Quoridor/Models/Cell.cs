using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quoridor.Models
{
    public class Cell
    {
        //наличие стен вокруг клетки
        private bool northWall { get; set; }
        private bool eastWall { get; set; }
        private bool southWall { get; set; }
        private bool westWall { get; set; }

        //наличие края доски рядом с клеткой
        private bool northEdge { get; set; }
        private bool eastEdge { get; set; }
        private bool southEdge { get; set; }
        private bool westEdge { get; set; }
    }
}