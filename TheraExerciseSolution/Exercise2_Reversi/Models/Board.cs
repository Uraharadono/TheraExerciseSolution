using System;

namespace Exercise2_Reversi.Models
{
    public class Board
    {
        public BoardPiece[,] ReversiBoard { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }


        public Board(int width, int height)
        {
            Width = width;
            Height = height;
            ReversiBoard = new BoardPiece[height, width];
        }

        public void AddBoardPiece(BoardPiece piece, int x, int y)
        {
            ReversiBoard[x, y] = piece;
        }

        public void ShowBoard()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Console.Write(ReversiBoard[i, j] + "  ");
                }
                Console.WriteLine("\n");
            }
        }
    }
}