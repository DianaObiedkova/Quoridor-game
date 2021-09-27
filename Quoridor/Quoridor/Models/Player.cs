using System;

namespace Quoridor.Models
{
    public abstract class Player
    {
        //оставила логику в Player, поскольку она будет общей и для человека, и для бота
        public int Id { get; set; }
        public string Name { get; set; }
        public int CurrentFences { get; private set; } = 10;
        public Pawn Pawn { get; set; }

        protected void SubtractFence()
        {
            CurrentFences -= 1;
        }
        //ход фишкой
        public void PlayPawn(Cell cell)
        {
            Pawn.Cell.X = Convert.ToString(cell.Name.Substring(0, 1)[0]);
            Pawn.Cell.Y = Convert.ToInt32(cell.Name.Substring(1, 1));
        }
        //ход - поставить перегородку
        public void PlayFence()
        {
            SubtractFence();
        }
    }
}