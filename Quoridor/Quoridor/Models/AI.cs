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
        private Cell[,] cells { get; set; }
        private Fence[] AllFences { get; set; }
        private Player AIPlayer { get; set; }
        private Player humanPlayer { get; set; }

        private List<int[]> possiblePawnMoves { get; set; }
        private List<string> possibleFences { get; set; }

        readonly string[] letters = { "a", "b", "c", "d", "e", "f", "g", "h", "i" };

        public AI(Cell[,] cells, Fence[] AllFences, Player AIPlayer, Player humanPlayer)
        {
            this.cells = cells;
            this.AllFences = AllFences;
            this.AIPlayer = AIPlayer;
            this.humanPlayer = humanPlayer;
        }

        public void AIUpdate(Fence[] AllFences, Player AIPlayer, Player humanPlayer, List<int[]> possiblePawnMoves, List<string> possibleFences)
        {
            this.AllFences = AllFences;
            this.AIPlayer = AIPlayer;
            this.humanPlayer = humanPlayer;
            this.possiblePawnMoves = possiblePawnMoves;
            this.possibleFences = possibleFences;
        }

        //decision function
        //uses ShortestPathDiff(...) and FencesSquaredDiff(...) to choose pawn/fence strategy
        //uses SEF(...)
        public string Decision() 
        {
            int currentShortestPathDiff = ShortestPathDiff(AIPlayer.Pawn.Cell, humanPlayer.Pawn.Cell, cells, AllFences);
            double currentFencesSquaredDiff = FencesSquaredDiff(AIPlayer.CurrentFences, humanPlayer.CurrentFences);

            //range: (0-79)
            //type: int
            int pathDiffLimit = 50;

            //range: (0.0-100.0)
            //type: double
            int fencesDiffLimit = 80;

            //ход стенкой
            if(currentShortestPathDiff > pathDiffLimit) {
                //for each possible fence:
                //  ShortestPathDiff() should be called with:
                //      CURRENT cells and
                //      a modified COPY of the AllFences (a possible fence is added)
                int maxSEF = 0;
                int maxSEFindex = 0;
                foreach(Fence possibleFence in AllFences) {
                    Fence[] tempFences = new Fence[20];
                    Array.Copy(AllFences, tempFences, 20);
                    int index = 0;
                    if (Array.Exists(tempFences, x => x == null))
                    {
                        index = Array.FindIndex(tempFences, i => i == null);
                    }
                    tempFences[index] = new Fence()
                    {
                        Id = AllFences.Count(x => x != null),
                        Name = possibleFence.Name
                    };               
                    int tempSEF = SEF(AIPlayer.Pawn.Cell, humanPlayer.Pawn.Cell, cells, tempFences);
                    if(tempSEF > maxSEF) {
                        maxSEF = tempSEF;
                        maxSEFindex = Array.IndexOf(AllFences, possibleFence);
                    }
                }
                return AllFences[maxSEFindex].Name; //string
            }
            //ход пешкой
            else if(currentFencesSquaredDiff > fencesDiffLimit) {
                //for each possible cell move:
                //  ShortestPathDiff() should be called with:
                //      AI POSSIBLE cell and humanPlayer CURRENT cell
                List<Cell> possibleCells = new List<Cell>();
                foreach(int[] possibleMove in possiblePawnMoves) {
                    Cell possibleCell = new Cell();
                    string currentCellName = AIPlayer.Pawn.Cell.Name;
                    int possibleXind = Array.IndexOf(letters, currentCellName.Substring(0, 1)) + possibleMove[0];
                    string possibleX = letters[possibleXind];
                    string possibleY = Convert.ToString(Convert.ToInt32(currentCellName[1]) + possibleMove[1]);
                    string possibleCellName = possibleX + possibleY;
                    possibleCells.Add(possibleCell);
                }
                int maxSEF = 0;
                int maxSEFindex = 0;
                foreach(Cell possibleCell in possibleCells) {
                    int tempSEF = SEF(possibleCell, humanPlayer.Pawn.Cell, cells, AllFences);
                    if(tempSEF > maxSEF) {
                        maxSEF = tempSEF;
                        maxSEFindex = possibleCells.IndexOf(possibleCell);
                    }
                }
                return possibleCells[maxSEFindex].Name;
            }
            //ход пешкой
            else {
                //for each possible cell move:
                //  ShortestPathDiff() should be called with:
                //      AI POSSIBLE cell and humanPlayer CURRENT cell
                List<Cell> possibleCells = new List<Cell>();
                foreach(int[] possibleMove in possiblePawnMoves) {
                    Cell possibleCell = new Cell();
                    string currentCellName = AIPlayer.Pawn.Cell.Name;
                    int possibleXind = Array.IndexOf(letters, currentCellName.Substring(0, 1)) + possibleMove[0];
                    string possibleX = letters[possibleXind];
                    string possibleY = Convert.ToString(Convert.ToInt32(currentCellName[1]) + possibleMove[1]);
                    string possibleCellName = possibleX + possibleY;
                    possibleCells.Add(possibleCell);
                }
                int maxSEF = 0;
                int maxSEFindex = 0;
                foreach(Cell possibleCell in possibleCells) {
                    int tempSEF = SEF(possibleCell, humanPlayer.Pawn.Cell, cells, AllFences);
                    if(tempSEF > maxSEF) {
                        maxSEF = tempSEF;
                        maxSEFindex = possibleCells.IndexOf(possibleCell);
                    }
                }
                return possibleCells[maxSEFindex].Name;
            }
        }

        //static evaluation function
        //uses ShortestPathDiff(...)
        private static int SEF(Cell possibleCell, Cell enemyCell, Cell[,] cells, Fence[] AllFences) 
        {   
            return ShortestPathDiff(possibleCell, enemyCell, cells, AllFences);
        }

        //the first SEF variable: int
        //players' order (1/2) is permanent
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

            return result2-result1;
        }

        //the second SEF variable: double
        //players' order (1/2) is permanent
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