using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quoridor.Models
{
    public class Player: Entity
    {
        public string Name { get; set; }
        public Position position { get; set; }

        public Player(Position position)
        {
            this.position = position;
            Name = "Player " + Id;
        }

        public Player(string name, Position position)
        {
            this.position = position;
            Name = name;
        }
    }
}
