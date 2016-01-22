using System;

namespace Evolution.game
{
    public class Game
    {
        private const int MaxTurn = 400;
        private const int BoardSize = 60;
        private Cell[,] _array = new Cell[BoardSize, BoardSize];
        private Pos[] _neighborsNavigator = new Pos[] {new Pos{X = -1, Y = -1},
                                            new Pos{X = -1, Y = 0},
                                            new Pos{X = -1, Y = 1},
                                            new Pos{X = 0, Y = -1},
                                            new Pos{X = 0, Y = 1},
                                            new Pos{X = 1, Y = -1},
                                            new Pos{X = 1, Y = 0},
                                            new Pos{X = 1, Y = 1},
                                            };

        //Clean the grid between simulations
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

        //For means of optimisation, call only once to setup the grid's Cells. To clean between simulations instead use ReinitBoard()
        public void InitBoard()
        {
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

        
        //Game loop for a unique simulation whose starting living cells are determined by the startBoard parameter
        //Will eventually return average number of live cells during simulation
        public void StartGame(Pos[] startBoard)
        {
            SetLiving(startBoard);
            DateTime _span = DateTime.Now;
            int totalLiveCellsOfSimulation = 0;

            for (int t = 0; t < MaxTurn; t++)
            {
                System.Threading.Thread.Sleep(200);
                Console.Clear();

                totalLiveCellsOfSimulation += generateFrame();
                showNewFrame(); //without pausing the thread and showing frames, time spent on a simulation is 0.07 ish seconds, while 
                                //time spent on a simulation with graphics seems to be of 0.9 ish seconds

                Console.WriteLine(totalLiveCellsOfSimulation / (t + 1));
            }

            Console.WriteLine(DateTime.Now - _span);
            Console.Read();
            //return averageLivingCell / MaxTurn;
        }


        //Render and show the next frame
        private void showNewFrame()
        {
            string renderedLine = "";

            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (_array[j, i].IsAlive)
                    {
                        renderedLine += "█";
                    }
                    else
                    {
                        renderedLine += "▒";
                    }
                }
                Console.WriteLine(renderedLine);
                renderedLine = "";
            }
        }

        //Generate next frame's game state and assign it to _array. 
        //Returns the number of living cells of the current frame
        private int generateFrame()
        {
            DetermineNbOfNeighbors();
            return applyRules();
        }

        //Apply the rules 
        //Returns the number of living cells of the current frame
        private int applyRules()
        {
            Cell[,] buffer = _array;
            int liveCellsOfFrame = 0;

            for (int i = 0; i < BoardSize; i++)
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
                                ++liveCellsOfFrame;
                            break;

                        case 3:
                            buffer[i, j].IsAlive = true;
                            ++liveCellsOfFrame;
                            break;

                        default:
                            buffer[i, j].IsAlive = false;
                            break;
                    }
                }
            }
            _array = buffer;
            return liveCellsOfFrame;
        }

        //Parse the grid to set each cell's number of neighbors
        private void DetermineNbOfNeighbors()
        {
            resetNumberOfNeighbors();

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

        //Reset the numbere of neighbors of all the cells in the grid
        private void resetNumberOfNeighbors()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    _array[i, j].Neighbors = 0;
                }
            }
        }

        //Set the state of Cells whose position correspond to the given parameter's X-Y coordinates
        private void SetLiving(Pos[] startBoard)
        {
            foreach (Pos pos in startBoard)
            {
                _array[pos.X, pos.Y].IsAlive = true;
            }
        }

        private struct Cell
        {
            public Pos Pos;
            public bool IsAlive;
            public int Neighbors;
        }
    }

    public struct Pos
    {
        public int X;
        public int Y;
    }


}