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
            ViewData["fPwalls"] = Game.FirstPWalls;
            ViewData["sPwalls"] = Game.SecondPWalls;
        }

        public IActionResult Board()
        {
            ViewData["fPwalls"] = Game.FirstPWalls;
            ViewData["sPwalls"] = Game.SecondPWalls;
            ViewData["moves"] = Game.PossibleMovePawn();
            return View(Game);
        }

        public IActionResult NewGame()
        {
            Game = new Game();
            ViewData["fPwalls"] = Game.FirstPWalls;
            ViewData["sPwalls"] = Game.SecondPWalls;
            ViewData["moves"] = Game.PossibleMovePawn();
            return View("Views/Quoridor/Board.cshtml");
        }

        public IActionResult PossibleMoves()
        {
            ViewData["moves"] = Game.PossibleMovePawn();
            return PartialView("PartialMoves", Game.PossibleMovePawn());
        }

        public IActionResult MovePawn(Direction direction)
        {
            Game.MovePawn(direction);
            return Ok();
        }
        public IActionResult SetFence(Cell c1, Cell c2)
        {
            ViewData["fPwalls"] = Game.FirstPWalls;
            ViewData["sPwalls"] = Game.SecondPWalls;
            Game.SetFence(c1, c2);
            return Ok();
        }
    }
}
