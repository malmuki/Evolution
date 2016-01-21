using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Evolution.game
{
    public class Visual
    {
        private readonly Form _form;
        private readonly PictureBox _pckBox;

        public Visual()
        {
            _form = new Form();
            _pckBox = new PictureBox { Image = new Bitmap(@"C:\Users\Francois\Desktop\Sans titre.bmp") };
            _form.Controls.Add(_pckBox);

            _form.Show();
        }

        public void Update(Cell[,] board)
        {
            const int squareWidth = 25;
            const int squareHeight = 25;
            using (Bitmap bmp = new Bitmap((board.GetUpperBound(0) + 1) * squareWidth, (board.GetUpperBound(1) + 1) * squareHeight))
            {
                using (Graphics gfx = Graphics.FromImage(bmp))
                {
                    gfx.Clear(Color.Black);
                    for (int y = 0; y <= board.GetUpperBound(1); y++)
                    {
                        for (int x = 0; x <= board.GetUpperBound(0); x++)
                        {
                            gfx.FillRectangle(board[x, y].IsAlive ? Brushes.White : Brushes.Black,
                                new Rectangle(x*squareWidth, y*squareHeight, squareWidth, squareHeight));
                        }
                    }
                }
                _pckBox.Image = bmp;

            }
        }
    }
}