using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quoridor.Models
{
    public class Player: Entity
    {
        //public string Name { get; set; }
        public Position position { get; set; }
        public const int maxFences = 10;
        public int CurrentFences { get; private set; } = 10;

        public Fence[] fences;

        public Player(Position position)
        {
            this.position = position;
            Name = "Player " + Id;

            fences = new Fence[maxFences];
        }

        public Player(string name, Position position)
        {
            this.position = position;
            Name = name;
        }

        public void SubtractWall()
        {
            CurrentFences -= 1;
        }
         
        //ход фишкой
        public void PlayPawn() { }
        //ход - поставить перегородку
        public void PlayFence() { }
    }
}
