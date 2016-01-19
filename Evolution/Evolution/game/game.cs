using System;

namespace Evolution.game
{
    public class Game
    {
        private const int MaxTurn = 500;
        private const int BoardSize = 500;
        private readonly bool[,] _array = new bool[BoardSize, BoardSize];

        public void ReinitBoard()
        {
            for (int i = 0; i < BoardSize - 1; i++)
            {
                for (int j = 0; j < BoardSize - 1; j++)
                {
                    _array[i, j] = false;
                }
            }
        }

        public void InitBoard()
        {
            for (int i = 0; i < BoardSize - 1; i++)
            {
                for (int j = 0; j < BoardSize - 1; j++)
                {
                    _array[i, j] = new bool();
                }
            }
        }

        public int StartGame()
        {
            int averageLivingCell = 0;

            bool[,] array = _array;

            for (int t = 0; t < MaxTurn; t++)
            {
                for (int i = 0; i < BoardSize - 1; i++)
                {
                    for (int j = 0; j < BoardSize - 1; j++)
                    {
                        switch (GetNbNeighbors(i, j))
                        {
                            case 0:
                            case 1:
                                array[i, j] = false;
                                break;

                            case 2:
                                averageLivingCell++;
                                break;

                            case 3:
                                array[i, j] = true;
                                averageLivingCell++;
                                break;

                            default:
                                array[i, j] = false;
                                break;
                        }
                    }
                }
                Console.WriteLine(averageLivingCell / MaxTurn);
            }

            return averageLivingCell/MaxTurn;
        }

        private int GetNbNeighbors(int x, int y)
        {
            int nbNeighbors = 0;
            for (int i = x - 1; i < x + 1; i++)
            {
                if (x == 0 || x != BoardSize - 1) continue;
                for (int j = y - 1; j < y + 1; j++)
                {
                    if (y == 0 || y != BoardSize - 1) continue;
                    if (_array[i, j])
                        nbNeighbors++;
                }
            }
            return nbNeighbors;
        }

        public void SetLiving(Pos[] startBoard)
        {
            foreach (Pos pos in startBoard)
            {
                _array[pos.X, pos.Y] = true;
            }
        }
    }

    public struct Pos
    {
        public int X;
        public int Y;
    }
}