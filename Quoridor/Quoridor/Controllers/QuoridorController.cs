using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Quoridor.Models;

namespace Quoridor.Controllers
{
    public class QuoridorController : Controller
    {
        public static Game Game { get; set; }

        public QuoridorController()
        {
            //Game = new Game(); 
            //ViewData["fPwalls"] = Game.FirstPWalls;
            //ViewData["sPwalls"] = Game.SecondPWalls;
        }

        public IActionResult Board()
        {
            Game = new Game();
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

        public ContentResult PossibleMoves()
        {
            StringBuilder str=new StringBuilder("");

            foreach(var item in Game.PossibleMovePawn())
            {
                str.Append("<div class=\"posMoveX\">" + item[0] + "</div><div class=\"posMoveY\">" + item[1] + "</div>");
            }
            //ViewData["moves"] = Game.PossibleMovePawn();
            //return PartialView("PartialMoves", Game.PossibleMovePawn());
            return base.Content(str.ToString(), "text/html");
        }

        public IActionResult MovePawn(string name)
        {
            string new_cellname = name;
            string cur_cellname = Game.CurrentP.Pawn.Cell.Name;
            Direction direction = Direction.North;
            if((cur_cellname[1]) == (new_cellname[1]))
            {
                if((cur_cellname[0]) > (new_cellname[0]))
                    direction = Direction.West;
                else if((cur_cellname[0]) < (new_cellname[0]))
                    direction = Direction.East;
                else{}
            }
            else if((cur_cellname[1]) > (new_cellname[1]))
            {
                if((cur_cellname[0]) > (new_cellname[0]))
                    direction = Direction.NorthWest;
                else if((cur_cellname[0]) < (new_cellname[0]))
                    direction = Direction.NorthEast;
                else
                    direction = Direction.North;
            }
            else if((cur_cellname[1]) < (new_cellname[1]))
            {
                if((cur_cellname[0]) > (new_cellname[0]))
                    direction = Direction.SouthWest;
                else if((cur_cellname[0]) < (new_cellname[0]))
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
        public IActionResult SetFence(string c1name, string c2name)
        {
            Cell c1 = new Cell();
            Cell c2 = new Cell();
            c1.Name = c1name;
            c2.Name = c2name;
            bool res = Game.SetFence(c1, c2);
            if(res)
                return Ok();
            else
                return BadRequest();
        }
    }
}
