using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        private bool isFencePossibleForCurrentUser()
        {
            if (IsFencePossible())
            {
                if (CurrentP.CurrentFences > 0)
                {
                    return true;
                }
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
            int index = Array.FindIndex(CurrentP.fences, i => i == null || i.Id == 0 || string.IsNullOrEmpty(i.Name));
            if(index != null)
            {
                //if(IsFencePossible(передать координаты)) {
                //    
                //}

                //наименование перегородок + ИД
                //горизонтальные
                if (X.Name.Substring(1, 1) == Y.Name.Substring(1, 1))
                {
                    CurrentP.fences[index] = new Fence()
                    {
                        Id = CurrentP.fences.Count(x => x != null),
                        Name = X.Name.Substring(1, 1) + X.Name.Substring(0, 1) + Y.Name.Substring(0, 1)
                    };
                }
                //вертикальные
                else if(X.Name.Substring(0, 1) == Y.Name.Substring(0, 1))
                {
                    CurrentP.fences[index] = new Fence()
                    {
                        Id = CurrentP.fences.Count(x => x != null),
                        Name = X.Name.Substring(0, 1) + X.Name.Substring(1, 1) + Y.Name.Substring(1, 1)
                    };
                }
            }

            CurrentP.PlayFence(X,Y);

        }

        //можно ли поставить перегородку?
        public bool IsFencePossible()
        {
            //проверить по каждым координатам (из 2 пар) для двух клеток по сторонам от них
            return default;
        }

        // отдельно выделить алгоритм поиска пути в графе
        // для проверки IsFencePossible()

        private bool IsCellHasPawn()
        {
            return default;
        }

        public void EndGame() { }
        
        
    }
}
