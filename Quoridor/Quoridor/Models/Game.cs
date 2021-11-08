using System;
using System.Collections.Generic;
using System.Linq;

namespace Quoridor.Models
{
    public class Game
    {
        public readonly Player firstP;
        public readonly Player secondP;
        private readonly Board board;
        public Player CurrentP { get; private set; }
        public Player Winner { get; private set; }
        public bool IsEnded { get; private set; }

        public int FirstPWalls { get; private set; }
        public int SecondPWalls { get; private set; }
        public List<string> Moves { get; private set; }

        public event Action<Cell[,]> FieldUpdated;
        public Cell[,] GetCells() => board.cells.Clone() as Cell[,];
        public Fence[] Fences { get { return board.AllFences; } }

        public AI AIinstance { get; private set; }

        public Game(Player firstP, Player secondP)
        {
            board = new Board();
            this.firstP = firstP;
            this.secondP = secondP;
            firstP.Pawn = new Pawn(){ Cell = (Cell)board.cells[8, 4].Clone() };
            secondP.Pawn = new Pawn() { Cell = (Cell)board.cells[0, 4].Clone() };
            FirstPWalls = firstP.CurrentFences;
            SecondPWalls = secondP.CurrentFences;
            Moves = new List<string>();
            if(this.firstP is AIPlayer)
                AIinstance = new AI(board.cells, board.AllFences, this.firstP, this.secondP, true);
            else if(this.secondP is AIPlayer)
                AIinstance = new AI(board.cells, board.AllFences, this.secondP, this.firstP, false);
            StartGame();
        }

        public Game()
        {
            board = new Board();
            firstP = new HumanPlayer(new Pawn() { Cell = (Cell)board.cells[0, 4].Clone() }) { Id = 1, Name = "Player 1" };
            secondP = new HumanPlayer(new Pawn() { Cell = (Cell)board.cells[8, 4].Clone() }) { Id = 2, Name = "Player 2" };
            FirstPWalls = firstP.CurrentFences;
            SecondPWalls = secondP.CurrentFences;
            Moves = new List<string>();
            StartGame();
        }

        public void StartGame()
        {
            CurrentP = firstP;
        }

        public void SwitchPlayers()
        {
            CurrentP = CurrentP == firstP ? secondP : firstP;
        }

        public List<string> PossibleFences()
        {
            return board.PossibleFences();
        }

        private bool IsFencePossibleForCurrentUser()
        {
            if (CurrentP.CurrentFences > 0) // убираю, так как используется уже прямиком в SetFence() - board.IsFencePossible()
            {
                return true;
            }
            return false;
        }


        //ставим перегородку
        public bool SetFence(Cell X, Cell Y)
        {
            if (IsFencePossibleForCurrentUser())
            {
                if (!board.SetFence(X, Y, firstP.Pawn.Cell, secondP.Pawn.Cell))
                {
                    return false;
                }

                CurrentP.PlayFence();
                SwitchPlayers();
                Moves.Clear();
                CheckGameEnd();
                FieldUpdated?.Invoke(GetCells());
            }

            return true;
        }


        public List<int[]> PossiblePawnMoves()
        {
            List<int[]> result = new List<int[]>();

            int index = board.indexes.FirstOrDefault(x => x.Value == CurrentP.Pawn.Cell.X).Key;

            //направо
            if (index + 1 < Board.Size)//чтоб след.проверка и выборка по массиву не давала IndexOutOfRange 
            {
                var cellRight = board.cells[CurrentP.Pawn.Cell.Y, index + 1];
                if (!IsCellHasPawn(cellRight)) //если рядом нет пешки
                {
                    if (!CurrentP.Pawn.Cell.X.Contains("i")
                    && !Array.Find(board.cells.Cast<Cell>().ToArray(), x => x.Equals(CurrentP.Pawn.Cell)).EastWall) // d3 -> e3
                    {
                        AddMovesHelper(result, 1, 0, cellRight.Name);
                    }
                }
                else //если есть 
                {
                    if (!cellRight.X.Contains("i") && !cellRight.EastWall) // d3 -> f3
                    {
                        AddMovesHelper(result, 2, 0, board.cells[CurrentP.Pawn.Cell.Y, index + 2].Name);
                    }
                }
            }

            //налево
            if (index > 0)//чтоб след.проверка и выборка по массиву не давала IndexOutOfRange
            {
                var cellLeft = board.cells[CurrentP.Pawn.Cell.Y, index - 1];
                if (!IsCellHasPawn(cellLeft)) //если рядом нет пешки
                {
                    if (!CurrentP.Pawn.Cell.X.Contains("a") &&
                    !Array.Find(board.cells.Cast<Cell>().ToArray(), x => x.Equals(CurrentP.Pawn.Cell)).WestWall) // e3 -> d3 //&& CurrentP.Pawn.Cell.WestWall //East
                    {
                        AddMovesHelper(result, -1, 0, cellLeft.Name);
                    }
                }
                else //если есть 
                {
                    if (!cellLeft.X.Contains("a") && !cellLeft.WestWall) // f3 -> d3 
                    {
                        AddMovesHelper(result, -2, 0, board.cells[CurrentP.Pawn.Cell.Y, index - 2].Name);
                    }
                }
            }

            //наверх
            if (CurrentP.Pawn.Cell.Y > 0)
            {
                var cellUp = board.cells[CurrentP.Pawn.Cell.Y - 1, index];
                if (!IsCellHasPawn(cellUp)) //если рядом нет пешки
                {
                    if (!CurrentP.Pawn.Cell.Y.Equals(0)
                    && !Array.Find(board.cells.Cast<Cell>().ToArray(), x => x.Equals(CurrentP.Pawn.Cell)).NorthWall)// e3 -> e2
                    {
                        AddMovesHelper(result, 0, -1, cellUp.Name);
                    }
                }
                else //если есть 
                {
                    if (!cellUp.Y.Equals(0) && !cellUp.NorthWall) // e3 -> e1
                    {
                        AddMovesHelper(result, 0, -2, board.cells[CurrentP.Pawn.Cell.Y - 2, index].Name);
                    }
                }
            }
            //вниз
            if (CurrentP.Pawn.Cell.Y + 1 < Board.Size)
            {
                var cellDown = board.cells[CurrentP.Pawn.Cell.Y + 1, index];
                if (!IsCellHasPawn(cellDown)) //если рядом нет пешки
                {
                    if (!CurrentP.Pawn.Cell.Y.Equals(Board.Size - 1)
                    && !Array.Find(board.cells.Cast<Cell>().ToArray(), x => x.Equals(CurrentP.Pawn.Cell)).SouthWall)// e2 -> e3
                    {
                        AddMovesHelper(result, 0, 1, cellDown.Name);
                    }
                }
                else //если есть 
                {
                    if (!cellDown.Y.Equals(Board.Size - 1) && !cellDown.SouthWall) // e1 -> e3
                    {
                        AddMovesHelper(result, 0, 2, board.cells[CurrentP.Pawn.Cell.Y + 2, index].Name);
                    }
                }
            }
            //северо-восток
            if (index + 1 < Board.Size && CurrentP.Pawn.Cell.Y - 1 > 0)
            {
                var upEnemy = board.cells[CurrentP.Pawn.Cell.Y - 1, index];
                var rightEnemy = board.cells[CurrentP.Pawn.Cell.Y, index + 1];
                var moveCellName = board.cells[CurrentP.Pawn.Cell.Y - 1, index + 1].Name;

                if (IsCellHasPawn(upEnemy))//сверху вражеская пешка
                {
                    if ((upEnemy.NorthWall || CurrentP.Pawn.Cell.Y == 1) && !upEnemy.EastWall && !upEnemy.SouthWall)
                    {
                        AddMovesHelper(result, 1, -1, moveCellName);
                    }
                }
                //справа вражеская пешка
                else if (IsCellHasPawn(rightEnemy) && (rightEnemy.EastWall || index + 1 == Board.Size - 1)
                        && !rightEnemy.NorthWall && !rightEnemy.WestWall)
                {
                    AddMovesHelper(result, 1, -1, moveCellName);
                }

            }
            //юго-восток
            if (index + 1 < Board.Size && CurrentP.Pawn.Cell.Y + 1 < Board.Size)
            {
                var downEnemy = board.cells[CurrentP.Pawn.Cell.Y + 1, index];
                var rightEnemy = board.cells[CurrentP.Pawn.Cell.Y, index + 1];
                var moveCellName = board.cells[CurrentP.Pawn.Cell.Y + 1, index + 1].Name;

                if (IsCellHasPawn(downEnemy))//снизу вражеская пешка
                {
                    if ((downEnemy.SouthWall || CurrentP.Pawn.Cell.Y == Board.Size - 1) && !downEnemy.NorthWall && !downEnemy.EastWall)
                    {
                        AddMovesHelper(result, 1, 1, moveCellName);
                    }
                }
                //справа вражеская пешка
                else if (IsCellHasPawn(rightEnemy) && (rightEnemy.EastWall || index + 1 == Board.Size - 1) && !rightEnemy.SouthWall && !rightEnemy.WestWall)
                {
                    AddMovesHelper(result, 1, 1, moveCellName);
                }

            }
            //юго-запад
            if (index > 0 && CurrentP.Pawn.Cell.Y + 1 < Board.Size)
            {
                var downCell = board.cells[CurrentP.Pawn.Cell.Y + 1, index];
                var leftCell = board.cells[CurrentP.Pawn.Cell.Y, index - 1];
                var moveCellName = board.cells[CurrentP.Pawn.Cell.Y + 1, index - 1].Name;

                if (IsCellHasPawn(downCell))//снизу вражеская пешка
                {
                    if ((downCell.SouthWall || CurrentP.Pawn.Cell.Y == Board.Size - 1) && !downCell.NorthWall && !downCell.WestWall)
                    {
                        AddMovesHelper(result, -1, 1, moveCellName);
                    }
                }
                //слева вражеская пешка
                else if (IsCellHasPawn(leftCell) && (leftCell.WestWall || index == 1) && !leftCell.SouthWall && !leftCell.EastWall)
                {
                    AddMovesHelper(result, -1, 1, moveCellName);
                }

            }
            //северо-запад
            if (index > 0 && CurrentP.Pawn.Cell.Y > 0)
            {
                var upCell = board.cells[CurrentP.Pawn.Cell.Y - 1, index];
                var leftCell = board.cells[CurrentP.Pawn.Cell.Y, index - 1];
                var moveCellName = board.cells[CurrentP.Pawn.Cell.Y - 1, index - 1].Name;

                if (IsCellHasPawn(upCell))//сверху вражеская пешка
                {
                    if ((upCell.NorthWall || CurrentP.Pawn.Cell.Y == 1)  && !upCell.SouthWall && !upCell.WestWall)
                    {
                        AddMovesHelper(result, -1, -1, moveCellName);
                    }
                }
                else if (IsCellHasPawn(leftCell) && (leftCell.WestWall || index == 1) && !leftCell.NorthWall && !leftCell.EastWall)
                {
                    AddMovesHelper(result, -1, -1, moveCellName);
                }
            }

            return result;
        }

        private void AddMovesHelper(List<int[]> arr, int offsetX, int offsetY, string cellName)
        {
            arr.Add(new int[] { offsetX, offsetY });
            if (!Moves.Contains(cellName))
                Moves.Add(cellName);
        }

        public void MovePawn(Direction direction)
        {
            int index = board.indexes.FirstOrDefault(x => x.Value == CurrentP.Pawn.Cell.X).Key;

            switch (direction)
            {
                case Direction.East:
                    {
                        if (index + 1 < Board.Size)//чтоб след.проверка и выборка по массиву не давала IndexOutOfRange
                        {
                            var rightcell = board.cells[CurrentP.Pawn.Cell.Y, index + 1];

                            if (!IsCellHasPawn(rightcell)) //если рядом нет пешки
                            {
                                if (!CurrentP.Pawn.Cell.X.Contains("i")
                                && !Array.Find(board.cells.Cast<Cell>().ToArray(), x => x.Equals(CurrentP.Pawn.Cell)).EastWall) // d3 -> e3
                                {
                                    CurrentP.Pawn.Cell.X = board.indexes[index + 1];
                                    CurrentP.Pawn.Cell.Name = board.indexes[index + 1] + CurrentP.Pawn.Cell.Y;
                                }
                            }
                            else //если есть 
                            {
                                if (!rightcell.X.Contains("i") && !rightcell.EastWall) // d3 -> f3
                                {
                                    CurrentP.Pawn.Cell.X = board.indexes[index + 2];
                                    CurrentP.Pawn.Cell.Name = board.indexes[index + 2] + CurrentP.Pawn.Cell.Y;
                                }
                            }
                        }

                        break;
                    }
                case Direction.West:
                    {
                        if (index > 0)//чтоб след.проверка и выборка по массиву не давала IndexOutOfRange
                        {
                            var leftcell = board.cells[CurrentP.Pawn.Cell.Y, index - 1];

                            if (!IsCellHasPawn(leftcell)) //если рядом нет пешки
                            {
                                if (!CurrentP.Pawn.Cell.X.Contains("a")
                                && !Array.Find(board.cells.Cast<Cell>().ToArray(), x => x.Equals(CurrentP.Pawn.Cell)).WestWall) // e3 -> d3
                                {
                                    CurrentP.Pawn.Cell.X = board.indexes[index - 1];
                                    CurrentP.Pawn.Cell.Name = board.indexes[index - 1] + CurrentP.Pawn.Cell.Y;
                                }
                            }
                            else //если есть 
                            {
                                if (!leftcell.X.Contains("a") && !leftcell.WestWall) // f3 -> d3
                                {
                                    CurrentP.Pawn.Cell.X = board.indexes[index - 2];
                                    CurrentP.Pawn.Cell.Name = board.indexes[index - 2] + CurrentP.Pawn.Cell.Y;
                                }
                            }
                        }

                        break;
                    }
                case Direction.North:
                    {
                        if (CurrentP.Pawn.Cell.Y > 0)
                        {
                            var upCell = board.cells[CurrentP.Pawn.Cell.Y - 1, index];

                            if (!IsCellHasPawn(upCell)) //если рядом нет пешки
                            {
                                if (!CurrentP.Pawn.Cell.Y.Equals(0)
                                && !Array.Find(board.cells.Cast<Cell>().ToArray(), x => x.Equals(CurrentP.Pawn.Cell)).NorthWall)// e3 -> e2
                                {
                                    CurrentP.Pawn.Cell.Y -= 1;
                                    CurrentP.Pawn.Cell.Name = CurrentP.Pawn.Cell.X + (CurrentP.Pawn.Cell.Y).ToString();
                                }
                            }
                            else //если есть 
                            {
                                if (!upCell.Y.Equals(0) && !upCell.NorthWall) // e3 -> e1
                                {
                                    CurrentP.Pawn.Cell.Y -= 2;
                                    CurrentP.Pawn.Cell.Name = CurrentP.Pawn.Cell.X + (CurrentP.Pawn.Cell.Y).ToString();
                                }
                            }
                        }

                        break;
                    }
                case Direction.South:
                    {
                        if (CurrentP.Pawn.Cell.Y + 1 < Board.Size)
                        {
                            var downCell = board.cells[CurrentP.Pawn.Cell.Y + 1, index];

                            if (!IsCellHasPawn(downCell)) //если рядом нет пешки
                            {
                                if (!CurrentP.Pawn.Cell.Y.Equals(Board.Size - 1)
                                && !Array.Find(board.cells.Cast<Cell>().ToArray(), x => x.Equals(CurrentP.Pawn.Cell)).SouthWall)// e2 -> e3
                                {
                                    CurrentP.Pawn.Cell.Y += 1;
                                    CurrentP.Pawn.Cell.Name = CurrentP.Pawn.Cell.X + CurrentP.Pawn.Cell.Y.ToString();
                                }
                            }
                            else //если есть 
                            {
                                if (!downCell.Y.Equals(Board.Size - 1) && !downCell.SouthWall) // e1 -> e3
                                {
                                    CurrentP.Pawn.Cell.Y += 2;
                                    CurrentP.Pawn.Cell.Name = CurrentP.Pawn.Cell.X + CurrentP.Pawn.Cell.Y.ToString();
                                }
                            }
                        }
                        break;
                    }
                case Direction.NorthEast:
                    {
                        if (index + 1 < Board.Size && CurrentP.Pawn.Cell.Y - 1 > 0)
                        {
                            var upCell = board.cells[CurrentP.Pawn.Cell.Y - 1, index];
                            var rightCell = board.cells[CurrentP.Pawn.Cell.Y, index + 1];

                            if (IsCellHasPawn(upCell))//сверху вражеская пешка
                            {
                                if ((upCell.NorthWall || CurrentP.Pawn.Cell.Y == 1) && !upCell.EastWall && !upCell.SouthWall)
                                {
                                    CurrentP.Pawn.Cell.Y -= 1;
                                    CurrentP.Pawn.Cell.X = board.indexes[index + 1];
                                    CurrentP.Pawn.Cell.Name = board.indexes[index + 1] + CurrentP.Pawn.Cell.Y.ToString(); //суть одна, но условия разные
                                }
                            }
                            else if (IsCellHasPawn(rightCell) && (rightCell.EastWall || index == Board.Size - 1)
                                    && !rightCell.NorthWall && !rightCell.WestWall) //справа вражеская пешка
                            {
                                CurrentP.Pawn.Cell.Y -= 1;
                                CurrentP.Pawn.Cell.X = board.indexes[index + 1];
                                CurrentP.Pawn.Cell.Name = board.indexes[index + 1] + CurrentP.Pawn.Cell.Y.ToString();  //суть/действия одни, но условия разные, needs to be revised
                            }
                            

                        }
                        break;
                    }
                case Direction.SouthEast:
                    {
                        if (index + 1 < Board.Size && CurrentP.Pawn.Cell.Y + 1 < Board.Size)
                        {
                            var downCell = board.cells[CurrentP.Pawn.Cell.Y + 1, index];
                            var rightCell = board.cells[CurrentP.Pawn.Cell.Y, index + 1];

                            if (IsCellHasPawn(downCell))//снизу вражеская пешка
                            {
                                if ((downCell.SouthWall || CurrentP.Pawn.Cell.Y == Board.Size - 1) && !downCell.NorthWall && !downCell.EastWall)
                                {
                                    CurrentP.Pawn.Cell.Y += 1;
                                    CurrentP.Pawn.Cell.X = board.indexes[index + 1];
                                    CurrentP.Pawn.Cell.Name = board.indexes[index + 1] + CurrentP.Pawn.Cell.Y.ToString(); //суть одна, но условия разные
                                }
                            }
                            else if (IsCellHasPawn(rightCell) && (rightCell.EastWall || index == Board.Size - 1)
                                    && !rightCell.SouthWall && !rightCell.WestWall) //справа вражеская пешка
                            {
                                CurrentP.Pawn.Cell.Y += 1;
                                CurrentP.Pawn.Cell.X = board.indexes[index + 1];
                                CurrentP.Pawn.Cell.Name = board.indexes[index + 1] + CurrentP.Pawn.Cell.Y.ToString();  //суть/действия одни, но условия разные, needs to be revised
                            }
                            

                        }
                        break;
                    }
                case Direction.SouthWest:
                    {
                        if (index > 0 && CurrentP.Pawn.Cell.Y + 1 < Board.Size)
                        {
                            var downCell = board.cells[CurrentP.Pawn.Cell.Y + 1, index];
                            var leftCell = board.cells[CurrentP.Pawn.Cell.Y, index - 1];

                            if (IsCellHasPawn(downCell))//снизу вражеская пешка
                            {
                                if ((downCell.SouthWall || CurrentP.Pawn.Cell.Y == Board.Size - 1) && !downCell.NorthWall && !downCell.EastWall)
                                {
                                    CurrentP.Pawn.Cell.Y += 1;
                                    CurrentP.Pawn.Cell.X = board.indexes[index - 1];
                                    CurrentP.Pawn.Cell.Name = board.indexes[index - 1] + CurrentP.Pawn.Cell.Y.ToString(); //суть одна, но условия разные
                                }
                            }
                            else if (IsCellHasPawn(leftCell) && (leftCell.WestWall || index == 1)
                                    && !leftCell.SouthWall && !leftCell.EastWall) //слева вражеская пешка
                            {
                                CurrentP.Pawn.Cell.Y += 1;
                                CurrentP.Pawn.Cell.X = board.indexes[index - 1];
                                CurrentP.Pawn.Cell.Name = board.indexes[index - 1] + CurrentP.Pawn.Cell.Y.ToString();  //суть/действия одни, но условия разные, needs to be revised
                            }

                        }
                        break;
                    }
                case Direction.NorthWest:
                    {
                        if (index > 0 && CurrentP.Pawn.Cell.Y > 0)
                        {
                            var upCell = board.cells[CurrentP.Pawn.Cell.Y - 1, index];
                            var leftCell = board.cells[CurrentP.Pawn.Cell.Y, index - 1];

                            if (IsCellHasPawn(upCell))//сверху вражеская пешка
                            {
                                if ((upCell.NorthWall || CurrentP.Pawn.Cell.Y == 1) && !upCell.SouthWall && !upCell.WestWall)
                                {
                                    CurrentP.Pawn.Cell.Y -= 1;
                                    CurrentP.Pawn.Cell.X = board.indexes[index - 1];
                                    CurrentP.Pawn.Cell.Name = board.indexes[index - 1] + CurrentP.Pawn.Cell.Y.ToString(); //суть одна, но условия разные
                                }
                            }
                            else if (IsCellHasPawn(leftCell) && (leftCell.WestWall || index == 1)
                                    && !leftCell.NorthWall && !leftCell.EastWall) //слева вражеская пешка
                            {
                                CurrentP.Pawn.Cell.Y -= 1;
                                CurrentP.Pawn.Cell.X = board.indexes[index - 1];
                                CurrentP.Pawn.Cell.Name = board.indexes[index - 1] + CurrentP.Pawn.Cell.Y.ToString();  //суть/действия одни, но условия разные, needs to be revised
                            }

                        }
                        break;
                    }
                default: 
                    {
                        Console.WriteLine("You entered unavailable move, try again.");
                        return;
                    } 
            }
            Moves.Clear();
            SwitchPlayers();
            FieldUpdated?.Invoke(GetCells());
            CheckGameEnd();
        }


        private bool IsCellHasPawn(Cell cell)
        {
            var opPlayer = CurrentP == secondP ? firstP : secondP;
            if (opPlayer.Pawn.Cell.Equals(cell))
            {
                return true;
            }
            return false;
        }

        public virtual void EndGame(Player winner = null)
        {
            Winner = winner;
            IsEnded = true;
        }

        private void CheckGameEnd()
        {
            if (secondP.Pawn.Cell.Y == (Board.Size - 1) - secondP.StartRow)
            {
                EndGame(secondP);
            }
            else if (firstP.Pawn.Cell.Y == (Board.Size - 1) - firstP.StartRow)
            {
                EndGame(firstP);
            }
        }
    }
}
