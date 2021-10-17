using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quoridor.Models
{
    public class Cell:Entity
    {
        //наличие стен вокруг клетки
        public bool NorthWall { get; private set; }
        public bool EastWall { get; set; }
        public bool SouthWall { get; set; }
        public bool WestWall { get; set; }

        public string X { get; set; }
        public int Y { get; set; }

        //public Cell(int id,string name)
        //{
        //    Id = id;
        //    Name = name;
        //}
        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Cell p = (Cell)obj;
                return (p.X == X && p.Y == Y)||(p.Name == Name);
            }
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}