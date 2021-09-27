using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quoridor.Models
{
    public class HumanPlayer: Player
    {
        public HumanPlayer(Pawn pawn)
        {
            Pawn = pawn;
            Name = "Player " + Id;
        }

        public HumanPlayer(string name, Pawn pawn)
        {
            Pawn = pawn;
            Name = name;
        }

    }
}
