using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quoridor.Models
{
    public class Board
    {
        public int Size { get; set; }

        //ставим перегородку
        public void SetFence() { }

        //можно ли поставить перегородку?
        public bool IsFencePossible()
        {
            return default;
        }
    }
}
