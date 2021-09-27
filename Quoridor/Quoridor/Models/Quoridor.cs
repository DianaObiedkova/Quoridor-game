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

            //если рядом нет пешки
            if (direction == Direction.East && !CurrentP.Pawn.Cell.X.Contains("i")
                && !CurrentP.Pawn.Cell.EastWall) // d3 -> e3
            {
                CurrentP.Pawn.Name = board.indexes[index + 1] + CurrentP.Pawn.Cell.Y;
            }
            else if (direction == Direction.West && !CurrentP.Pawn.Cell.X.Contains("a")
                && !CurrentP.Pawn.Cell.WestWall)// e3 -> d3
            {
                CurrentP.Pawn.Name = board.indexes[index - 1] + CurrentP.Pawn.Cell.Y;
            }
            else if (direction == Direction.North && !CurrentP.Pawn.Cell.Y.Equals(0)
                && !CurrentP.Pawn.Cell.NorthWall)// e3 -> e2
            {
                CurrentP.Pawn.Name = CurrentP.Pawn.Cell.X + (CurrentP.Pawn.Cell.Y-1).ToString();
            }
            else if (direction == Direction.South && !CurrentP.Pawn.Cell.Y.Equals(8)
                && !CurrentP.Pawn.Cell.SouthWall)// e3 -> e4
            {
                CurrentP.Pawn.Name = CurrentP.Pawn.Cell.X + (CurrentP.Pawn.Cell.Y + 1).ToString();
            }
           
        }

        private bool IsCellHasPawn()
        {
            return default;
        }

        public void EndGame() { }
        
        
    }
}
