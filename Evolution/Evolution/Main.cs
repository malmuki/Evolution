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
                new Pos(){X = 1,Y=1}, new Pos(){X = 1,Y=2}, new Pos(){X = 1,Y=3}
            });
            game.StartGame();
        }
    }
}