using System;
using System.Collections.Generic;
using System.Linq;

namespace Quoridor.Models
{
    public class Game
    {
        private readonly Player firstP;
        private readonly Player secondP;
        private readonly Board board;
        public Player CurrentP { get; private set; }
        public Player Winner { get; private set; }
        public bool IsEnded { get; private set; }

        public int FirstPWalls { get; private set; }
        public int SecondPWalls { get; private set; }

        public Game(Player firstP, Player secondP)
        {
            board = new Board();
            this.firstP = firstP;
            this.secondP = secondP;
            FirstPWalls = firstP.CurrentFences;
            SecondPWalls = firstP.CurrentFences;
            StartGame();
        }

        public Game()
        {
            board = new Board();
            firstP = new HumanPlayer(new Pawn() { Cell = board.cells[0, 4] });
            secondP = new HumanPlayer(new Pawn() { Cell = board.cells[8, 4] });
            FirstPWalls = firstP.CurrentFences;
            SecondPWalls = firstP.CurrentFences;
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
                if(!board.SetFence(X, Y))
                {
                    return false;
                }
                
                CurrentP.PlayFence();
                Console.WriteLine(FirstPWalls + " " + SecondPWalls);
            }

            return true;
        }


        public List<int[]> PossibleMovePawn()
        {
            List<int[]> result = new List<int[]>();

            int index = board.indexes.FirstOrDefault(x => x.Value == CurrentP.Pawn.Cell.X).Key;

            //направо
            if (index + 1 < Board.Size)//чтоб след.проверка и выборка по массиву не давала IndexOutOfRange 
            {
                if (!IsCellHasPawn(board.cells[index + 1, CurrentP.Pawn.Cell.Y])) //если рядом нет пешки
                {
                    if (!CurrentP.Pawn.Cell.X.Contains("i")
                    && !CurrentP.Pawn.Cell.EastWall) // d3 -> e3
                    {
                        result.Add(new int[] { 1, 0 });
                    }
                }
                else //если есть 
                {
                    if (!board.cells[index + 1, CurrentP.Pawn.Cell.Y].X.Contains("i")
                   && !board.cells[index + 1, CurrentP.Pawn.Cell.Y].EastWall) // d3 -> f3
                    {
                        result.Add(new int[] { 2, 0 });
                    }
                }
            }

            //налево
            if (index > 0)//чтоб след.проверка и выборка по массиву не давала IndexOutOfRange
            {
                if (!IsCellHasPawn(board.cells[index - 1, CurrentP.Pawn.Cell.Y])) //если рядом нет пешки
                {
                    if (!CurrentP.Pawn.Cell.X.Contains("a")
                    && !CurrentP.Pawn.Cell.WestWall) // e3 -> d3
                    {
                        result.Add(new int[] { -1, 0 });
                    }
                }
                else //если есть 
                {
                    if (!board.cells[index - 1, CurrentP.Pawn.Cell.Y].X.Contains("a")
                   && !board.cells[index - 1, CurrentP.Pawn.Cell.Y].WestWall) // f3 -> d3
                    {
                        result.Add(new int[] { -2, 0 });
                    }
                }
            }

            //наверх
            if (CurrentP.Pawn.Cell.Y > 0)
            {
                if (!IsCellHasPawn(board.cells[index, CurrentP.Pawn.Cell.Y - 1])) //если рядом нет пешки
                {
                    if (!CurrentP.Pawn.Cell.Y.Equals(0)
                    && !CurrentP.Pawn.Cell.NorthWall)// e3 -> e2
                    {
                        result.Add(new int[] { 0, -1 });
                    }
                }
                else //если есть 
                {
                    if (!board.cells[index, CurrentP.Pawn.Cell.Y - 1].Y.Equals(0)
                   && !board.cells[index, CurrentP.Pawn.Cell.Y - 1].NorthWall) // e3 -> e1
                    {
                        result.Add(new int[] { 0, -2 });
                    }
                }
            }
            //вниз
            if (CurrentP.Pawn.Cell.Y + 1 < Board.Size)
            {
                if (!IsCellHasPawn(board.cells[index, CurrentP.Pawn.Cell.Y + 1])) //если рядом нет пешки
                {
                    if (!CurrentP.Pawn.Cell.Y.Equals(Board.Size - 1)
                    && !CurrentP.Pawn.Cell.SouthWall)// e2 -> e3
                    {
                        result.Add(new int[] { 0, 1 });
                    }
                }
                else //если есть 
                {
                    if (!board.cells[index, CurrentP.Pawn.Cell.Y + 1].Y.Equals(Board.Size - 1)
                   && !board.cells[index, CurrentP.Pawn.Cell.Y + 1].SouthWall) // e1 -> e3
                    {
                        result.Add(new int[] { 0, 2 });
                    }
                }
            }
            //северо-восток
            if (index + 1 < Board.Size && CurrentP.Pawn.Cell.Y - 1 > 0)
            {
                if (IsCellHasPawn(board.cells[index, CurrentP.Pawn.Cell.Y - 1]))//сверху вражеская пешка
                {
                    if ((board.cells[index, CurrentP.Pawn.Cell.Y - 1].NorthWall || CurrentP.Pawn.Cell.Y == 1)
                        && !board.cells[index, CurrentP.Pawn.Cell.Y - 1].WestWall && !board.cells[index, CurrentP.Pawn.Cell.Y - 1].SouthWall)
                    {
                        result.Add(new int[] { 1, -1 });
                    }
                }
                //справа вражеская пешка
                else if (IsCellHasPawn(board.cells[index + 1, CurrentP.Pawn.Cell.Y]) && (board.cells[index + 1, CurrentP.Pawn.Cell.Y].EastWall || index == Board.Size - 1)
                        && !board.cells[index + 1, CurrentP.Pawn.Cell.Y].NorthWall && !board.cells[index + 1, CurrentP.Pawn.Cell.Y].WestWall)
                {
                    result.Add(new int[] { 1, -1 });
                }

            }
            //юго-восток
            if (index + 1 < Board.Size && CurrentP.Pawn.Cell.Y + 1 < Board.Size)
            {
                if (IsCellHasPawn(board.cells[index, CurrentP.Pawn.Cell.Y + 1]))//снизу вражеская пешка
                {
                    if ((board.cells[index, CurrentP.Pawn.Cell.Y + 1].SouthWall || CurrentP.Pawn.Cell.Y == Board.Size - 1)
                        && !board.cells[index, CurrentP.Pawn.Cell.Y + 1].NorthWall && !board.cells[index, CurrentP.Pawn.Cell.Y + 1].EastWall)
                    {
                        result.Add(new int[] { 1, 1 });
                    }
                }
                //справа вражеская пешка
                else if (IsCellHasPawn(board.cells[index + 1, CurrentP.Pawn.Cell.Y]) && (board.cells[index + 1, CurrentP.Pawn.Cell.Y].EastWall || index == Board.Size - 1)
                        && !board.cells[index + 1, CurrentP.Pawn.Cell.Y].SouthWall && !board.cells[index + 1, CurrentP.Pawn.Cell.Y].WestWall)
                {
                    result.Add(new int[] { 1, 1 });
                }

            }
            //юго-запад
            if (index > 0 && CurrentP.Pawn.Cell.Y + 1 < Board.Size)
            {
                if (IsCellHasPawn(board.cells[index, CurrentP.Pawn.Cell.Y + 1]))//снизу вражеская пешка
                {
                    if ((board.cells[index, CurrentP.Pawn.Cell.Y + 1].SouthWall || CurrentP.Pawn.Cell.Y == Board.Size - 1)
                        && !board.cells[index, CurrentP.Pawn.Cell.Y + 1].NorthWall && !board.cells[index, CurrentP.Pawn.Cell.Y + 1].EastWall)
                    {
                        result.Add(new int[] { -1, 1 });
                    }
                }
                //слева вражеская пешка
                else if (IsCellHasPawn(board.cells[index - 1, CurrentP.Pawn.Cell.Y]) && (board.cells[index - 1, CurrentP.Pawn.Cell.Y].EastWall || index == 1)
                        && !board.cells[index - 1, CurrentP.Pawn.Cell.Y].SouthWall && !board.cells[index - 1, CurrentP.Pawn.Cell.Y].WestWall)
                {
                    result.Add(new int[] { -1, 1 });
                }

            }
            //северо-запад
            if (index > 0 && CurrentP.Pawn.Cell.Y > 0)
            {
                if (IsCellHasPawn(board.cells[index, CurrentP.Pawn.Cell.Y - 1]))//сверху вражеская пешка
                {
                    if ((board.cells[index, CurrentP.Pawn.Cell.Y - 1].NorthWall || CurrentP.Pawn.Cell.Y == 1)
                        && !board.cells[index, CurrentP.Pawn.Cell.Y - 1].SouthWall && !board.cells[index, CurrentP.Pawn.Cell.Y - 1].WestWall)
                    {
                        result.Add(new int[] { -1, -1 });
                    }
                }
                else if (IsCellHasPawn(board.cells[index - 1, CurrentP.Pawn.Cell.Y]) && (board.cells[index - 1, CurrentP.Pawn.Cell.Y].WestWall || index == 1)
                        && !board.cells[index - 1, CurrentP.Pawn.Cell.Y].NorthWall && !board.cells[index - 1, CurrentP.Pawn.Cell.Y].EastWall)
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
                            if (!IsCellHasPawn(board.cells[index + 1, CurrentP.Pawn.Cell.Y])) //если рядом нет пешки
                            {
                                if (!CurrentP.Pawn.Cell.X.Contains("i")
                                && !CurrentP.Pawn.Cell.EastWall) // d3 -> e3
                                {
                                    CurrentP.Pawn.Cell.X = board.indexes[index + 1];
                                    CurrentP.Pawn.Name = board.indexes[index + 1] + CurrentP.Pawn.Cell.Y;
                                }
                            }
                            else //если есть 
                            {
                                if (!board.cells[index + 1, CurrentP.Pawn.Cell.Y].X.Contains("i")
                               && !board.cells[index + 1, CurrentP.Pawn.Cell.Y].EastWall) // d3 -> f3
                                {
                                    CurrentP.Pawn.Cell.X = board.indexes[index + 2];
                                    CurrentP.Pawn.Name = board.indexes[index + 2] + CurrentP.Pawn.Cell.Y;
                                }
                            }
                        }

                        break;
                    }
                case Direction.West:
                    {
                        if (index > 0)//чтоб след.проверка и выборка по массиву не давала IndexOutOfRange
                        {
                            if (!IsCellHasPawn(board.cells[index - 1, CurrentP.Pawn.Cell.Y])) //если рядом нет пешки
                            {
                                if (!CurrentP.Pawn.Cell.X.Contains("a")
                                && !CurrentP.Pawn.Cell.WestWall) // e3 -> d3
                                {
                                    CurrentP.Pawn.Cell.X = board.indexes[index - 1];
                                    CurrentP.Pawn.Name = board.indexes[index - 1] + CurrentP.Pawn.Cell.Y;
                                }
                            }
                            else //если есть 
                            {
                                if (!board.cells[index - 1, CurrentP.Pawn.Cell.Y].X.Contains("a")
                               && !board.cells[index - 1, CurrentP.Pawn.Cell.Y].WestWall) // f3 -> d3
                                {
                                    CurrentP.Pawn.Cell.X = board.indexes[index - 2];
                                    CurrentP.Pawn.Name = board.indexes[index - 2] + CurrentP.Pawn.Cell.Y;
                                }
                            }
                        }

                        break;
                    }
                case Direction.North:
                    {
                        if (CurrentP.Pawn.Cell.Y > 0)
                        {
                            if (!IsCellHasPawn(board.cells[index, CurrentP.Pawn.Cell.Y - 1])) //если рядом нет пешки
                            {
                                if (!CurrentP.Pawn.Cell.Y.Equals(0)
                                && !CurrentP.Pawn.Cell.NorthWall)// e3 -> e2
                                {
                                    CurrentP.Pawn.Cell.Y -= 1;
                                    CurrentP.Pawn.Name = CurrentP.Pawn.Cell.X + (CurrentP.Pawn.Cell.Y - 1).ToString();
                                }
                            }
                            else //если есть 
                            {
                                if (!board.cells[index, CurrentP.Pawn.Cell.Y - 1].Y.Equals(0)
                               && !board.cells[index, CurrentP.Pawn.Cell.Y - 1].NorthWall) // e3 -> e1
                                {
                                    CurrentP.Pawn.Cell.Y -= 2;
                                    CurrentP.Pawn.Name = CurrentP.Pawn.Cell.X + (CurrentP.Pawn.Cell.Y - 2).ToString();
                                }
                            }
                        }

                        break;
                    }
                case Direction.South:
                    {
                        if (CurrentP.Pawn.Cell.Y + 1 < Board.Size)
                        {
                            if (!IsCellHasPawn(board.cells[index, CurrentP.Pawn.Cell.Y + 1])) //если рядом нет пешки
                            {
                                if (!CurrentP.Pawn.Cell.Y.Equals(Board.Size - 1)
                                && !CurrentP.Pawn.Cell.SouthWall)// e2 -> e3
                                {
                                    CurrentP.Pawn.Cell.Y += 1;
                                    CurrentP.Pawn.Name = CurrentP.Pawn.Cell.X + (CurrentP.Pawn.Cell.Y + 1).ToString();
                                }
                            }
                            else //если есть 
                            {
                                if (!board.cells[index, CurrentP.Pawn.Cell.Y + 1].Y.Equals(Board.Size - 1)
                               && !board.cells[index, CurrentP.Pawn.Cell.Y + 1].SouthWall) // e1 -> e3
                                {
                                    CurrentP.Pawn.Cell.Y += 2;
                                    CurrentP.Pawn.Name = CurrentP.Pawn.Cell.X + (CurrentP.Pawn.Cell.Y + 2).ToString();
                                }
                            }
                        }
                        break;
                    }
                case Direction.NorthEast:
                    {
                        if (index + 1 < Board.Size && CurrentP.Pawn.Cell.Y - 1 > 0)
                        {
                            if (IsCellHasPawn(board.cells[index, CurrentP.Pawn.Cell.Y - 1]))//сверху вражеская пешка
                            {
                                if ((board.cells[index, CurrentP.Pawn.Cell.Y - 1].NorthWall || CurrentP.Pawn.Cell.Y == 1)
                                    && !board.cells[index, CurrentP.Pawn.Cell.Y - 1].WestWall && !board.cells[index, CurrentP.Pawn.Cell.Y - 1].SouthWall)
                                {
                                    CurrentP.Pawn.Cell.Y -= 1;
                                    CurrentP.Pawn.Name = board.indexes[index + 1] + (CurrentP.Pawn.Cell.Y - 1).ToString(); //суть одна, но условия разные
                                }
                            }
                            else if (IsCellHasPawn(board.cells[index + 1, CurrentP.Pawn.Cell.Y]))//справа вражеская пешка
                            {
                                if ((board.cells[index + 1, CurrentP.Pawn.Cell.Y].EastWall || index == Board.Size - 1)
                                    && !board.cells[index + 1, CurrentP.Pawn.Cell.Y].NorthWall && !board.cells[index + 1, CurrentP.Pawn.Cell.Y].WestWall)
                                {
                                    CurrentP.Pawn.Cell.Y -= 1;
                                    CurrentP.Pawn.Name = board.indexes[index + 1] + (CurrentP.Pawn.Cell.Y - 1).ToString();  //суть/действия одни, но условия разные, needs to be revised
                                }
                            }

                        }
                        break;
                    }
                case Direction.SouthEast:
                    {
                        if (index + 1 < Board.Size && CurrentP.Pawn.Cell.Y + 1 < Board.Size)
                        {
                            if (IsCellHasPawn(board.cells[index, CurrentP.Pawn.Cell.Y + 1]))//снизу вражеская пешка
                            {
                                if ((board.cells[index, CurrentP.Pawn.Cell.Y + 1].SouthWall || CurrentP.Pawn.Cell.Y == Board.Size - 1)
                                    && !board.cells[index, CurrentP.Pawn.Cell.Y + 1].NorthWall && !board.cells[index, CurrentP.Pawn.Cell.Y + 1].EastWall)
                                {
                                    CurrentP.Pawn.Cell.Y += 1;
                                    CurrentP.Pawn.Name = board.indexes[index + 1] + (CurrentP.Pawn.Cell.Y + 1).ToString(); //суть одна, но условия разные
                                }
                            }
                            else if (IsCellHasPawn(board.cells[index + 1, CurrentP.Pawn.Cell.Y]))//справа вражеская пешка
                            {
                                if ((board.cells[index + 1, CurrentP.Pawn.Cell.Y].EastWall || index == Board.Size - 1)
                                    && !board.cells[index + 1, CurrentP.Pawn.Cell.Y].SouthWall && !board.cells[index + 1, CurrentP.Pawn.Cell.Y].WestWall)
                                {
                                    CurrentP.Pawn.Cell.Y += 1;
                                    CurrentP.Pawn.Name = board.indexes[index + 1] + (CurrentP.Pawn.Cell.Y + 1).ToString();  //суть/действия одни, но условия разные, needs to be revised
                                }
                            }

                        }
                        break;
                    }
                case Direction.SouthWest:
                    {
                        if (index > 0 && CurrentP.Pawn.Cell.Y + 1 < Board.Size)
                        {
                            if (IsCellHasPawn(board.cells[index, CurrentP.Pawn.Cell.Y + 1]))//снизу вражеская пешка
                            {
                                if ((board.cells[index, CurrentP.Pawn.Cell.Y + 1].SouthWall || CurrentP.Pawn.Cell.Y == Board.Size - 1)
                                    && !board.cells[index, CurrentP.Pawn.Cell.Y + 1].NorthWall && !board.cells[index, CurrentP.Pawn.Cell.Y + 1].EastWall)
                                {
                                    CurrentP.Pawn.Cell.Y += 1;
                                    CurrentP.Pawn.Name = board.indexes[index - 1] + (CurrentP.Pawn.Cell.Y + 1).ToString(); //суть одна, но условия разные
                                }
                            }
                            else if (IsCellHasPawn(board.cells[index - 1, CurrentP.Pawn.Cell.Y]))//слева вражеская пешка
                            {
                                if ((board.cells[index - 1, CurrentP.Pawn.Cell.Y].EastWall || index == 1)
                                    && !board.cells[index - 1, CurrentP.Pawn.Cell.Y].SouthWall && !board.cells[index - 1, CurrentP.Pawn.Cell.Y].WestWall)
                                {
                                    CurrentP.Pawn.Cell.Y += 1;
                                    CurrentP.Pawn.Name = board.indexes[index - 1] + (CurrentP.Pawn.Cell.Y + 1).ToString();  //суть/действия одни, но условия разные, needs to be revised
                                }
                            }

                        }
                        break;
                    }
                case Direction.NorthWest:
                    {
                        if (index > 0 && CurrentP.Pawn.Cell.Y > 0)
                        {
                            if (IsCellHasPawn(board.cells[index, CurrentP.Pawn.Cell.Y - 1]))//сверху вражеская пешка
                            {
                                if ((board.cells[index, CurrentP.Pawn.Cell.Y - 1].NorthWall || CurrentP.Pawn.Cell.Y == 1)
                                    && !board.cells[index, CurrentP.Pawn.Cell.Y - 1].SouthWall && !board.cells[index, CurrentP.Pawn.Cell.Y - 1].WestWall)
                                {
                                    CurrentP.Pawn.Cell.Y -= 1;
                                    CurrentP.Pawn.Name = board.indexes[index - 1] + (CurrentP.Pawn.Cell.Y - 1).ToString(); //суть одна, но условия разные
                                }
                            }
                            else if (IsCellHasPawn(board.cells[index - 1, CurrentP.Pawn.Cell.Y]))//слева вражеская пешка
                            {
                                if ((board.cells[index - 1, CurrentP.Pawn.Cell.Y].WestWall || index == 1)
                                    && !board.cells[index - 1, CurrentP.Pawn.Cell.Y].NorthWall && !board.cells[index - 1, CurrentP.Pawn.Cell.Y].EastWall)
                                {
                                    CurrentP.Pawn.Cell.Y -= 1;
                                    CurrentP.Pawn.Name = board.indexes[index - 1] + (CurrentP.Pawn.Cell.Y - 1).ToString();  //суть/действия одни, но условия разные, needs to be revised
                                }
                            }

                        }
                        break;
                    }
                default: break;
            }

            SwitchPlayers();
            CheckGameEnd();
        }

        private bool IsCellHasPawn(Cell cell)
        {
            if (firstP.Pawn.Cell.Equals(cell))
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
