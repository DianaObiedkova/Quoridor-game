using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quoridor.Models
{
    public class AIPlayer:Player
    {
        public AIPlayer(Pawn pawn)
        {
            Pawn = pawn;
            Name = "Player " + Id;
        }
    }
}
