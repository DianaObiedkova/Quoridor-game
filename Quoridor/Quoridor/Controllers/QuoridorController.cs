using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Quoridor.Models;

namespace Quoridor.Controllers
{
    public class QuoridorController : Controller
    {
        public Game Game { get; set; }

        public QuoridorController()
        {
            Game = new Game();
        }

        public IActionResult Board()
        {
            return View(Game);
        }
    }
}
