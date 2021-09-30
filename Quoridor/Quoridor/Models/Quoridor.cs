using System;
using System.Linq;

namespace Quoridor.Models
{
    public class Quoridor
    {
        private readonly Player firstP;
        private readonly Player secondP;
        private readonly Board board;
        public Player CurrentP { get; private set; }
        public Player Winner { get; private set; }
        public bool IsEnded { get; private set; }

        public Quoridor(Player firstP, Player secondP)
        {
            this.firstP = firstP;
            this.secondP = secondP;
            board = new Board();
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
            if (IsFencePossible() && CurrentP.CurrentFences > 0)
            {
                return true;
            }
            return false;
        }


        //ставим перегородку
        //передать какие-то хотя бы 2 пары координат (для каждой половины по 1)
        public void SetFence(Cell X, Cell Y)
        {
            // ставятся две половины перегородки
            // каждая половина ставится  в обе смежные клетки (north+south east+west)

            //ищу индекс первого пустого элемента в массиве

            bool exists = Array.Exists(board.AllFences, x => x == null || x.Id == 0 || string.IsNullOrEmpty(x.Name));
            int index = 0;

            if (exists)
            {
                index = Array.FindIndex(board.AllFences, i => i == null || i.Id == 0 || string.IsNullOrEmpty(i.Name));
            }
            else return;

            if (IsFencePossibleForCurrentUser())
            {
                //наименование перегородок + ИД
                //горизонтальные
                if (X.Name.Substring(1, 1) == Y.Name.Substring(1, 1))
                {
                    board.AllFences[index] = new Fence()
                    {
                        Id = board.AllFences.Count(x => x != null),
                        Name = X.Name.Substring(1, 1) + X.Name.Substring(0, 1) + Y.Name.Substring(0, 1)
                    };
                }
                //вертикальные
                else if (X.Name.Substring(0, 1) == Y.Name.Substring(0, 1))
                {
                    board.AllFences[index] = new Fence()
                    {
                        Id = board.AllFences.Count(x => x != null),
                        Name = X.Name.Substring(0, 1) + X.Name.Substring(1, 1) + Y.Name.Substring(1, 1)
                    };
                }

                CurrentP.PlayFence();

            }
        }

        //можно ли поставить перегородку?
        public bool IsFencePossible()
        {
            //проверить по каждым координатам (из 2 пар) для двух клеток по сторонам от них
            return default;
        }

        // отдельно выделить алгоритм поиска пути в графе
        // для проверки IsFencePossible()

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
                                if((board.cells[index, CurrentP.Pawn.Cell.Y - 1].NorthWall || CurrentP.Pawn.Cell.Y == 1)
                                    && !board.cells[index, CurrentP.Pawn.Cell.Y - 1].WestWall && !board.cells[index, CurrentP.Pawn.Cell.Y - 1].SouthWall)
                                {
                                    CurrentP.Pawn.Cell.Y -= 1;
                                    CurrentP.Pawn.Name = board.indexes[index + 1] + (CurrentP.Pawn.Cell.Y - 1).ToString(); //суть одна, но условия разные
                                }
                            }
                            else if (IsCellHasPawn(board.cells[index+1, CurrentP.Pawn.Cell.Y]))//справа вражеская пешка
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
            if(secondP.Pawn.Cell.Y == Board.Size - secondP.StartRow)
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
