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
            //game.StartGame("0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000000000000000000100000000000000000001000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");


            //Random RNG = new Random();

            //string stringBuilder = "";
            //for (int x = 0; x < 20; x++)
            //{
            //    for (int y = 0; y < 20; y++)
            //    {
            //        stringBuilder += RNG.Next(0, 2);
            //    }
            //}
            //game.StartGame(stringBuilder);

            EvolutionAI ai = new EvolutionAI(game);
            ai.startEvolution(500);
            Console.Read();
        }
    }
}

//{new Pos{X = 1,Y=0}, new Pos(){X = 1,Y=1}, new Pos(){X = 1,Y=2}