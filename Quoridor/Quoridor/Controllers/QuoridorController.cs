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

        public IActionResult MovePawn(string new_cellname)
        {
            string cur_cellname = Game.CurrentP.Pawn.Cell.Name;
            Direction direction = Direction.North;
            if(((byte)cur_cellname[1]) == ((byte)new_cellname[1]))
            {
                if(((byte)cur_cellname[0]) > ((byte)new_cellname[0]))
                    direction = Direction.West;
                else if(((byte)cur_cellname[0]) < ((byte)new_cellname[0]))
                    direction = Direction.East;
                else{}
            }
            else if(((byte)cur_cellname[1]) > ((byte)new_cellname[1]))
            {
                if(((byte)cur_cellname[0]) > ((byte)new_cellname[0]))
                    direction = Direction.NorthWest;
                else if(((byte)cur_cellname[0]) < ((byte)new_cellname[0]))
                    direction = Direction.NorthEast;
                else
                    direction = Direction.North;
            }
            else if(((byte)cur_cellname[1]) < ((byte)new_cellname[1]))
            {
                if(((byte)cur_cellname[0]) > ((byte)new_cellname[0]))
                    direction = Direction.SouthWest;
                else if(((byte)cur_cellname[0]) < ((byte)new_cellname[0]))
                    direction = Direction.SouthEast;
                else
                    direction = Direction.South;
            }
            else
            {
                return BadRequest();
            }
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
