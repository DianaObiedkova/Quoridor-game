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

        }

        public IActionResult Board()
        {
            Game = new Game();
            //ViewData["fPwalls"] = Game.FirstPWalls;
            //ViewData["sPwalls"] = Game.SecondPWalls;
            //ViewData["moves"] = Game.PossibleMovePawn();
            return View(Game);
        }

        public IActionResult NewGame()
        {
            Game = new Game();
            //ViewData["fPwalls"] = Game.FirstPWalls;
            //ViewData["sPwalls"] = Game.SecondPWalls;
            //ViewData["moves"] = Game.PossibleMovePawn();
            return View("Views/Quoridor/Board.cshtml");
        }

        public void NewGameConsole(string color)
        {
            if (color == "white")
            {
                Game = new Game(new HumanPlayer(new Pawn()) { Id = 1, Name = "Player 1" }, new AIPlayer(new Pawn()) { Id = 2, Name = "Player 2" });
            }
            else
            {
                Game = new Game(new AIPlayer(new Pawn()) { Id = 1, Name = "Player 1" }, new HumanPlayer(new Pawn()) { Id = 2, Name = "Player 2" });
            }
            
        }

        public ContentResult PossibleMoves()
        {
            StringBuilder str=new StringBuilder("");

            foreach(var item in Game.PossibleMovePawn())
            {
                str.Append("<div class=\"posMoveX\">" + item[0] + "</div><div class=\"posMoveY\">" + item[1] + "</div>");
            }
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

        public void MovePawnConsole(string name)
        {
            string new_cellname = name;
            string cur_cellname = Game.CurrentP.Pawn.Cell.Name;
            Direction direction = 0;
            if ((cur_cellname[1]) == (new_cellname[1]))
            {
                if ((cur_cellname[0]) > (new_cellname[0]))
                    direction = Direction.West;
                else if ((cur_cellname[0]) < (new_cellname[0]))
                    direction = Direction.East;
                else { }
            }
            else if ((cur_cellname[1]) > (new_cellname[1]))
            {
                if ((cur_cellname[0]) > (new_cellname[0]))
                    direction = Direction.NorthWest;
                else if ((cur_cellname[0]) < (new_cellname[0]))
                    direction = Direction.NorthEast;
                else
                    direction = Direction.North;
            }
            else if ((cur_cellname[1]) < (new_cellname[1]))
            {
                if ((cur_cellname[0]) > (new_cellname[0]))
                    direction = Direction.SouthWest;
                else if ((cur_cellname[0]) < (new_cellname[0]))
                    direction = Direction.SouthEast;
                else
                    direction = Direction.South;
            }
            else
            {
                Console.WriteLine("You entered inappropriate cell value, try again in format 'e1'");
            }
            if (direction != 0)
            {
                Game.MovePawn(direction);
            }
        }
    }
}
