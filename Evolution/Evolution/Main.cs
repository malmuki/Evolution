using Evolution.game;

namespace Evolution
{
    public class main
    {
        public static void Main(string[] args)
        {
            Game game = new Game();

            game.InitBoard();
            game.SetLiving(new Pos[]
            {
                new Pos(){X = 0,Y=0}, new Pos(){X = 0,Y=1}, new Pos(){X = 0,Y=2}
            });
            game.StartGame();
        }
    }
}