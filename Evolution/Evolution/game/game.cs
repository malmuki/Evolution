using System;

namespace Evolution.game
{
    public class Game
    {
        private const int MaxTurn = 30;
        private const int BoardSize = 60;
        private Cell[,] _array = new Cell[BoardSize, BoardSize];
        private Pos[] _neighborsNavigator;

        public void ReinitBoard()
        {
            for (int i = 0; i < BoardSize - 1; i++)
            {
                for (int j = 0; j < BoardSize - 1; j++)
                {
                    _array[i, j].IsAlive = false;
                    _array[i, j].Neighbors = 0;
                }
            }
        }

        public void InitBoard()
        {
            InitNeighborsNavigator();
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    _array[i, j] = new Cell
                    {
                        IsAlive = false,
                        Neighbors = 0,
                        Pos = new Pos
                        {
                            X = i,
                            Y = j
                        }
                    };
                }
            }
        }

        private DateTime _span;

        public void StartGame()
        {
            _span = DateTime.Now;
            int averageLivingCell = 0;

            Cell[,] buffer = _array;

            for (int t = 0; t < MaxTurn; t++)
            {
                System.Threading.Thread.Sleep(250);
                Console.Clear();
                SetNbNeighbors();
                for (int i = 0; i < BoardSize ; i++)
                {
                    for (int j = 0; j < BoardSize; j++)
                    {
                        switch (_array[i, j].Neighbors)
                        {
                            case 0:
                            case 1:
                                buffer[i, j].IsAlive = false;
                                break;

                            case 2:
                                if (_array[i, j].IsAlive)
                                    averageLivingCell++;
                                break;

                            case 3:
                                buffer[i, j].IsAlive = true;
                                averageLivingCell++;
                                break;

                            default:
                                buffer[i, j].IsAlive = false;
                                break;
                        }
                    }
                }
                _array = buffer;
                string bigString = "";

                for (int i = 0; i < BoardSize; i++)
                {
                    for (int j = 0; j < BoardSize; j++)//▒ █

                    {
                        if (_array[j, i].IsAlive)
                        {
                            bigString += "█";
                        } else {
                            bigString += "▒";
                        }
                        //bigString += Convert.ToInt32(_array[j, i].IsAlive);
                    }
                    Console.WriteLine(bigString);
                    bigString = "";
                }


                Console.WriteLine(averageLivingCell / (t + 1));
            }

            Console.WriteLine(DateTime.Now - _span);
            Console.Read();
            //return averageLivingCell / MaxTurn;
        }

        private void SetNbNeighbors()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    _array[i, j].Neighbors = 0;
                }
            }

            foreach (Cell cell in _array)
            {
                if (cell.IsAlive)
                {
                    foreach (Pos pos in _neighborsNavigator)
                    {
                        if ((cell.Pos.X + pos.X >= 0 && cell.Pos.X + pos.X <= BoardSize - 1) && (cell.Pos.Y + pos.Y <= BoardSize - 1 && cell.Pos.Y + pos.Y >= 0))
                        {
                            _array[cell.Pos.X + pos.X, cell.Pos.Y + pos.Y].Neighbors++;
                        }
                    }
                }
            }
        }

        private void InitNeighborsNavigator()
        {
            _neighborsNavigator = new Pos[] {new Pos{X = -1, Y = -1},
                                            new Pos{X = -1, Y = 0},
                                            new Pos{X = -1, Y = 1},
                                            new Pos{X = 0, Y = -1},
                                            new Pos{X = 0, Y = 1},
                                            new Pos{X = 1, Y = -1},
                                            new Pos{X = 1, Y = 0},
                                            new Pos{X = 1, Y = 1},
                                           };
        }

        //private int GetNbNeighbors(int x, int y)
        //{
        //    int nbNeighbors = 0;

        //

        //    return nbNeighbors;
        //}

        public void SetLiving(Pos[] startBoard)
        {
            foreach (Pos pos in startBoard)
            {
                _array[pos.X, pos.Y].IsAlive = true;
            }
        }
    }

    public struct Pos
    {
        public int X;
        public int Y;
    }

    public struct Cell
    {
        public Pos Pos;
        public bool IsAlive;
        public int Neighbors;
    }
}