﻿using System;
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
        readonly string[] letters = { "a", "b", "c", "d", "e", "f", "g", "h", "i" };
        readonly string[] fencesLetters = { "s", "t", "u", "v", "w", "x", "y", "z" };
        public readonly Dictionary<string, int> indexes = new Dictionary<string, int>();
        public readonly Dictionary<string, int> fencesIndexes = new Dictionary<string, int>();
        public QuoridorController()
        {
            for (int i = 0; i < letters.Length; i++)
            {
                indexes.Add(letters[i], i);
            }
            for (int i = 0; i < fencesLetters.Length; i++)
            {
                fencesIndexes.Add(fencesLetters[i], i);
            }
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
            Console.WriteLine(Game.CurrentP.Pawn.Cell.Name);
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

        public bool IsMovePossible(string cName)
        {
            var moves = Game.PossibleMovePawn();
            
            foreach(var item in Game.Moves)
            {
                if (item == cName) return true;
            }
            return false;
        }

        public void MovePawnConsole(string name)
        {
            if (IsMovePossible(name))
            {
                string new_cellname = name;
                string cur_cellname = Game.CurrentP.Pawn.Cell.Name;
                Direction direction = 0;

                if (Convert.ToInt32(new_cellname.Substring(1, 1)) <= 8 && Convert.ToInt32(new_cellname.Substring(1, 1)) >= 0)
                {
                    if ((cur_cellname[1]) == (new_cellname[1]))
                    {
                        if (indexes[new_cellname[0].ToString()] - indexes[cur_cellname[0].ToString()] == 1)
                            direction = Direction.West;
                        else if (indexes[cur_cellname[0].ToString()] - indexes[new_cellname[0].ToString()] == 1)
                            direction = Direction.East;
                        else
                        {
                            Console.WriteLine("Inappropriate value");
                            return;
                        }
                    }
                    else if (Convert.ToInt32(cur_cellname.Substring(1, 1)) - Convert.ToInt32(new_cellname.Substring(1, 1)) == 1)
                    {
                        if (indexes[cur_cellname[0].ToString()] - indexes[new_cellname[0].ToString()] == 1)
                            direction = Direction.NorthWest;
                        else if (indexes[new_cellname[0].ToString()] - indexes[cur_cellname[0].ToString()] == 1)
                            direction = Direction.NorthEast;
                        else
                            direction = Direction.North;
                    }
                    else if (Convert.ToInt32(new_cellname.Substring(1, 1)) - Convert.ToInt32(cur_cellname.Substring(1, 1)) == 1)
                    {
                        if (indexes[cur_cellname[0].ToString()] - indexes[new_cellname[0].ToString()] == 1)
                            direction = Direction.SouthWest;
                        else if (indexes[new_cellname[0].ToString()] - indexes[cur_cellname[0].ToString()] == 1)
                            direction = Direction.SouthEast;
                        else
                            direction = Direction.South;
                    }
                    else
                    {
                        Console.WriteLine("You entered inappropriate cell value, try again in format 'e1' or nearly your position.");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Index of cell is out of range");
                    return;
                }


                if (direction != 0)
                {
                    Game.MovePawn(direction);
                    Console.WriteLine("Ok!");
                }
            }
            else
            {
                Console.WriteLine("Move is impossible");
            }
            
        }

        public void JumpPawnConsole(string name)
        {
            if (IsMovePossible(name))
            {
                string new_cellname = name;
                string cur_cellname = Game.CurrentP.Pawn.Cell.Name;
                Direction direction = 0;

                if (Convert.ToInt32(new_cellname.Substring(1, 1)) <= 8 && Convert.ToInt32(new_cellname.Substring(1, 1)) >= 0)
                {
                    if ((cur_cellname[1]) == (new_cellname[1]))
                    {
                        if (indexes[new_cellname[0].ToString()] - indexes[cur_cellname[0].ToString()] == 2)
                            direction = Direction.West;
                        else if (indexes[cur_cellname[0].ToString()] - indexes[new_cellname[0].ToString()] == 2)
                            direction = Direction.East;
                        else
                        {
                            Console.WriteLine("Inappropriate value");
                            return;
                        }
                    }
                    else if (Convert.ToInt32(cur_cellname.Substring(1, 1)) - Convert.ToInt32(new_cellname.Substring(1, 1)) == 2)
                    {
                        if (indexes[cur_cellname[0].ToString()] - indexes[new_cellname[0].ToString()] != 1 && indexes[new_cellname[0].ToString()] - indexes[cur_cellname[0].ToString()] != 1)
                            direction = Direction.North;
                    }
                    else if (Convert.ToInt32(new_cellname.Substring(1, 1)) - Convert.ToInt32(cur_cellname.Substring(1, 1)) == 2)
                    {
                        if (indexes[cur_cellname[0].ToString()] - indexes[new_cellname[0].ToString()] != 1 && indexes[new_cellname[0].ToString()] - indexes[cur_cellname[0].ToString()] != 1)
                            direction = Direction.South;
                    }
                    else
                    {
                        Console.WriteLine("You entered inappropriate cell value, try again in format 'e1' or nearly your position.");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Index of cell is out of range");
                    return;
                }


                if (direction != 0)
                {
                    Game.MovePawn(direction);
                    //Console.WriteLine("Ok!");
                }
            }
            else
            {
                Console.WriteLine("Move is impossible");
            }
        }


        public void SetFenceConsole(string fenceName)
        {
            if(fenceName.Length>3)
            {
                Console.WriteLine("More than 3 letters for fence, try in format 'V7h'");
                return;
            }

            Cell c1 = new Cell();
            Cell c2 = new Cell();
            if (int.TryParse(fenceName.Substring(1, 1), out int num))
            {
                if (fenceName.Substring(2, 1) == "h")
                {
                    c1.Name = letters[fencesIndexes[fenceName.Substring(0, 1)]] + (int.Parse(fenceName.Substring(1, 1)) - 1).ToString();
                    c1.X = letters[fencesIndexes[fenceName.Substring(0, 1)]];
                    c1.Y = int.Parse(fenceName.Substring(1, 1)) - 1;
                    c2.Name = letters[fencesIndexes[fenceName.Substring(0, 1)] + 1] + (int.Parse(fenceName.Substring(1, 1)) - 1).ToString();
                    c2.X = letters[fencesIndexes[fenceName.Substring(0, 1)] + 1];
                    c2.Y = int.Parse(fenceName.Substring(1, 1)) - 1;
                }
                else if (fenceName.Substring(2, 1) == "v")
                {
                    c1.Name = letters[fencesIndexes[fenceName.Substring(0, 1)] + 1] + int.Parse(fenceName.Substring(1, 1)).ToString();
                    c1.X = letters[fencesIndexes[fenceName.Substring(0, 1)] + 1];
                    c1.Y = int.Parse(fenceName.Substring(1, 1));
                    c2.Name = letters[fencesIndexes[fenceName.Substring(0, 1)] + 1] + (int.Parse(fenceName.Substring(1, 1)) + 1).ToString();
                    c2.X = letters[fencesIndexes[fenceName.Substring(0, 1)] + 1];
                    c2.Y = int.Parse(fenceName.Substring(1, 1)) + 1;
                }
                else
                {
                    Console.WriteLine("Undefined direction for wall, try 'v' or 'h'");
                    return;
                }
            }
            else
            {
                Console.WriteLine("You entered inappropriate digit");
            }

            bool res = Game.SetFence(c1, c2);
            if (res)
            {
                Console.WriteLine("Ok!");
            }
            else
                Console.WriteLine("Invalid wall placement!");
        }
    }
}
