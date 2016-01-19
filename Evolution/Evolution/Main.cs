using Evolution.game;

namespace Evolution
{
    public class main
    {
        public static void Main(string[] args)
        {
            Game game = new Game();

            game.InitBoard();
            game.StartGame();
        }
    }
}