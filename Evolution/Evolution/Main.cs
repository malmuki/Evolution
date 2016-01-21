using Evolution.game;
using System.Threading;

namespace Evolution
{
    public class main
    {
        public static void Main(string[] args)
        {
            Game game = new Game();
            game.InitBoard();

            //Living cells at the start of the simulation
            Pos[] startBoard = new Pos[]{new Pos{X = 1,Y=0}, new Pos(){X = 1,Y=1}, new Pos(){X = 1,Y=2}}; 

            game.StartGame(startBoard);
        }
    }
}