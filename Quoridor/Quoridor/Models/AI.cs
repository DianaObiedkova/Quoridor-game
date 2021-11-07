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

        //false for white AI
        //true for black AI
        private bool sideIndicator;

        private List<int[]> possiblePawnMoves { get; set; }
        private List<string> possibleFences { get; set; }

        readonly string[] letters = { "a", "b", "c", "d", "e", "f", "g", "h", "i" };

        public AI(Cell[,] cells, Fence[] AllFences, Player AIPlayer, Player humanPlayer, bool sideIndicator)
        {
            this.cells = cells;
            this.AllFences = AllFences;
            this.AIPlayer = AIPlayer;
            this.humanPlayer = humanPlayer;
            this.sideIndicator = sideIndicator;
        }

        public void AIUpdate(Fence[] AllFences, List<int[]> possiblePawnMoves, List<string> possibleFences)
        {
            this.AllFences = AllFences;
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
            string newFence = "";

            if(ShortestEnemyPath(humanPlayer.Pawn.Cell, cells, AllFences) < 2) {
                string enemyCellName = humanPlayer.Pawn.Cell.Name;
                newFence = "h"; 

                //goes to 0th row, enemy is in the topRight
                if(!sideIndicator) {
                    string bottomRight = enemyCellName.Substring(0, 1) + Convert.ToString(Convert.ToInt32(enemyCellName.Substring(1, 1)) - 1);
                    int leftInd = Array.IndexOf(letters, enemyCellName.Substring(0, 1)) - 1;
                    string bottomLeft = letters[leftInd] + Convert.ToString(Convert.ToInt32(enemyCellName.Substring(1, 1)) - 1);
                    string topLeft = letters[leftInd] + enemyCellName.Substring(1, 1);

                    newFence += bottomLeft + topLeft + bottomRight + enemyCellName;
                }
                //goes to 8 row, enemy is in the bottomRight
                else {
                    string topRight = enemyCellName.Substring(0, 1) + Convert.ToString(Convert.ToInt32(enemyCellName.Substring(1, 1)) + 1);
                    int leftInd = Array.IndexOf(letters, enemyCellName.Substring(0, 1)) - 1;
                    string bottomLeft = letters[leftInd] + enemyCellName.Substring(1, 1);
                    string topLeft = letters[leftInd] + Convert.ToString(Convert.ToInt32(enemyCellName.Substring(1, 1)) + 1);

                    newFence += bottomLeft + topLeft + enemyCellName + topRight;
                }
            }
            else if(ShortestAIPath(AIPlayer.Pawn.Cell, cells, AllFences) == 2) {
                string AICellName = AIPlayer.Pawn.Cell.Name;
                newFence = "v"; 

                //goes to 8 row, in the bottomRight
                if(sideIndicator) {
                    string topRight = AICellName.Substring(0, 1) + Convert.ToString(Convert.ToInt32(AICellName.Substring(1, 1)) + 1);
                    int leftInd = Array.IndexOf(letters, AICellName.Substring(0, 1)) - 1;
                    string bottomLeft = letters[leftInd] + AICellName.Substring(1, 1);
                    string topLeft = letters[leftInd] +  Convert.ToString(Convert.ToInt32(AICellName.Substring(1, 1)) + 1);

                    newFence += topLeft + topRight + bottomLeft + AICellName;
                    newFence += bottomLeft + AICellName + topLeft + topRight;
                }
                //goes to 0th row, in the topRight
                else {
                    string bottomRight = AICellName.Substring(0, 1) + Convert.ToString(Convert.ToInt32(AICellName.Substring(1, 1)) - 1);
                    int leftInd = Array.IndexOf(letters, AICellName.Substring(0, 1)) - 1;
                    string bottomLeft = letters[leftInd] + Convert.ToString(Convert.ToInt32(AICellName.Substring(1, 1)) - 1);
                    string topLeft = letters[leftInd] + AICellName.Substring(1, 1);

                    newFence += bottomRight + bottomLeft + AICellName + topLeft;
                }
            }
            string tempNewFence= string.Empty;
            if (!string.IsNullOrEmpty(newFence))
            {
                tempNewFence = newFence.Substring(1, 2) + newFence.Substring(5, 2);
            }

            List<string> overlapFences = new List<string>();
            if(newFence[0] == 'h') {
                overlapFences.Add(letters[Array.IndexOf(letters, tempNewFence.Substring(0, 1)) - 1] +  tempNewFence.Substring(1, 1) + tempNewFence.Substring(0, 2));
                overlapFences.Add(tempNewFence.Substring(2, 2) + letters[Array.IndexOf(letters, tempNewFence.Substring(2, 1)) + 1] +  tempNewFence.Substring(1, 1));
                overlapFences.Add(tempNewFence.Substring(2, 2) + tempNewFence.Substring(2, 1) + Convert.ToString(Convert.ToInt32(tempNewFence.Substring(2, 2)) + 1));
            }
            if(newFence[0] == 'v') {
                overlapFences.Add(tempNewFence.Substring(0, 1) + Convert.ToString(Convert.ToInt32(tempNewFence.Substring(1, 1)) - 1) + tempNewFence.Substring(0, 2));
                overlapFences.Add(tempNewFence.Substring(2, 2) + tempNewFence.Substring(2, 1) + Convert.ToString(Convert.ToInt32(tempNewFence.Substring(2, 2)) + 1));
                overlapFences.Add(letters[Array.IndexOf(letters, tempNewFence.Substring(0, 1)) - 1] + tempNewFence.Substring(1, 1) + tempNewFence.Substring(0, 2));
            }
            if(possibleFences.Contains(tempNewFence))
            {
                bool skip = false;
                foreach(string fence in overlapFences) {
                    if(!possibleFences.Contains(fence)) 
                        skip = true;
                }
                if(!skip)
                    return newFence;
            }

            //ход стенкой
            if(currentShortestPathDiff > pathDiffLimit) {
                //for each possible fence:
                //  ShortestPathDiff() should be called with:
                //      CURRENT cells and
                //      a modified COPY of the AllFences (a possible fence is added)
                int minSEF = 0;
                int minSEFindex = 0;
                foreach(string possibleFence in possibleFences) {
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
                        Name = possibleFence
                    };               
                    int tempSEF = SEF(AIPlayer.Pawn.Cell, cells, tempFences);
                    if(tempSEF < minSEF) {
                        minSEF = tempSEF;
                        minSEFindex = possibleFences.FindIndex(f => f == possibleFence);
                    }
                }
                return possibleFences[minSEFindex]; //string
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
                    string possibleY = Convert.ToString(Convert.ToInt32(currentCellName.Substring(1, 1)) + possibleMove[1]);
                    string possibleCellName = possibleX + possibleY;
                    possibleCell.Name = possibleCellName;
                    possibleCells.Add(possibleCell);
                }
                int minSEF = int.MaxValue;
                int minSEFindex = 0;
                foreach(Cell possibleCell in possibleCells) {
                    int tempSEF = SEF(possibleCell, cells, AllFences);
                    if(tempSEF < minSEF) {
                        minSEF = tempSEF;
                        minSEFindex = possibleCells.FindIndex(c => c.Name == possibleCell.Name);
                    }
                }
                return possibleCells[minSEFindex].Name;
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
                    string possibleY = Convert.ToString(Convert.ToInt32(currentCellName.Substring(1, 1)) + possibleMove[1]);
                    string possibleCellName = possibleX + possibleY;
                    possibleCell.Name = possibleCellName;
                    possibleCells.Add(possibleCell);
                }
                int minSEF = int.MaxValue;
                int minSEFindex = 0;
                foreach(Cell possibleCell in possibleCells) {
                    int tempSEF = SEF(possibleCell, cells, AllFences);
                    if(tempSEF < minSEF) {
                        minSEF = tempSEF;
                        minSEFindex = possibleCells.FindIndex(c => c.Name == possibleCell.Name);
                    }
                }
                return possibleCells[minSEFindex].Name;
            }
        }

        //static evaluation function
        private int SEF(Cell possibleCell, Cell[,] cells, Fence[] AllFences) 
        {   
            return ShortestAIPath(possibleCell, cells, AllFences);
        }

        //uses ShortestPathDiff(...)
        private int DiffSEF(Cell possibleCell, Cell enemyCell, Cell[,] cells, Fence[] AllFences) 
        {   
            return ShortestPathDiff(possibleCell, enemyCell, cells, AllFences);
        }

        private int ShortestAIPath(Cell currentAICell, Cell[,] cells, Fence[] AllFences)
        {
            Vertex currentVertex1 = Dijkstra.StartAIDijkstra(currentAICell, cells.Cast<Cell>().ToArray(), AllFences);

            List<Vertex> finalVertices = new List<Vertex>();
            for (int i = 0; i < 9; i++)
            {
                Vertex sideVertex;
                if(sideIndicator) {
                    sideVertex = Array.Find(Dijkstra.Vertices, v => v.Name == cells[0, i].Name);
                }
                else {
                sideVertex = Array.Find(Dijkstra.Vertices, v => v.Name == cells[8, i].Name);
                }
                finalVertices.Add(sideVertex);
            }

            int minResult = int.MaxValue;
            foreach (Vertex final in finalVertices)
            {
                Dijkstra.FindShortestPath(currentVertex1);
                int result1 = GetPathLength(currentAICell, finalVertices, cells, AllFences);
                if(result1 < minResult)
                    minResult = result1;
            }

            return minResult;
        }

        private int ShortestEnemyPath(Cell currentEnemyCell, Cell[,] cells, Fence[] AllFences)
        {
            Vertex currentVertex1 = Dijkstra.StartAIDijkstra(currentEnemyCell, cells.Cast<Cell>().ToArray(), AllFences);

            List<Vertex> finalVertices = new List<Vertex>();
            for (int i = 0; i < 9; i++)
            {
                Vertex sideVertex;
                if(sideIndicator) {
                    sideVertex = Array.Find(Dijkstra.Vertices, v => v.Name == cells[8, i].Name);
                }
                else {
                sideVertex = Array.Find(Dijkstra.Vertices, v => v.Name == cells[0, i].Name);
                }
                finalVertices.Add(sideVertex);
            }

            int minResult = int.MaxValue;
            foreach (Vertex final in finalVertices)
            {
                Dijkstra.FindShortestPath(currentVertex1);
                int result1 = GetPathLength(currentEnemyCell, finalVertices, cells, AllFences);
                if(result1 < minResult)
                    minResult = result1;
            }

            return minResult;
        }

        //the first SEF variable: int
        //players' order (1/2) is permanent
        private int ShortestPathDiff(Cell currentCell1, Cell currentCell2, Cell[,] cells, Fence[] AllFences)
        {
            Vertex currentVertex1 = Dijkstra.StartAIDijkstra(currentCell1, cells.Cast<Cell>().ToArray(), AllFences);

            List<Vertex> finalVertices1 = new List<Vertex>();
            List<Vertex> finalVertices2 = new List<Vertex>();
            for (int i = 0; i < 9; i++)
            {
                Vertex sideVertex1;
                Vertex sideVertex2;
                if(sideIndicator) {
                    sideVertex1 = Array.Find(Dijkstra.Vertices, v => v.Name == cells[0, i].Name);
                    sideVertex2 = Array.Find(Dijkstra.Vertices, v => v.Name == cells[8, i].Name);
                }
                else {
                    sideVertex1 = Array.Find(Dijkstra.Vertices, v => v.Name == cells[8, i].Name);
                    sideVertex2 = Array.Find(Dijkstra.Vertices, v => v.Name == cells[0, i].Name);
                }
                finalVertices1.Add(sideVertex1);
                finalVertices2.Add(sideVertex2);
            }

            int minResult1 = int.MaxValue;
            foreach (Vertex final in finalVertices1)
            {
                Dijkstra.FindShortestPath(currentVertex1);
                int result1 = GetPathLength(currentCell1, finalVertices1, cells, AllFences);
                if(result1 < minResult1)
                    minResult1 = result1;
            }

            int minResult2 = int.MaxValue;
            Vertex currentVertex2 = Dijkstra.StartAIDijkstra(currentCell2, cells.Cast<Cell>().ToArray(), AllFences);
            foreach (Vertex final in finalVertices2)
            {
                Dijkstra.FindShortestPath(currentVertex2);
                int result2 = GetPathLength(currentCell2, finalVertices2, cells, AllFences);
                if(result2 < minResult2)
                    minResult2 = result2;
            }

            return minResult2-minResult1;
        }

        //the second SEF variable: double
        //players' order (1/2) is permanent
        private double FencesSquaredDiff(int player1CurrentFences, int player2CurrentFences)
        {
            double result = Math.Pow((player1CurrentFences-player2CurrentFences), 2);
            return result;
        }

        //returns minimal length
        //uses Dijkstra.cs
        private int GetPathLength(Cell currentCell, List<Vertex> finalVertices, Cell[,] cells, Fence[] AllFences)
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