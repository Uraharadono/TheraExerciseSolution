using System;
using System.IO;
using System.Linq;
using Exercise2_Reversi.Models;

namespace Exercise2_Reversi.Util
{
    public class Solution
    {
        public static string PlaceToken(string board)
        {
            string[] dimensions = GetDimensions(board);
            int width = Convert.ToInt32(dimensions[0]);
            int height = Convert.ToInt32(dimensions[1]);

            Board myBoard = new Board(width, height);
            ParseBoard(board, myBoard);

            // Console.WriteLine(myBoard.ReversiBoard[3, 3].GetPrettyPosition());

            myBoard.ShowBoard();

            return "";
        }

        public static string[] GetDimensions(string board)
        {
            String[] boardSplit = board.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            String[] firstBoardLineSplit = boardSplit[0].Split(new string[] { " " }, StringSplitOptions.None);
            return firstBoardLineSplit;
        }
        public static void ParseBoard(string board, Board myBoard)
        {
            String[] boardSplit = board.Split(new string[] { "\r\n" }, StringSplitOptions.None).Skip(1).ToArray();
            for (int i = 0; i < boardSplit.Length; i++)
            {
                String[] lineSplit = boardSplit[i].Trim().Split(new string[] { " " }, StringSplitOptions.None);
                for (int j = 0; j < lineSplit.Length; j++)
                {
                    BoardPiece nBoardPiece = new BoardPiece();
                    nBoardPiece.OriginalValue = lineSplit[j];
                    nBoardPiece.BoardPieceStatus = ResolveBoardPieceStatus(lineSplit[j]);
                    nBoardPiece.XPosition = i;
                    nBoardPiece.YPosition = j;
                    myBoard.AddBoardPiece(nBoardPiece, i, j);
                }
            }
        }

        private static BoardPieceStatus ResolveBoardPieceStatus(string s)
        {
            switch (s)
            {
                case ".":
                    return BoardPieceStatus.EmptyTile;
                case "O":
                    return BoardPieceStatus.OpponentOwned;
                case "X":
                    return BoardPieceStatus.ActivePlayerOwned;
            }
            // This is bad, should be exception thrown but no time to think about that
            return BoardPieceStatus.EmptyTile;
        }
    }

}
