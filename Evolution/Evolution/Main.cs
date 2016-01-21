using Evolution.game;
using System.Threading;

namespace Evolution
{
    public class main
    {
        public static void Main(string[] args)
        {
            Game game = new Game();
            Visual ui = new Visual();

            game.InitBoard(ui);
            game.SetLiving(new Pos[]
            {
                new Pos{X = 1,Y=0}, new Pos(){X = 1,Y=1}, new Pos(){X = 1,Y=2}
            });

            Thread oThread = new Thread(new ThreadStart(game.StartGame));

            oThread.Start();
        }
    }
}