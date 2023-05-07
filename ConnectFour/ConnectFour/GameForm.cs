using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectFour
{
    public partial class GameForm : Form
    {
        private Rectangle[] boardColumns;
        private int[,] board;
        private int turn;

        public GameForm()
        {
            InitializeComponent();
            this.boardColumns = new Rectangle[9];

            this.board = new int[9, 9];
            this.turn = 1;
        }
        
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
        //4 ways to winning(vertical,horizontal,diagnal(2))
        private int Winner(int playerCheck)
        {
            //vertical
            for (int row = 0; row < this.board.GetLength(0) - 3; row++)
            {
                for (int col = 0; col < this.board.GetLength(1); col++)
                {
                    if (this.AllNumEqual(playerCheck, this.board[row, col], this.board[row + 1, col], this.board[row + 2, col], this.board[row + 3, col])) ;
                    return playerCheck;
                }
            }
            //horiontal
            for (int row = 0; row < this.board.GetLength(0); row++)
            {
                for (int col = 0; col < this.board.GetLength(1) - 3; col++)
                {
                    if (this.AllNumEqual(playerCheck, this.board[row, col], this.board[row, col + 1], this.board[row, col + 2], this.board[row, col + 3])) ;
                    return playerCheck;
                }
            }
            // left diagonal
            for (int row = 0; row < this.board.GetLength(0) - 3; row++)
            {
                for (int col = 0; col < this.board.GetLength(1) - 3; col++)
                {
                    if (this.AllNumEqual(playerCheck, this.board[row, col], this.board[row + 1, col + 1], this.board[row + 2, col + 2], this.board[row + 3, col + 3])) ;
                    return playerCheck;
                }
            }
            // right diagonal
            for (int row = 0; row < this.board.GetLength(0) - 3; row++)
            {
                for (int col = 0; col < this.board.GetLength(1); col++)
                {
                    if (this.AllNumEqual(playerCheck, this.board[row, col], this.board[row + 1, col - 1], this.board[row + 2, col - 2], this.board[row + 3, col - 3])) ;
                    return playerCheck;
                }
            }

            return -1;
        }

        private bool AllNumEqual(int toCheck, params int[] number)
        {
            foreach (int num in number)
            {
                if (num != toCheck)
                    return false;
            }
            return true;
        }

        private int ColumnNumber(Point mouse)
        {
            for (int i = 0; i < this.boardColumns.Length; i++)
            {
                if ((mouse.X >= this.boardColumns[i].X) && (mouse.Y >= this.boardColumns[i].Y))
                {
                    if ((mouse.X <= this.boardColumns[i].X + this.boardColumns[i].Width) && (mouse.Y <= this.boardColumns[i].Y + this.boardColumns[i].Height))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        private int EmptyRow(int col)
        {
            for (int i = 9; i > 0; i--)
            {
                if (this.board[i, col] == 0)
                {
                    return i;
                }
            }
            return -1;
        }

       
        private void GameForm_MouseClick_1(object sender, MouseEventArgs e)
        {
            int columnIndex = this.ColumnNumber(e.Location);
            if (columnIndex != -1)
            {
                int rowIndex = this.EmptyRow(columnIndex);
                if (rowIndex != -1)
                {

                    this.board[rowIndex, columnIndex] = this.turn;
                    if (this.turn == 1)
                    {
                        Graphics g = this.CreateGraphics();
                        g.FillEllipse(Brushes.Black, 20 + 48 * columnIndex, 20 + 48 * rowIndex, 32, 32);
                    }
                    else if (this.turn == 2)
                    {
                        Graphics g = this.CreateGraphics();
                        g.FillEllipse(Brushes.Red, 20 + 48 * columnIndex, 20 + 48 * rowIndex, 32, 32);
                    }
                    int winn = this.Winner(this.turn);
                    if (winn != -1)
                    {
                        string player = (winn == 1) ? "Black" : "Red"; //if winner equal 1 winner is red else winner is yellow
                        MessageBox.Show("CONGRATLATIONS!" + player + "Player is won");
                        Application.Restart();
                    }
                    if (this.turn == 1)
                        this.turn = 2;
                    else
                        this.turn = 1;

                }
            }
        }

        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            this.Paint += GameForm_Paint1;
            e.Graphics.FillRectangle(Brushes.CornflowerBlue, 10, 10, 450, 450);
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (i == 0)
                    {
                        this.boardColumns[j] = new Rectangle(20 + 48 * j, 24, 32, 450);
                    }
                    e.Graphics.FillEllipse(Brushes.White, 20 + 48 * j, 20 + 48 * i, 32, 32);
                }
            }
        }

        private void GameForm_Paint1(object sender, PaintEventArgs e)
        {
            throw new NotImplementedException();
        }

       
    }
}
