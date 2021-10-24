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
            Console.WriteLine("Welcome to Quoridor game! Type 'white' or 'black' to choose a side: ");
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
                        var x = splitCommand[1];
                        Console.WriteLine(x);
                        game.MovePawnConsole(x);
                        Console.WriteLine("Ok!");
                        //game.MakeMove(x, y);
                        break;
                }

            }

        }
    }
}