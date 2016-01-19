using System;

namespace Evolution.game
{
    public class Game
    {
        private const int MaxTurn = 500;
        private const int BoardSize = 100;
        private bool[,] _array = new bool[BoardSize, BoardSize];
        private Pos[] neighborsNavigator;

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
            initNeighborsNavigator();
            for (int i = 0; i < BoardSize - 1; i++)
            {
                for (int j = 0; j < BoardSize - 1; j++)
                {
                    _array[i, j] = new bool();
                }
            }
        }

        private DateTime span;

        public int StartGame()
        {
            span = DateTime.Now;
            int averageLivingCell = 0;

            bool[,] buffer = _array;

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
                                buffer[i, j] = false;
                                break;

                            case 2:
                                if (_array[i, j])
                                    averageLivingCell++;
                                break;

                            case 3:
                                buffer[i, j] = true;
                                averageLivingCell++;
                                break;

                            default:
                                buffer[i, j] = false;
                                break;
                        }
                    }
                }
                _array = buffer;
                Console.WriteLine(averageLivingCell / (t + 1));
            }
            Console.WriteLine(DateTime.Now - span);
            Console.Read();
            return averageLivingCell / MaxTurn;
        }

        private int GetNbNeighbors(int x, int y)
        {
            int nbNeighbors = 0;
            for (int i = x - 1; i <= x + 1; i++)
            {
                if (i == -1 && i != BoardSize) continue;
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (j == -1 && j != BoardSize) continue;
                    if (!(i == x && j == y) && _array[i, j])
                        nbNeighbors++;
                }
            }

            return nbNeighbors;
        }

        //private int GetNbNeighbors(int x, int y)
        //{
        //    int nbNeighbors = 0;

        //    foreach (Pos pos in neighborsNavigator)
        //    {
        //        if ((x + pos.X >= 0 && x + pos.X <= BoardSize - 1) && (y + pos.Y <= BoardSize - 1 && y + pos.Y >= 0) && (_array[x + pos.X, y + pos.Y]))
        //        {
        //            ++nbNeighbors;
        //        }
        //    }

        //    return nbNeighbors;
        //}

        public void SetLiving(Pos[] startBoard)
        {
            foreach (Pos pos in startBoard)
            {
                _array[pos.X, pos.Y] = true;
            }
        }

        private void initNeighborsNavigator()
        {
            neighborsNavigator = new Pos[] {new Pos{X = -1, Y = -1},
                                            new Pos{X = -1, Y = 0},
                                            new Pos{X = -1, Y = 1},
                                            new Pos{X = 0, Y = -1},
                                            new Pos{X = 0, Y = 1},
                                            new Pos{X = 1, Y = -1},
                                            new Pos{X = 1, Y = 0},
                                            new Pos{X = 1, Y = 1},
                                           };
        }
    }

    public struct Pos
    {
        public int X;
        public int Y;
    }
}