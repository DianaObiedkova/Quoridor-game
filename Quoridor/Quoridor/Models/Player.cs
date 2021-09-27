using System;

namespace Quoridor.Models
{
    public abstract class Player
    {
        //�������� ������ � Player, ��������� ��� ����� ����� � ��� ��������, � ��� ����
        public int Id { get; set; }
        public string Name { get; set; }
        public int CurrentFences { get; private set; } = 10;
        public Pawn Pawn { get; set; }

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