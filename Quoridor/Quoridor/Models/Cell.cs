using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quoridor.Models
{
    public class Cell:Entity,ICloneable
    {
        //наличие стен вокруг клетки
        public bool NorthWall { get; set; }
        public bool EastWall { get; set; }
        public bool SouthWall { get; set; }
        public bool WestWall { get; set; }

        public string X { get; set; }
        public int Y { get; set; }

        public object Clone()
        {
            Cell cell = new Cell
            {
                Id = Id,
                Name=Name,
                X=X,
                Y=Y,
                EastWall=EastWall,
                NorthWall=NorthWall,
                SouthWall=SouthWall,
                WestWall=WestWall
            };
            return cell;
        }

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