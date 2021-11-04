﻿namespace Quoridor.Console.App
{
    using Quoridor.Controllers;
    using Quoridor.Models;
    using System;

    class ConsoleApp
    {
        static void Main(string[] args)
        {
            QuoridorController game = new QuoridorController();

            string com = "";
            //Console.WriteLine("Welcome to Quoridor game! Type 'white' or 'black' to choose a side: ");
            while (true)
            {
                com = Console.ReadLine();
                var splitCommand = com.Split(new char[0]);
                switch (splitCommand[0].ToLower())
                {
                    case "white":
                        game.NewGameConsole(splitCommand[0].ToLower());
                        break;
                    case "black":
                        game.NewGameConsole(splitCommand[0].ToLower());
                        break;
                    case "exit":
                        break;
                    case "move":
                        var x = splitCommand[1].ToLower();
                        x = x.Substring(0, 1) + Convert.ToString(Convert.ToInt32(x.Substring(1, 1)) - 1);
                        game.MovePawnConsole(x);
                        //game.CheckWinner();
                        game.BestAITurn();
                        break;
                    case "wall":
                        game.SetFenceConsole(splitCommand[1].ToLower());
                        //game.CheckWinner();
                        game.BestAITurn();
                        break;
                    case "jump":
                        var x = splitCommand[1].ToLower();
                        x = x.Substring(0, 1) + Convert.ToString(Convert.ToInt32(x.Substring(1, 1)) - 1);
                        game.JumpPawnConsole(x);
                        //game.CheckWinner();
                        game.BestAITurn();
                        break;
                }
            }

        }
    }
}
