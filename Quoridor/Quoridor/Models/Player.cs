using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quoridor.Models
{
    public abstract class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}