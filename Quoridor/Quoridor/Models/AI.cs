using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Quoridor.Models
{
    public class AI
    {
        //AI.cs needs the game's state: 
        //      Board info:
        //      Cell[] cells and Fences[] AllFences for ShortestPathDiff()
        //      Players' info:
        //      current players' cells (Player.Pawn.Cell) for ShortestPathDiff()
        //      players' available walls counters (Player.CurrentFences) for FencesSquaredDiff()
        private Board board { get; set; } //cells, AllFences
        private Player player { get; set; } //current player cells + player available walls
        private int EnemyWalls { get; set; }

        public AI(Board board, Player player, int walls )
        {
            this.board = board;
            this.player = player;
            EnemyWalls = walls;
        }

        //static evaluation function
        //uses ShortestPathDiff(...) and FencesSquaredDiff(...)
        public static int SEF() 
        {
            return default;
        }

        //the first SEF variable: int
        //players' order (1/2) is permanent
        //      the result should be processed with negamax
        private static int ShortestPathDiff(Cell currentCell1, Cell currentCell2, Cell[,] cells, Fence[] AllFences)
        {
            List<Vertex> finalVertices1 = new List<Vertex>();
            List<Vertex> finalVertices2 = new List<Vertex>();
            for (int i = 0; i < 9; i++)
            {
                Vertex sideVertex1 = Array.Find(Dijkstra.Vertices, v => v.Name == cells[8, i].Name);
                finalVertices1.Add(sideVertex1);
                Vertex sideVertex2 = Array.Find(Dijkstra.Vertices, v => v.Name == cells[0, i].Name);
                finalVertices2.Add(sideVertex2);
            }

            int result1 = GetPathLength(currentCell1, finalVertices1, cells, AllFences);
            int result2 = GetPathLength(currentCell2, finalVertices2, cells, AllFences);

            return result1-result2;
        }

        //the second SEF variable: double
        //players' order (1/2) is permanent
        //      the result should be processed with negamax
        private static double FencesSquaredDiff(int player1CurrentFences, int player2CurrentFences)
        {
            double result = Math.Pow((player1CurrentFences-player2CurrentFences), 2);
            return result;
        }

        //returns minimal length
        //uses Dijkstra.cs
        private static int GetPathLength(Cell currentCell, List<Vertex> finalVertices, Cell[,] cells, Fence[] AllFences)
        {
            Vertex currentVertex = Dijkstra.StartAIDijkstra(currentCell, cells.Cast<Cell>().ToArray(), AllFences);

            int minpath = int.MaxValue;
            foreach (Vertex final in finalVertices)
            {
                Dijkstra.FindShortestPath(currentVertex);
                int local_res = Dijkstra.GetShortestPathLength(final);
                if(local_res == -1)
                    continue;
                if(local_res < minpath)
                    minpath = local_res;
            }

            return minpath;
        }
    }
}