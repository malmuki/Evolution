using Evolution.game;
using System.Threading;
using Evolution.AI;
using System;

namespace Evolution
{
    public class main
    {
        public static void Main(string[] args)
        {
            Game game = new Game();
            game.InitBoard();

            EvolutionAI ai = new EvolutionAI(game);
            ai.startEvolution(10000);
            Console.Read();
        }
    }
}

//{new Pos{X = 1,Y=0}, new Pos(){X = 1,Y=1}, new Pos(){X = 1,Y=2}