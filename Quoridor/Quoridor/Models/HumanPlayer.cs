using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quoridor.Models
{
    public class HumanPlayer: Player
    {
        //public string Name { get; set; }

        public const int maxFences = 10;
        public Pawn Pawn { get; set; }
        public int CurrentFences { get; private set; } = 10;
        public Fence[] Fences { get; set; }

        public HumanPlayer(Pawn pawn)
        {
            Pawn = pawn;
            Name = "Player " + Id;

            Fences = new Fence[maxFences];
        }

        public HumanPlayer(string name, Pawn pawn)
        {
            Pawn = pawn;
            Name = name;
        }

        private void SubtractFence()
        {
            CurrentFences -= 1;
        }
         
        //ход фишкой
        public void PlayPawn(Cell cell) 
        {
            Pawn.Cell.X = Convert.ToString(cell.Name.Substring(0,1)[0]);
            Pawn.Cell.Y = Convert.ToInt32(cell.Name.Substring(1, 1));
        }
        //ход - поставить перегородку
        public void PlayFence() 
        {
            SubtractFence();
        }
    }
}
