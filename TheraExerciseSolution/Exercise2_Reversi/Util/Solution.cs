using System;
using Exercise2_Reversi.Models;

namespace Exercise2_Reversi.Util
{
    public class Solution
    {
        public static string PlaceToken(string board)
        {
            string[] dimensions = Parsers.GetDimensions(board);
            int width = Convert.ToInt32(dimensions[0]);
            int height = Convert.ToInt32(dimensions[1]);

            /* I didn't check for size of board, because I don't see point at the moment, but if needed would be simple. */

            Board myBoard = new Board(width, height);
            Parsers.ParseBoard(board, myBoard);


            myBoard.ShowBoard();

            return myBoard.GetOptimalPathToTake();
        }
    }

}
