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
            return View(Game);
        }

        public IActionResult NewGame()
        {
            Game = new Game();
            return View("Views/Quoridor/Board.cshtml");
        }

        public void NewGameConsole(string color)
        {
            if (color == "white")
            {
                Game = new Game(new AIPlayer(new Pawn()) { Id = 1, Name = "Player 1", StartRow = 8 }, new HumanPlayer(new Pawn()) { Id = 2, Name = "Player 2", StartRow = 0 });
                BestAITurn();
            }
            else
            {
                Game = new Game(new HumanPlayer(new Pawn()) { Id = 1, Name = "Player 1", StartRow = 8 }, new AIPlayer(new Pawn()) { Id = 2, Name = "Player 2", StartRow = 0 });
                
            }
            //Console.WriteLine(Game.CurrentP.Pawn.Cell.Name);
            //Game.FieldUpdated += OnFieldUpdated;
        }

        private void OnFieldUpdated(Cell[,] cells)
        {
            for(int i = 0; i < cells.GetLength(0); i++)
            {
                Console.Write($" {PawnOrNot(cells[i, 0])} {VFence(Array.Find(Game.Fences, x => !(x is null) && x.Name.Contains("v") && x.Name.Contains($"{cells[i, 0].Name}{cells[i, 1].Name}")))}" +
                    $" {PawnOrNot(cells[i, 1])} {VFence(Array.Find(Game.Fences, x => !(x is null) && x.Name.Contains("v") && x.Name.Contains($"{cells[i, 1].Name}{cells[i, 2].Name}")))}" +
                    $" {PawnOrNot(cells[i, 2])} {VFence(Array.Find(Game.Fences, x => !(x is null) && x.Name.Contains("v") && x.Name.Contains($"{cells[i, 2].Name}{cells[i, 3].Name}")))}" +
                    $" {PawnOrNot(cells[i, 3])} {VFence(Array.Find(Game.Fences, x => !(x is null) && x.Name.Contains("v") && x.Name.Contains($"{cells[i, 3].Name}{cells[i, 4].Name}")))}" +
                    $" {PawnOrNot(cells[i, 4])} {VFence(Array.Find(Game.Fences, x => !(x is null) && x.Name.Contains("v") && x.Name.Contains($"{cells[i, 4].Name}{cells[i, 5].Name}")))}" +
                    $" {PawnOrNot(cells[i, 5])} {VFence(Array.Find(Game.Fences, x => !(x is null) && x.Name.Contains("v") && x.Name.Contains($"{cells[i, 5].Name}{cells[i, 6].Name}")))}" +
                    $" {PawnOrNot(cells[i, 6])} {VFence(Array.Find(Game.Fences, x => !(x is null) && x.Name.Contains("v") && x.Name.Contains($"{cells[i, 6].Name}{cells[i, 7].Name}")))}" +
                    $" {PawnOrNot(cells[i, 7])} {VFence(Array.Find(Game.Fences, x => !(x is null) && x.Name.Contains("v") && x.Name.Contains($"{cells[i, 7].Name}{cells[i, 8].Name}")))}" +
                    $" {PawnOrNot(cells[i, 8])} \n");
                
                if (i < cells.GetLength(0) - 1)
                {
                    Console.Write($"{HFence(Array.Find(Game.Fences, x => !(x is null) && x.Name.Contains("h") && x.Name.Contains($"{cells[i, 0].Name}{cells[i + 1, 0].Name}")))}+" +//═╬
                            $"{HFence(Array.Find(Game.Fences, x => !(x is null) && x.Name.Contains("h") && x.Name.Contains($"{cells[i, 1].Name}{cells[i + 1, 1].Name}")))}+" +
                            $"{HFence(Array.Find(Game.Fences, x => !(x is null) && x.Name.Contains("h") && x.Name.Contains($"{cells[i, 2].Name}{cells[i + 1, 2].Name}")))}+" +
                            $"{HFence(Array.Find(Game.Fences, x => !(x is null) && x.Name.Contains("h") && x.Name.Contains($"{cells[i, 3].Name}{cells[i + 1, 3].Name}")))}+" +
                            $"{HFence(Array.Find(Game.Fences, x => !(x is null) && x.Name.Contains("h") && x.Name.Contains($"{cells[i, 4].Name}{cells[i + 1, 4].Name}")))}+" +
                            $"{HFence(Array.Find(Game.Fences, x => !(x is null) && x.Name.Contains("h") && x.Name.Contains($"{cells[i, 5].Name}{cells[i + 1, 5].Name}")))}+" +
                            $"{HFence(Array.Find(Game.Fences, x => !(x is null) && x.Name.Contains("h") && x.Name.Contains($"{cells[i, 6].Name}{cells[i + 1, 6].Name}")))}+" +
                            $"{HFence(Array.Find(Game.Fences, x => !(x is null) && x.Name.Contains("h") && x.Name.Contains($"{cells[i, 7].Name}{cells[i + 1, 7].Name}")))}+" +
                            $"{HFence(Array.Find(Game.Fences, x => !(x is null) && x.Name.Contains("h") && x.Name.Contains($"{cells[i, 8].Name}{cells[i + 1, 8].Name}")))}\n");
                    //Console.Write("═══╬═╬═══╬═╬═══╬═╬═══╬═╬═══╬═╬═══╬═╬═══╬═╬═══╬═╬═══\n");
                }
            }

            static string PawnOrNot(Cell cell)
            {
                if (Game.firstP.Pawn.Cell.Equals(cell)) 
                    return Game.firstP.Id.ToString();
                if (Game.secondP.Pawn.Cell.Equals(cell)) 
                    return Game.secondP.Id.ToString();
                return " ";
            }

            static string VFence(Fence fence)
            {
                if (fence is null) return "|";
                return "║";
            }

            static string HFence(Fence fence)
            {
                if (fence is null) return "---";
                return "═══";
            }
        }

        public ContentResult PossibleMoves()
        {
            StringBuilder str=new StringBuilder("");

            foreach(var item in Game.PossiblePawnMoves())
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
                if((cur_cellname[0]) < (new_cellname[0]))
                    direction = Direction.West;
                else if((cur_cellname[0]) > (new_cellname[0]))
                    direction = Direction.East;
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
            var moves = Game.PossiblePawnMoves();
            
            foreach(var item in Game.Moves)
            {
                if (item == cName) return true;
            }
            return false;
        }

        public void MovePawnConsole(string name)
        {
            name = name.Substring(0, 1) + Convert.ToString(Convert.ToInt32(name.Substring(1, 1)) - 1);
            if (IsMovePossible(name))
            {
                string new_cellname = name;
                string cur_cellname = Game.CurrentP.Pawn.Cell.Name;
                Direction direction = 0;

                var cellsXDiff = indexes[cur_cellname[0].ToString()] - indexes[new_cellname[0].ToString()];
                var newCellSecondDigit = Convert.ToInt32(new_cellname.Substring(1, 1));
                var curCellSecondDigit = Convert.ToInt32(cur_cellname.Substring(1, 1));

                if (newCellSecondDigit <= 8 && newCellSecondDigit >= 0)
                {
                    if (cur_cellname[1] == new_cellname[1])
                    {
                        if (cellsXDiff == 1)
                            direction = Direction.West;
                        else if (-cellsXDiff == 1)
                            direction = Direction.East;
                        else
                        {
                            Console.WriteLine("Inappropriate value");
                            return;
                        }
                    }
                    else if (curCellSecondDigit - newCellSecondDigit == 1)
                    {
                        if (cellsXDiff == 1)
                            direction = Direction.NorthWest;
                        else if (-cellsXDiff == 1)
                            direction = Direction.NorthEast;
                        else
                            direction = Direction.North;
                    }
                    else if (newCellSecondDigit - curCellSecondDigit == 1)
                    {
                        if (cellsXDiff == 1)
                            direction = Direction.SouthWest;
                        else if (-cellsXDiff == 1)
                            direction = Direction.SouthEast;
                        else
                            direction = Direction.South;
                    }
                    else
                    {
                        JumpPawnConsole(new_cellname);
                        //Console.WriteLine("You entered inappropriate cell value, try again in format 'e1' or nearly your position.");
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
                }
            }
            else
            {
                Console.WriteLine("Move is impossible");
            }
            
        }

        public void JumpPawnConsole(string name)
        {
            name = name.Substring(0, 1) + Convert.ToString(Convert.ToInt32(name.Substring(1, 1)) - 1);
            if (IsMovePossible(name))
            {
                string new_cellname = name;
                string cur_cellname = Game.CurrentP.Pawn.Cell.Name;
                Direction direction = 0;

                var cellsXDiff = indexes[cur_cellname[0].ToString()] - indexes[new_cellname[0].ToString()];
                var newCellSecondDigit = Convert.ToInt32(new_cellname.Substring(1, 1));
                var curCellSecondDigit = Convert.ToInt32(cur_cellname.Substring(1, 1));

                if (newCellSecondDigit <= 8 && newCellSecondDigit >= 0)
                {
                    if ((cur_cellname[1]) == (new_cellname[1]))
                    {
                        if (-cellsXDiff == 2)
                            direction = Direction.West;
                        else if (cellsXDiff == 2)
                            direction = Direction.East;
                        else
                        {
                            Console.WriteLine("Inappropriate value");
                            return;
                        }
                    }
                    else if (curCellSecondDigit - newCellSecondDigit == 2)
                    {
                        if (cellsXDiff != 1 && -cellsXDiff != 1)
                            direction = Direction.North;
                    }
                    else if (newCellSecondDigit - curCellSecondDigit == 2)
                    {
                        if (cellsXDiff != 1 && -cellsXDiff != 1)
                            direction = Direction.South;
                    }
                    else if (curCellSecondDigit - newCellSecondDigit == 1)
                    {
                        if (cellsXDiff == 1)
                            direction = Direction.NorthWest;
                        else if (-cellsXDiff == 1)
                            direction = Direction.NorthEast;
                    }
                    else if (newCellSecondDigit - curCellSecondDigit == 1)
                    {
                        if (cellsXDiff == 1)
                            direction = Direction.SouthWest;
                        else if (-cellsXDiff == 1)
                            direction = Direction.SouthEast;
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
                }
            }
            else
            {
                Console.WriteLine("Move is impossible");
            }
        }

        private void CheckWinner()
        {
            if (!(Game.Winner is null))
            {
                Console.WriteLine("Game is ended! the winner is: " + Game.Winner.Name);
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
                    c2.Name = letters[fencesIndexes[fenceName.Substring(0, 1)] + 1] + (int.Parse(fenceName.Substring(1, 1)) - 1).ToString();
                    c2.X = letters[fencesIndexes[fenceName.Substring(0, 1)] + 1];
                    c2.Y = int.Parse(fenceName.Substring(1, 1)) - 1;
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

            bool res = Game.SetFence(c2, c1);
            if (res)
            {
                //Console.WriteLine("Ok!");
            }
            else
                Console.WriteLine("Invalid wall placement!");
        }


        public void BestAITurn()
        {
            //Game.PossiblePawnMoves();
            //Random ran = new Random();
            //int i = ran.Next(2);

            //if (i == 1)
            //{
            //    var x = Game.Moves.FirstOrDefault();
            //    MovePawnConsole(x);
            //    Console.WriteLine("<- move " + x);
            //}
            //else
            //{
            //    List<string> fences = Game.PossibleFences();

            //    int r = ran.Next(fences.Count - 1);

            //    string cell1Name = fences[r].Substring(0, 2);
            //    string cell2Name = fences[r].Substring(2, 2);

            //    SetFence(cell1Name,cell2Name);

            //    if (cell1Name[0] == cell2Name[0])
            //    {
            //        Console.WriteLine("<- wall " + fencesLetters[Array.IndexOf(letters, cell1Name.Substring(0, 1))].ToString()+(Convert.ToInt32(cell1Name.Substring(1)) + 1) +"h");
            //    }
            //    else
            //    {
            //        Console.WriteLine("<- wall " + fencesLetters[Array.IndexOf(letters, cell1Name.Substring(0, 1))].ToString() + (Convert.ToInt32(cell1Name.Substring(1)) + 1) + "v");
            //    }

            //}

            Game.AIinstance.AIUpdate(Game.Fences, Game.PossiblePawnMoves(), Game.PossibleFences());

            string move = Game.AIinstance.Decision();

            if(move.Length == 9)
            {
                string cell1Name = move.Substring(1, 2);
                string cell2Name = move.Substring(5, 2);

                SetFence(cell1Name, cell2Name);

                if (cell1Name[1] == cell2Name[1])
                {
                    Console.WriteLine("wall " + fencesLetters[Array.IndexOf(letters, cell1Name.Substring(0, 1))].ToString() + (Convert.ToInt32(cell1Name.Substring(1)) + 1) + "h");
                    //CheckWinner();
                }
                else
                {
                    Console.WriteLine("wall " + fencesLetters[Array.IndexOf(letters, cell1Name.Substring(0, 1))].ToString() + (Convert.ToInt32(cell1Name.Substring(1)) + 1) + "v");
                    //CheckWinner();
                }
            }
            else if(move.Length == 2)
            {
                if(Math.Abs(indexes[Game.CurrentP.Pawn.Cell.Name.Substring(0, 1)] - indexes[move.Substring(0, 1)]) > 1 || 
                    Math.Abs(Convert.ToInt32(Game.CurrentP.Pawn.Cell.Name.Substring(1, 1)) - Convert.ToInt32(move.Substring(1, 1))) > 1 )
                {
                    move = move.Substring(0, 1) + Convert.ToString(Convert.ToInt32(move.Substring(1, 1)) + 1);
                    JumpPawnConsole(move);
                    Console.WriteLine("jump " + move);
                    //CheckWinner();
                }
                else if(Math.Abs(indexes[Game.CurrentP.Pawn.Cell.Name.Substring(0, 1)] - indexes[move.Substring(0, 1)]) == 1 &&
                    Math.Abs(Convert.ToInt32(Game.CurrentP.Pawn.Cell.Name.Substring(1, 1)) - Convert.ToInt32(move.Substring(1, 1))) == 1){
                    move = move.Substring(0, 1) + Convert.ToString(Convert.ToInt32(move.Substring(1, 1)) + 1);
                    MovePawnConsole(move);
                    Console.WriteLine("jump " + move);
                }
                else
                {
                    move = move.Substring(0, 1) + Convert.ToString(Convert.ToInt32(move.Substring(1, 1)) + 1);
                    MovePawnConsole(move);
                    Console.WriteLine("move " + move);
                    //CheckWinner();
                }
            }
            else
            {

                move = move.Substring(0, 1) + Convert.ToString(Convert.ToInt32(move.Substring(1, 1)) + 1);
                Console.WriteLine("move " + move);
                //CheckWinner();
            }
        }
    }
}
