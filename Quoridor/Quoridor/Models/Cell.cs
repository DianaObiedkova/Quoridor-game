using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quoridor.Models
{
    public class Cell:Entity
    {
        //наличие стен вокруг клетки
        public  bool northWall { get; private set; }
        private bool eastWall { get; set; }
        private bool southWall { get; set; }
        private bool westWall { get; set; }

        //наличие края доски рядом с клеткой
        public bool northEdge { get; set; }
        public bool eastEdge { get; set; }
        public bool southEdge { get; set; }
        public bool westEdge { get; set; }

        //public Cell(int id,string name)
        //{
        //    Id = id;
        //    Name = name;
        //}

    }
}