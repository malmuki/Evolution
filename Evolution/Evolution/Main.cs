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
            //game.StartGame("0110100110101000100111111011001000000010100000001010010001100110100011001100101001100100010010101010010000001111100010111101011100001100010100100100000101000110110100001111011101110011100100010101110110100110011111001011001100000110100010101111001000110010110000111011101100001101011110000101001000100000100100111011011111010001010010010111011000101100011110101101000010100101101101110000000000011010");


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
            ai.startEvolution(10000);
            Console.Read();
        }
    }
}

//{new Pos{X = 1,Y=0}, new Pos(){X = 1,Y=1}, new Pos(){X = 1,Y=2}