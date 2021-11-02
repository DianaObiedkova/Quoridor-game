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
                AIinstance = new AI(this.board, this.firstP, this.secondP);
            else if(this.secondP is AIPlayer)
                AIinstance = new AI(this.board, this.secondP, this.firstP);
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
                        result.Add(new int[] { 1, 0 });
                        if (!Moves.Contains(cellRight.Name)) Moves.Add(cellRight.Name);
                    }
                }
                else //если есть 
                {
                    if (!board.cells[CurrentP.Pawn.Cell.Y, index + 1].X.Contains("i")
                   && !board.cells[CurrentP.Pawn.Cell.Y, index + 1].EastWall) // d3 -> f3
                    {
                        result.Add(new int[] { 2, 0 });
                        if (!Moves.Contains(board.cells[CurrentP.Pawn.Cell.Y, index + 2].Name)) Moves.Add(board.cells[CurrentP.Pawn.Cell.Y, index + 2].Name);
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
                        result.Add(new int[] { -1, 0 });
                        if (!Moves.Contains(cellLeft.Name)) Moves.Add(cellLeft.Name);
                    }
                }
                else //если есть 
                {
                    if (!board.cells[CurrentP.Pawn.Cell.Y, index - 1].X.Contains("a")
                   && !board.cells[CurrentP.Pawn.Cell.Y, index - 1].WestWall) // f3 -> d3 
                    {
                        result.Add(new int[] { -2, 0 });
                        if (!Moves.Contains(board.cells[CurrentP.Pawn.Cell.Y, index - 2].Name)) Moves.Add(board.cells[CurrentP.Pawn.Cell.Y, index - 2].Name);
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
                        result.Add(new int[] { 0, -1 });
                        if (!Moves.Contains(cellUp.Name)) Moves.Add(cellUp.Name);
                    }
                }
                else //если есть 
                {
                    if (!board.cells[CurrentP.Pawn.Cell.Y - 1, index].Y.Equals(0)
                   && !board.cells[CurrentP.Pawn.Cell.Y - 1, index].NorthWall) // e3 -> e1
                    {
                        result.Add(new int[] { 0, -2 });
                        if(!Moves.Contains(board.cells[CurrentP.Pawn.Cell.Y - 2, index].Name)) Moves.Add(board.cells[CurrentP.Pawn.Cell.Y - 2, index].Name);
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
                        result.Add(new int[] { 0, 1 });
                        if (!Moves.Contains(cellDown.Name)) Moves.Add(cellDown.Name);
                    }
                }
                else //если есть 
                {
                    if (!board.cells[CurrentP.Pawn.Cell.Y + 1, index].Y.Equals(Board.Size - 1)
                   && !board.cells[CurrentP.Pawn.Cell.Y + 1, index].SouthWall) // e1 -> e3
                    {
                        result.Add(new int[] { 0, 2 });
                        if (!Moves.Contains(board.cells[CurrentP.Pawn.Cell.Y + 2, index].Name)) Moves.Add(board.cells[CurrentP.Pawn.Cell.Y + 2, index].Name);
                    }
                }
            }
            //северо-восток
            if (index + 1 < Board.Size && CurrentP.Pawn.Cell.Y - 1 > 0)
            {
                if (IsCellHasPawn(board.cells[CurrentP.Pawn.Cell.Y - 1, index]))//сверху вражеская пешка
                {
                    if ((board.cells[CurrentP.Pawn.Cell.Y - 1, index].NorthWall || CurrentP.Pawn.Cell.Y == 1)
                        && !board.cells[CurrentP.Pawn.Cell.Y - 1, index].EastWall && !board.cells[CurrentP.Pawn.Cell.Y - 1, index].SouthWall)
                    {
                        result.Add(new int[] { 1, -1 });
                    }
                }
                //справа вражеская пешка
                else if (IsCellHasPawn(board.cells[CurrentP.Pawn.Cell.Y, index + 1]) && (board.cells[CurrentP.Pawn.Cell.Y, index + 1].EastWall || index + 1 == Board.Size - 1)
                        && !board.cells[CurrentP.Pawn.Cell.Y, index + 1].NorthWall && !board.cells[CurrentP.Pawn.Cell.Y, index + 1].WestWall)
                {
                    result.Add(new int[] { 1, -1 });
                }

            }
            //юго-восток
            if (index + 1 < Board.Size && CurrentP.Pawn.Cell.Y + 1 < Board.Size)
            {
                if (IsCellHasPawn(board.cells[CurrentP.Pawn.Cell.Y + 1, index]))//снизу вражеская пешка
                {
                    if ((board.cells[CurrentP.Pawn.Cell.Y + 1, index].SouthWall || CurrentP.Pawn.Cell.Y == Board.Size - 1)
                        && !board.cells[CurrentP.Pawn.Cell.Y + 1, index].NorthWall && !board.cells[CurrentP.Pawn.Cell.Y + 1, index].EastWall)
                    {
                        result.Add(new int[] { 1, 1 });
                    }
                }
                //справа вражеская пешка
                else if (IsCellHasPawn(board.cells[CurrentP.Pawn.Cell.Y, index + 1]) && (board.cells[CurrentP.Pawn.Cell.Y, index + 1].EastWall || index + 1 == Board.Size - 1)
                        && !board.cells[CurrentP.Pawn.Cell.Y, index + 1].SouthWall && !board.cells[CurrentP.Pawn.Cell.Y, index + 1].WestWall)
                {
                    result.Add(new int[] { 1, 1 });
                }

            }
            //юго-запад
            if (index > 0 && CurrentP.Pawn.Cell.Y + 1 < Board.Size)
            {
                if (IsCellHasPawn(board.cells[CurrentP.Pawn.Cell.Y + 1, index]))//снизу вражеская пешка
                {
                    if ((board.cells[CurrentP.Pawn.Cell.Y + 1, index].SouthWall || CurrentP.Pawn.Cell.Y == Board.Size - 1)
                        && !board.cells[CurrentP.Pawn.Cell.Y + 1, index].NorthWall && !board.cells[CurrentP.Pawn.Cell.Y + 1, index].WestWall)
                    {
                        result.Add(new int[] { -1, 1 });
                    }
                }
                //слева вражеская пешка
                else if (IsCellHasPawn(board.cells[CurrentP.Pawn.Cell.Y, index - 1]) && (board.cells[CurrentP.Pawn.Cell.Y, index - 1].WestWall || index == 1)
                        && !board.cells[CurrentP.Pawn.Cell.Y, index - 1].SouthWall && !board.cells[CurrentP.Pawn.Cell.Y, index - 1].EastWall)
                {
                    result.Add(new int[] { -1, 1 });
                }

            }
            //северо-запад
            if (index > 0 && CurrentP.Pawn.Cell.Y > 0)
            {
                if (IsCellHasPawn(board.cells[CurrentP.Pawn.Cell.Y - 1, index]))//сверху вражеская пешка
                {
                    if ((board.cells[CurrentP.Pawn.Cell.Y - 1, index].NorthWall || CurrentP.Pawn.Cell.Y == 1)
                        && !board.cells[CurrentP.Pawn.Cell.Y - 1, index].SouthWall && !board.cells[CurrentP.Pawn.Cell.Y - 1, index].WestWall)
                    {
                        result.Add(new int[] { -1, -1 });
                    }
                }
                else if (IsCellHasPawn(board.cells[CurrentP.Pawn.Cell.Y, index - 1]) && (board.cells[CurrentP.Pawn.Cell.Y, index - 1].WestWall || index == 1)
                        && !board.cells[CurrentP.Pawn.Cell.Y, index - 1].NorthWall && !board.cells[CurrentP.Pawn.Cell.Y, index - 1].EastWall)
                {
                    result.Add(new int[] { -1, -1 });
                }

            }

            return result;
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
                            if (!IsCellHasPawn(board.cells[CurrentP.Pawn.Cell.Y, index + 1])) //если рядом нет пешки
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
                                if (!board.cells[CurrentP.Pawn.Cell.Y, index + 1].X.Contains("i")
                               && !board.cells[CurrentP.Pawn.Cell.Y, index + 1].EastWall) // d3 -> f3
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
                            if (!IsCellHasPawn(board.cells[CurrentP.Pawn.Cell.Y, index - 1])) //если рядом нет пешки
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
                                if (!board.cells[CurrentP.Pawn.Cell.Y, index - 1].X.Contains("a")
                               && !board.cells[CurrentP.Pawn.Cell.Y, index - 1].WestWall) // f3 -> d3
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
                            if (!IsCellHasPawn(board.cells[CurrentP.Pawn.Cell.Y - 1, index])) //если рядом нет пешки
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
                                if (!board.cells[CurrentP.Pawn.Cell.Y - 1, index].Y.Equals(0)
                               && !board.cells[CurrentP.Pawn.Cell.Y - 1, index].NorthWall) // e3 -> e1
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
                            if (!IsCellHasPawn(board.cells[CurrentP.Pawn.Cell.Y + 1, index])) //если рядом нет пешки
                            {
                                if (!CurrentP.Pawn.Cell.Y.Equals(Board.Size - 1)
                                && !Array.Find(board.cells.Cast<Cell>().ToArray(), x => x.Equals(CurrentP.Pawn.Cell)).SouthWall)// e2 -> e3
                                {
                                    CurrentP.Pawn.Cell.Y += 1;
                                    CurrentP.Pawn.Cell.Name = CurrentP.Pawn.Cell.X + (CurrentP.Pawn.Cell.Y).ToString();
                                }
                            }
                            else //если есть 
                            {
                                if (!board.cells[CurrentP.Pawn.Cell.Y + 1, index].Y.Equals(Board.Size - 1)
                               && !board.cells[CurrentP.Pawn.Cell.Y + 1, index].SouthWall) // e1 -> e3
                                {
                                    CurrentP.Pawn.Cell.Y += 2;
                                    CurrentP.Pawn.Cell.Name = CurrentP.Pawn.Cell.X + (CurrentP.Pawn.Cell.Y).ToString();
                                }
                            }
                        }
                        break;
                    }
                case Direction.NorthEast:
                    {
                        if (index + 1 < Board.Size && CurrentP.Pawn.Cell.Y - 1 > 0)
                        {
                            if (IsCellHasPawn(board.cells[CurrentP.Pawn.Cell.Y - 1, index]))//сверху вражеская пешка
                            {
                                if ((board.cells[CurrentP.Pawn.Cell.Y - 1, index].NorthWall || CurrentP.Pawn.Cell.Y == 1)
                                    && !board.cells[CurrentP.Pawn.Cell.Y - 1, index].EastWall && !board.cells[CurrentP.Pawn.Cell.Y - 1, index].SouthWall)
                                {
                                    CurrentP.Pawn.Cell.Y -= 1;
                                    CurrentP.Pawn.Cell.X = board.indexes[index + 1];
                                    CurrentP.Pawn.Cell.Name = board.indexes[index + 1] + (CurrentP.Pawn.Cell.Y).ToString(); //суть одна, но условия разные
                                }
                            }
                            else if (IsCellHasPawn(board.cells[CurrentP.Pawn.Cell.Y, index + 1]))//справа вражеская пешка
                            {
                                if ((board.cells[CurrentP.Pawn.Cell.Y, index + 1].EastWall || index == Board.Size - 1)
                                    && !board.cells[CurrentP.Pawn.Cell.Y, index + 1].NorthWall && !board.cells[CurrentP.Pawn.Cell.Y, index + 1].WestWall)
                                {
                                    CurrentP.Pawn.Cell.Y -= 1;
                                    CurrentP.Pawn.Cell.X = board.indexes[index + 1];
                                    CurrentP.Pawn.Cell.Name = board.indexes[index + 1] + (CurrentP.Pawn.Cell.Y).ToString();  //суть/действия одни, но условия разные, needs to be revised
                                }
                            }

                        }
                        break;
                    }
                case Direction.SouthEast:
                    {
                        if (index + 1 < Board.Size && CurrentP.Pawn.Cell.Y + 1 < Board.Size)
                        {
                            if (IsCellHasPawn(board.cells[CurrentP.Pawn.Cell.Y + 1, index]))//снизу вражеская пешка
                            {
                                if ((board.cells[CurrentP.Pawn.Cell.Y + 1, index].SouthWall || CurrentP.Pawn.Cell.Y == Board.Size - 1)
                                    && !board.cells[CurrentP.Pawn.Cell.Y + 1, index].NorthWall && !board.cells[CurrentP.Pawn.Cell.Y + 1, index].EastWall)
                                {
                                    CurrentP.Pawn.Cell.Y += 1;
                                    CurrentP.Pawn.Cell.X = board.indexes[index + 1];
                                    CurrentP.Pawn.Cell.Name = board.indexes[index + 1] + (CurrentP.Pawn.Cell.Y).ToString(); //суть одна, но условия разные
                                }
                            }
                            else if (IsCellHasPawn(board.cells[CurrentP.Pawn.Cell.Y, index + 1]))//справа вражеская пешка
                            {
                                if ((board.cells[CurrentP.Pawn.Cell.Y, index + 1].EastWall || index == Board.Size - 1)
                                    && !board.cells[CurrentP.Pawn.Cell.Y, index + 1].SouthWall && !board.cells[CurrentP.Pawn.Cell.Y, index + 1].WestWall)
                                {
                                    CurrentP.Pawn.Cell.Y += 1;
                                    CurrentP.Pawn.Cell.X = board.indexes[index + 1];
                                    CurrentP.Pawn.Cell.Name = board.indexes[index + 1] + (CurrentP.Pawn.Cell.Y).ToString();  //суть/действия одни, но условия разные, needs to be revised
                                }
                            }

                        }
                        break;
                    }
                case Direction.SouthWest:
                    {
                        if (index > 0 && CurrentP.Pawn.Cell.Y + 1 < Board.Size)
                        {
                            if (IsCellHasPawn(board.cells[CurrentP.Pawn.Cell.Y + 1, index]))//снизу вражеская пешка
                            {
                                if ((board.cells[CurrentP.Pawn.Cell.Y + 1, index].SouthWall || CurrentP.Pawn.Cell.Y == Board.Size - 1)
                                    && !board.cells[CurrentP.Pawn.Cell.Y + 1, index].NorthWall && !board.cells[CurrentP.Pawn.Cell.Y + 1, index].EastWall)
                                {
                                    CurrentP.Pawn.Cell.Y += 1;
                                    CurrentP.Pawn.Cell.X = board.indexes[index - 1];
                                    CurrentP.Pawn.Cell.Name = board.indexes[index - 1] + (CurrentP.Pawn.Cell.Y).ToString(); //суть одна, но условия разные
                                }
                            }
                            else if (IsCellHasPawn(board.cells[CurrentP.Pawn.Cell.Y, index - 1]))//слева вражеская пешка
                            {
                                if ((board.cells[CurrentP.Pawn.Cell.Y, index - 1].WestWall || index == 1)
                                    && !board.cells[CurrentP.Pawn.Cell.Y, index - 1].SouthWall && !board.cells[CurrentP.Pawn.Cell.Y, index - 1].EastWall)
                                {
                                    CurrentP.Pawn.Cell.Y += 1;
                                    CurrentP.Pawn.Cell.X = board.indexes[index - 1];
                                    CurrentP.Pawn.Cell.Name = board.indexes[index - 1] + (CurrentP.Pawn.Cell.Y).ToString();  //суть/действия одни, но условия разные, needs to be revised
                                }
                            }

                        }
                        break;
                    }
                case Direction.NorthWest:
                    {
                        if (index > 0 && CurrentP.Pawn.Cell.Y > 0)
                        {
                            if (IsCellHasPawn(board.cells[CurrentP.Pawn.Cell.Y - 1, index]))//сверху вражеская пешка
                            {
                                if ((board.cells[CurrentP.Pawn.Cell.Y - 1, index].NorthWall || CurrentP.Pawn.Cell.Y == 1)
                                    && !board.cells[CurrentP.Pawn.Cell.Y - 1, index].SouthWall && !board.cells[CurrentP.Pawn.Cell.Y - 1, index].WestWall)
                                {
                                    CurrentP.Pawn.Cell.Y -= 1;
                                    CurrentP.Pawn.Cell.X = board.indexes[index - 1];
                                    CurrentP.Pawn.Cell.Name = board.indexes[index - 1] + (CurrentP.Pawn.Cell.Y).ToString(); //суть одна, но условия разные
                                }
                            }
                            else if (IsCellHasPawn(board.cells[CurrentP.Pawn.Cell.Y, index - 1]))//слева вражеская пешка
                            {
                                if ((board.cells[CurrentP.Pawn.Cell.Y, index - 1].WestWall || index == 1)
                                    && !board.cells[CurrentP.Pawn.Cell.Y, index - 1].NorthWall && !board.cells[CurrentP.Pawn.Cell.Y, index - 1].EastWall)
                                {
                                    CurrentP.Pawn.Cell.Y -= 1;
                                    CurrentP.Pawn.Cell.X = board.indexes[index - 1];
                                    CurrentP.Pawn.Cell.Name = board.indexes[index - 1] + (CurrentP.Pawn.Cell.Y).ToString();  //суть/действия одни, но условия разные, needs to be revised
                                }
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
            var opPlayer = (CurrentP == secondP ? firstP : secondP);
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
            Console.WriteLine("Game is ended! the winner is: " + Winner.Name);
        }

        private void CheckGameEnd()
        {
            if (secondP.Pawn.Cell.Y == Board.Size - secondP.StartRow)
            {
                EndGame(secondP);
            }
            else if (firstP.Pawn.Cell.Y == Board.Size - secondP.StartRow)
            {
                EndGame(firstP);
            }
        }
    }
}
