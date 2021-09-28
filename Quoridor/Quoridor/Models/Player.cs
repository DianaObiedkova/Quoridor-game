using System;

namespace Quoridor.Models
{
    public abstract class Player
    {
        //�������� ������ � Player, ��������� ��� ����� ����� � ��� ��������, � ��� ����
        private int startRow;
        public int Id { get; set; }
        public string Name { get; set; }
        public int CurrentFences { get; private set; } = 10;
        public Pawn Pawn { get; set; }
        public int StartRow
        {
            get { return startRow; }
            set
            {
                if (value < 0 || value > 8)
                    throw new ArgumentOutOfRangeException(
                          $"{nameof(value)} must be between 0 and 8.");

                startRow = value;
            }
        }

        protected void SubtractFence()
        {
            CurrentFences -= 1;
        }
        //��� ������
        public void PlayPawn(Cell cell)
        {
            Pawn.Cell.X = Convert.ToString(cell.Name.Substring(0, 1)[0]);
            Pawn.Cell.Y = Convert.ToInt32(cell.Name.Substring(1, 1));
        }
        //��� - ��������� �����������
        public void PlayFence()
        {
            SubtractFence();
        }
    }
}