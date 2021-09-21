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
        private readonly Board board;
        public Player CurrentP { get; private set; }

        public Quoridor(Player firstP, Player secondP)
        {
            this.firstP = firstP;
            this.secondP = secondP;
            board = new Board();
        }

        public void StartGame()
        {
            CurrentP = firstP;
        }

        public void SwitchPlayers()
        {
            CurrentP = CurrentP == firstP ? secondP : firstP;
        }

        private bool isFencePossibleForCurrentUser()
        {
            if (board.IsFencePossible())
            {
                if (CurrentP.CurrentWalls > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void EndGame() { }
        
    }
}
