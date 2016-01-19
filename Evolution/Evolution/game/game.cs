namespace Evolution.game
{
    public class Game
    {
        private const int MaxTurn = 500;
        private const int boardSize = 500;
        private bool[,] _array = new bool[boardSize, boardSize];

        public void ReinitBoard()
        {
            for (int i = 0; i < boardSize - 1; i++)
            {
                for (int j = 0; j < boardSize - 1; j++)
                {
                    _array[i, j] = false;
                }
            }
        }

        public void InitBoard()
        {
            for (int i = 0; i < boardSize - 1; i++)
            {
                for (int j = 0; j < boardSize - 1; j++)
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
                for (int i = 0; i < boardSize - 1; i++)
                {
                    for (int j = 0; j < boardSize - 1; j++)
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
            }

            return averageLivingCell;
        }

        private int GetNbNeighbors(int x, int y)
        {
            int nbNeighbors = 0;
            for (int i = x - 1; i < x + 1; i++)
            {
                if (x != 0 && x == boardSize - 1)
                {
                    for (int j = y - 1; j < y + 1; j++)
                    {
                        if (y != 0 && y == boardSize - 1)
                        {
                            if (_array[i, j])
                                nbNeighbors++;
                        }
                    }
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