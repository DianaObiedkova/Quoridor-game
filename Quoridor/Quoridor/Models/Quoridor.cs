using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quoridor.Models
{
    public class Quoridor
    {

        private readonly Player firstP;
        private readonly Player secondP;
        public Player CurrentP { get; private set; }

        public Quoridor(Player firstP, Player secondP)
        {
            this.firstP = firstP;
            this.secondP = secondP;
        }

        public void SwitchPlayers()
        {
            CurrentP = CurrentP == firstP ? secondP : firstP;
        }
    }
}
