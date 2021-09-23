using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quoridor.Models
{
    public class Cell:Entity
    {
        //наличие стен вокруг клетки
        public  bool NorthWall { get; private set; }
        public bool EastWall { get; set; }
        public bool SouthWall { get; set; }
        public bool WestWall { get; set; }

        //наличие края доски рядом с клеткой
        public bool NorthEdge { get; set; }
        public bool EastEdge { get; set; }
        public bool SouthEdge { get; set; }
        public bool WestEdge { get; set; }

        public string X { get; set; }
        public int Y { get; set; }

        //public Cell(int id,string name)
        //{
        //    Id = id;
        //    Name = name;
        //}

    }
}